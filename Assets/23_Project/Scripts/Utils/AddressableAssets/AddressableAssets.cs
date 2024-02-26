using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UIElements;

public class AddressableAssets : MonoBehaviour
{
    public static AddressableAssets instance;

    public List<AssetReference> L_PrefabReferences;

    public Dictionary<AssetReference, List<GameObject>> L_spawnedPrefabSystem =
    new Dictionary<AssetReference, List<GameObject>>();

    //queue

    public Dictionary<AssetReference, Queue<Vector3>> L_QueuedSpawnRequests =
    new Dictionary<AssetReference, Queue<Vector3>>();

    public Dictionary<AssetReference, AsyncOperationHandle<GameObject>> L_asynOperationHandles =
    new Dictionary<AssetReference, AsyncOperationHandle<GameObject>>();


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }


    public void Spawn(int index)
    {
        if (index < 0 || index >= L_PrefabReferences.Count) return;


        AssetReference assetReference = L_PrefabReferences[index];

        if (assetReference.RuntimeKeyIsValid() == false)
        {
            Debug.Log("Invaild Key " + assetReference.RuntimeKey.ToString());
        }

        if (L_asynOperationHandles.ContainsKey(assetReference))
        {
            if (L_asynOperationHandles[assetReference].IsDone)
            {
                SpawnFrefabFormLoadedReference(assetReference, new Vector3(0, 0, 0));
                // spawn
            }
            else
            {
                EnqueueSpawnforAferInit(assetReference);
                // queue
            }
        }
        LoadAndSpawn(assetReference);
    }

    public void LoadAndSpawn(AssetReference assetReference)
    {
        var op = Addressables.LoadAssetAsync<GameObject>(assetReference);
        L_asynOperationHandles[assetReference] = op;

        op.Completed += (operation) =>
        {
            SpawnFrefabFormLoadedReference(assetReference, new Vector3(0, 0, 0));

            if (L_QueuedSpawnRequests.ContainsKey(assetReference))
            {
                while (L_QueuedSpawnRequests[assetReference]?.Any() == true)
                {
                    var position = L_QueuedSpawnRequests[assetReference].Dequeue();
                    // spawn
                    SpawnFrefabFormLoadedReference(assetReference, position);
                }
            }
        };
    }

    public void EnqueueSpawnforAferInit(AssetReference assetReference)
    {
        if (L_QueuedSpawnRequests.ContainsKey(assetReference))
        {
            L_QueuedSpawnRequests[assetReference] = new Queue<Vector3>();
        }
        L_QueuedSpawnRequests[assetReference].Enqueue(new Vector3(0, 0, 0));
    }

    public void SpawnFrefabFormLoadedReference(AssetReference assetReference, Vector3 posititon)
    {
        assetReference.InstantiateAsync(posititon, Quaternion.identity).Completed += asynOperationHandle =>
        {
            if (L_spawnedPrefabSystem.ContainsKey(assetReference) == false)
            {
                L_spawnedPrefabSystem[assetReference] = new List<GameObject>();
            }
            L_spawnedPrefabSystem[assetReference].Add(asynOperationHandle.Result);

            var notify = asynOperationHandle.Result.AddComponent<NotifyOnDestroy>();
            notify.Destroyed += DestroyPrefab;
            notify.AssetReference = assetReference;
        };
    }
    public void DestroyPrefab(AssetReference assetReference, NotifyOnDestroy obj)
    {
        Addressables.ReleaseInstance(obj.gameObject);

        L_spawnedPrefabSystem[assetReference].Remove(obj.gameObject);
        if (L_spawnedPrefabSystem[assetReference].Count == 0)
        {
            Debug.Log("remove all" + assetReference.RuntimeKey.ToString());

            if (L_asynOperationHandles[assetReference].IsValid())
            {
                Addressables.Release(L_asynOperationHandles[assetReference]);
            }
            L_asynOperationHandles.Remove(assetReference);
        }
    }

}
