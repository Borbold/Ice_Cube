using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 _direction;
    [SerializeField] private float _maxStarfeoffset;
    [SerializeField] private float _speed;

    private Vector3 _offset;
    private float _strafe;

    private void Awake()
    {
        _offset = transform.position - _target.position;
    }

    private void LateUpdate()
    {
        Vector3 strafeDirection = transform.right;
        Vector3 strafeVector = Vector3.Project(_target.position, transform.right);
        float strafeOffset = strafeVector.magnitude;
        if (strafeVector.normalized != strafeDirection.normalized)
            strafeOffset *= -1;
        _strafe = Mathf.Clamp(strafeOffset, -_maxStarfeoffset, _maxStarfeoffset);
        Vector3 targetPosition = Vector3.Project(_target.position, _direction) + _offset;
        targetPosition += _strafe * strafeDirection;
        transform.position = Vector3.Lerp(transform.position, targetPosition, _speed * Time.deltaTime);
    }
}
