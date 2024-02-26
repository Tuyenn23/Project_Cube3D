using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class RotateBricks : MonoBehaviour
{
    public float rotateSpeed = 5f;
    public bool IsRotating;



    private void Update()
    {
        if (Input.GetMouseButton(0) && !GameManager.Instance.isPause)
        {
            IsRotating = true;
            RotationObject();
        }

        if (Input.GetMouseButtonUp(0))
        {
            IsRotating = false;
        }
    }
    private void RotationObject()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // ??o ng??c h??ng xoay
        float rotationAmountY = mouseY * rotateSpeed;
        float rotationAmountX = mouseX * rotateSpeed;

        // T?o Vector3 m?i ch?a góc xoay
        Vector3 newRotation = new Vector3(rotationAmountY, -rotationAmountX, 0);

        // Áp d?ng xoay cho GameObject
        transform.RotateAround(transform.position, newRotation, rotateSpeed * Time.deltaTime);
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(Vector3.zero, new Vector3(5, 5, 5));
    }


}
