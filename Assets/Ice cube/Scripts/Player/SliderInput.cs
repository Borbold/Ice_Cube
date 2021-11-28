using UnityEngine;

[RequireComponent(typeof(Camera))]
public class SliderInput : MonoBehaviour
{
    [SerializeField] [Range(0, 1f)] private float _screenSize;

    private float _minX;
    private float _maxX;
    private Camera _camera;

    public bool IsPressed => Input.GetMouseButton(0);

    private void Awake()
    {
        _camera = GetComponent<Camera>();
        _minX = _camera.pixelWidth * (0.5f - _screenSize / 2);
        _maxX = _camera.pixelWidth * (0.5f + _screenSize / 2);
    }

    public float GetPosition()
    {
            float x = Mathf.Clamp(Input.mousePosition.x, _minX, _maxX);
            float maxDelta = _maxX - _minX;
            return (x - _minX) / maxDelta;
    }
}
