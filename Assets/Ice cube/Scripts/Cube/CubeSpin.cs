using UnityEngine;

[RequireComponent(typeof(CubeJump))]
public class CubeSpin : MonoBehaviour
{
    [SerializeField] [Min(2)] private int _maxRotations;
    [SerializeField] private float _interpolationSpeed;

    private float _time;
    private float _timer;
    private Quaternion _start;
    private float  _rotation;
    private Vector3 _axis;
    private CubeJump _jump;

    private bool _isSpinning => _timer < _time;

    private void Awake()
    {
        _jump = GetComponent<CubeJump>();
        _timer = _time;
    }

    private void OnEnable()
    {
        _jump.Jumping += StartRotate;
    }

    private void OnDisable()
    {
        _jump.Jumping -= StartRotate;
    }

    private void Rotate(float progress)
    {
        var angle = Mathf.Lerp(0, _rotation, progress);
        Quaternion targetRotation = _start * Quaternion.AngleAxis(angle, _axis);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, _interpolationSpeed);
    }

    private void Update()
    {
        if (_isSpinning)
        {
            _timer += Time.deltaTime;
            Rotate(_timer / _time);
        }
    }

    private void StartRotate(float time)
    {
        transform.rotation = Quaternion.identity;
        _start = transform.rotation;
        _rotation = Random.Range(1, _maxRotations) * 180f;
        _axis = new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), Random.Range(-1, 1));
        _time = time;
        _timer = 0;
    }

    public void Reset()
    {
        _timer = _time;
        transform.rotation = Quaternion.identity;
    }
}
