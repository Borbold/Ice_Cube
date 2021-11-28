using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class HorizontalCameraInput : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] [Range(0,0.5f)] private float _closeMouseRange;
    [SerializeField] bool _debugMode;

    private Camera _camera;

    private float _power;

    public float Power => _power;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
    }

    private void Update()
    {
        UpdateDirection();
        if (_debugMode)
            ShowDebug();
    }

    private float CalculateCloseRangeInterpolation(float mouseX, float playerX)
    {
        var delta = mouseX - playerX;
        if (Mathf.Abs(delta) < _closeMouseRange * _camera.pixelWidth)
            return (mouseX - playerX) / (_closeMouseRange * _camera.pixelWidth);
        return delta/Mathf.Abs(delta);
    }

    private void ShowDebug()
    {
        var start = _camera.ScreenToWorldPoint(new Vector3(_camera.pixelWidth / 2, 0, 5));
        var target = _camera.ScreenToWorldPoint(new Vector3(_camera.pixelWidth / 2, _camera.pixelHeight, 5));
        Debug.DrawLine(start, target);
        start = _camera.ScreenToWorldPoint(new Vector3(_camera.pixelWidth * (0.5f + _closeMouseRange), 0, 5));
        target = _camera.ScreenToWorldPoint(new Vector3(_camera.pixelWidth * (0.5f + _closeMouseRange), _camera.pixelHeight, 5));
        Debug.DrawLine(start, target);
        start = _camera.ScreenToWorldPoint(new Vector3(_camera.pixelWidth * (0.5f - _closeMouseRange), 0, 5));
        target = _camera.ScreenToWorldPoint(new Vector3(_camera.pixelWidth * (0.5f - _closeMouseRange), _camera.pixelHeight, 5));
        Debug.DrawLine(start, target);
    }

    private void UpdateDirection()
    {
        if (Input.GetMouseButton(0))
        {
            var playerX = _camera.WorldToScreenPoint(_player.position).x;
            var mouseX = Input.mousePosition.x;
            _power = CalculateCloseRangeInterpolation(mouseX, playerX);
        }
        else
        {
            _power = 0;
        }
    }
}
