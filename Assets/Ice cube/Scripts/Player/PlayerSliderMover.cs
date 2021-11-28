using UnityEngine;
using UnityEngine.Events;

public class PlayerSliderMover : MonoBehaviour
{
    [SerializeField] private SliderInput _input;
    [SerializeField] private Formation _formation;
    [Header("Settings")]
    [SerializeField] private float _maxStrafe;
    [SerializeField] private float _interpolationRange;
    [SerializeField] private float _forwardSpeed;
    [SerializeField] private float _strafeSpeed;
    [SerializeField] private LayerMask _ground;

    private bool _isStrafeActive = true;
    private bool _active = false;
    private Vector3 _currentStrafe;

    private Vector2 _playerSize => _formation.ActiveRange;
    private float _maxStrafeLeft => Mathf.Clamp(_maxStrafe + _playerSize.x, 0, float.MaxValue);
    private float _maxStrafeRight => Mathf.Clamp(_maxStrafe - _playerSize.y, 0, float.MaxValue);


    public float ForwardSpeed => _forwardSpeed;

    public event UnityAction Activated;

    private void Update()
    {
        if (_active)
            TryMove();
    }

    public void Activate()
    {
        _active = true;
        Activated?.Invoke();
    }

    public void Stop() => _active = false;

    public Vector3 CalculatePositionAfterTime(float time) => transform.position + transform.forward * _forwardSpeed * time;

    public void TurnStrafeOFF() => _isStrafeActive = false;

    public void TurnStrafeOn() => _isStrafeActive = true;

    public void TurnStrafeOn(float delay) => Invoke(nameof(TurnStrafeOn), delay);
    private void TryMove()
    {
        var forwardStep = transform.forward * _forwardSpeed * Time.deltaTime;
        if (CheckWalkable(transform.position + forwardStep))
            transform.position += forwardStep;
        if (_input.IsPressed)
        {
            float input = (_input.GetPosition() - 0.5f) * 2;
            float maxStrafe;
            if (input >= 0)
                maxStrafe = _maxStrafeRight;
            else
                maxStrafe =_maxStrafeLeft;
            Vector3 targetStrafe = maxStrafe * input * transform.right;
            Vector3 strafeStep = (targetStrafe - _currentStrafe).normalized * Time.deltaTime * _strafeSpeed;
            if ((targetStrafe - _currentStrafe).magnitude < _interpolationRange)
                strafeStep *= (targetStrafe - _currentStrafe).magnitude / _interpolationRange;
            var newPos = transform.position + strafeStep;
            if (CheckWalkable(newPos) && _isStrafeActive)
            {
                transform.position = newPos;
                _currentStrafe += strafeStep;
            }
        }
    }

    private bool CheckWalkable(Vector3 position) => Physics.Raycast(position, Vector3.down, float.MaxValue, _ground);
}
