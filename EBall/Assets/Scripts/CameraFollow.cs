using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Player _target;
    [SerializeField] private float _smoothSpeed = 0.125f;

    private Vector3 _offset;
    private Camera _camera;
    private float _startCameraSize = 8f;

    public float StartCameraSize => _startCameraSize;

    private void Start()
    {
        _camera = GetComponent<Camera>();

        if (_target == null)
            return;

         _offset = transform.position - _target.transform.position;
    }

    private void FixedUpdate()
    {  
        Vector3 targetPosition = _target.transform.position + _offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, _smoothSpeed * Time.fixedDeltaTime);
        transform.position = smoothedPosition; 
    }

    public void ChangeCameraSize(float newSize)
    {
        _camera.orthographicSize = newSize;
    }
}

