using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    float _cameraOffSet = 5;
    private Transform _camera;
    public Transform SnakeHead;


    private void Awake()
    {
        _camera = GetComponent<Transform>();
    }
    private void Update()
    {
        if (_camera.position.z < SnakeHead.position.z - _cameraOffSet)
        {
            _camera.position = new Vector3(_camera.position.x, _camera.position.y, SnakeHead.position.z - _cameraOffSet);
        }
        Debug.Log(_camera.position.z);
        Debug.Log(SnakeHead.position.z - _cameraOffSet + "-----");
    }
}
