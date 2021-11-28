using UnityEngine;
using UnityEngine.UI;

public class UiFollow : MonoBehaviour
{
    public Vector3 canv;
    public Vector3 screen;
    public Vector3 world;

    [SerializeField] private  float _offsetZ = 10f;
    [SerializeField] private RectTransform _ui;
    [SerializeField] private RectTransform _parent;

    private void Update()
    {
        transform.position = CalculateWorldPosition();
    }

    private Vector3 CalculateWorldPosition()
    {
        var canvasPoint = new Vector2();
        canvasPoint.x =  _parent.anchoredPosition.x / _ui.rect.width + 0.5f;
        canvasPoint.y = _parent.anchoredPosition.y / _ui.rect.height + 0.5f;
        canv = canvasPoint;
        var screenPosition = new Vector3();
        var camera = Camera.main;
        screenPosition.x = camera.pixelWidth * canvasPoint.x;
        screenPosition.y = camera.pixelHeight * canvasPoint.y;
        screenPosition.z = _offsetZ;
        screen = screenPosition;
        Vector3 worldPoint = camera.ScreenToWorldPoint(screenPosition);
        world = worldPoint;
        return worldPoint;    
    }
}
