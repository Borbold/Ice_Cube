using UnityEngine;

[RequireComponent(typeof(Cube))]
public class CubeMelt : MonoBehaviour
{
    [SerializeField] private float _time;
    [SerializeField] private float _timeRandomnes;
    [SerializeField] private Vector3 _targetScale;

    public static float TimeScale = 1f;

    private Cube _cube;
    private Vector3 _baseScale;
    private float _timer;
    private bool _isMelting = true;

    private void Awake()
    {
        _cube = GetComponent<Cube>();
        _time += Random.Range(-_timeRandomnes, _timeRandomnes);
        _timer = 0;
        _baseScale = transform.localScale;
        var target = _baseScale;
        target.x *= _targetScale.x;
        target.y *= _targetScale.y;
        target.z *= _targetScale.z;
        _targetScale = target;
    }

    private void OnEnable()
    {
        _cube.Destroying += OnDestroying;
    }

    private void OnDisable()
    {
        _cube.Destroying -= OnDestroying;
    }

    private void Update()
    {
        _timer += Time.deltaTime * TimeScale;
        if (_isMelting)
        {
            UpdateScale();
            if (_timer >= _time)
            {
                _cube.KillbyMelt();
                _isMelting = false;
            }
        }
    }

    public void Reset()
    {
        _timer = 0;
        _isMelting = true;
    }

    private void UpdateScale() => transform.localScale = Vector3.Lerp(_baseScale, _targetScale, _timer/_time);

    private void OnDestroying(Cube cube)
    {
        _isMelting = false;
    }
}
