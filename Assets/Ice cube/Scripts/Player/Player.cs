using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] PlayerSliderMover _mover;

    public event UnityAction<Gate> GateHit;
    public event UnityAction<float, float> Jumping; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Gate gate))
        {
            gate.Deactivate();
            GateHit?.Invoke(gate);
        }
    }

    public void Jump(float height, float time)
    {
        var jumpDistance = Vector3.Distance(transform.position, _mover.CalculatePositionAfterTime(time));
        Jumping?.Invoke(height, time);
        _mover.TurnStrafeOFF();
        _mover.TurnStrafeOn(time);
    }
}
