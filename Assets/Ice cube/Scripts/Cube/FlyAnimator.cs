using UnityEngine;

//[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CubeMover))]
public class FlyAnimator : MonoBehaviour
{
    [SerializeField] private TrailRenderer _effect;

    private const float _activationDelay = 0.5f;
    private const float _minVelocity = 0.1f;
    private const int _cubesLayer = 6;
    private const int _animatedCubesLayer = 7;

    private Blocker _moveBlocker;
    private float _timer;
    //private Rigidbody _rigidBody;
    private CubeMover _mover;
    private bool _active;

    //private  bool _fallen => _rigidBody.velocity.magnitude < _minVelocity;

    private void Awake()
    {
        _moveBlocker = new Blocker("Fly");
        //_rigidBody = GetComponent<Rigidbody>();
        _mover = GetComponent<CubeMover>();
        gameObject.layer = _animatedCubesLayer;
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= _activationDelay)
            //if (_fallen)
                    if (_active)
                        Deactivate();
    }

    public void Reset()
    {
        Deactivate();
    }

    public void Activate()
    {
        _active = true;
        _effect.emitting = false;
        _mover.Block(_moveBlocker);
        //_rigidBody.isKinematic = false;
        gameObject.layer = _animatedCubesLayer;
        _timer = 0;
    }

    private void Deactivate()
    {
        _active = false;
        _effect.emitting = true;
        _mover.Unblock(_moveBlocker);
        //_rigidBody.isKinematic = true;
        gameObject.layer = _cubesLayer;
    }
}
