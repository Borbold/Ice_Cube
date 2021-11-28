using UnityEngine;
using UnityEngine.Events;

public class CubeJump : MonoBehaviour
{
    [SerializeField] private AnimationCurve _jumpCurve;

    private float _height;
    private float _startHeight;
    private float _time;
    private float _timer;

    public event UnityAction<float> Jumping;

    private void Awake()
    {
        _timer = _time;
    }

    private void Update()
    {
        if (_timer < _time)
        {
            _timer += Time.deltaTime;
            Move();
        }
    }

    private float CalculateHeigth(float progress) => Mathf.Clamp( _jumpCurve.Evaluate(progress), 0, 1) * _height + _startHeight;

    private void Move()
    {
        float height = CalculateHeigth(_timer / _time);
        Vector3 targetPosition = transform.position;
        targetPosition.y =  height;
        transform.position = targetPosition;
    }

    public void Jump(float time, float height)
    {
        _startHeight = transform.position.y;
        _height = height * Random.Range(0.8f, 1.1f);
        _time = time * Random.Range(0.9f, 1.1f);
        _timer = 0;
        Jumping?.Invoke(time);
    }

    public void Reset()
    {
        _timer = _time;
    }
}
