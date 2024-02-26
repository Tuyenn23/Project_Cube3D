using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 offset;

    void OnMouseDown()
    {
        // X? l� s? ki?n khi chu?t ???c nh?n
        if(isDragging)
        {
         
            Debug.Log("ONMOUSEDOWN");
            offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }

    void OnMouseUp()
    {
        // X? l� s? ki?n khi chu?t ???c nh?
        isDragging = false;
    }

    void OnMouseDrag()
    {
        // X? l� s? ki?n khi chu?t ?ang ???c gi? v� di chuy?n
        isDragging = true;
        Vector3 cursorPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, offset.z);
        Vector3 newPosition = Camera.main.ScreenToWorldPoint(cursorPosition) + offset;
        transform.position = newPosition;
    }

    void Update()
    {
        // X? l� c�c s? ki?n kh�c ? ?�y n?u c?n
        if (isDragging)
        {
            // X? l� c�c thao t�c kh�c khi ?ang di chuy?n
            Debug.Log("Dragging!");
        }
    }
}
