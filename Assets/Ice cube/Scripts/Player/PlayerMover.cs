using UnityEngine;
using UnityEngine.Events;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private HorizontalCameraInput _input;
    [SerializeField] private float _forwardSpeed;
    [SerializeField] private float _strafeSpeed;
    [SerializeField] private LayerMask _ground;

    private bool _isStrafeActive = true;
    private bool _active = false;

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
        var strafeStep = transform.right * _strafeSpeed * _input.Power * Time.deltaTime;
        if (CheckWalkable(transform.position + strafeStep) && _isStrafeActive)
            transform.position += strafeStep;
    }

    private bool CheckWalkable(Vector3 position) => Physics.Raycast(position, Vector3.down, float.MaxValue, _ground);

}
