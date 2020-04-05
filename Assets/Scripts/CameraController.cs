using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target;
    
    public float rotSpeed = 1.5f;
    public bool lockCursor = true;

    private Vector3 _offset;
    private float _rotY;
    
    void Start()
    {
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        
        _offset = transform.position - target.position;
        _rotY = transform.eulerAngles.y + 180f;
    }

    void LateUpdate()
    {
        _rotY += Input.GetAxis("Mouse X") * rotSpeed * 3;

        Quaternion rotation = Quaternion.Euler(0, _rotY, 0);
        transform.position = target.position + (rotation * _offset);
        transform.LookAt(target);
    }
}
