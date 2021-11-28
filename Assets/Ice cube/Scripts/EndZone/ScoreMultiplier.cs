using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider))]
public class ScoreMultiplier : MonoBehaviour
{
    [SerializeField] private float _value;
    [SerializeField] private int _cubeCost;

    private BoxCollider _collider;

    public bool Active { get; private set; }

    public float Value => _value;
    public int CubeCost => _cubeCost;

    public event UnityAction Reached;

    private void Awake()
    {
        _collider = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Active == false)
        {
            if (other.TryGetComponent(out Cube cube))
            {
                _collider.enabled = false;
                Reached?.Invoke();
                Active = true;
            }
        }
    }
}
