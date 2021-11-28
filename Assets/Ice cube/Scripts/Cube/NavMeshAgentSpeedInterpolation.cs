using UnityEngine;
using UnityEngine.AI;

public class NavMeshAgentSpeedInterpolation : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private float _distance;

    private float _speed;

    private float _interpolatedSpeed
    {
        get
        {
            if (_agent.remainingDistance < _distance)
                return Mathf.Lerp(0, _speed, _agent.remainingDistance / _distance);
            else
                return _speed;
        }
    }

    private void Awake()
    {
        _speed = _agent.speed;
    }

    private void Update()
    {
        if (_agent.isOnNavMesh)
            _agent.speed = _interpolatedSpeed;
    }

}
