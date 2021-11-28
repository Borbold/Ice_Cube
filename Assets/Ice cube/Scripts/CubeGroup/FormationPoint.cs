using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;

public class FormationPoint : MonoBehaviour
{
    public Cube _follower;
    private int _raw;
    public FormationPoint _previos;

    public Cube Follower
    {
        get => _follower;
        set
        {
            _follower = value;
            if (value == null)
                Cleared?.Invoke(this);
            else
                value.transform.parent = transform;
        }
    }
    public int Raw => _raw;
    public bool IsBysy => _follower != null;

    public event UnityAction<FormationPoint> Cleared;

    public void Init(int raw, FormationPoint previos)
    {
        _raw = raw;
        _previos = previos;
        if (_previos != null)
            _previos.Cleared += OnPointCleared;
    }

    private void Update()
    {
        if (IsBysy)
        {
            if (transform.position != _follower.transform.position)
            {
                Vector3 targetPosition = Vector3.Lerp(_follower.transform.position, transform.position, 0.08f);
                targetPosition.y = _follower.transform.position.y;
                _follower.transform.position = targetPosition;
            }
        }
    }

    private void OnPointCleared(FormationPoint point)
    {
        if (Follower != null && point.Follower == null)
        {
            point.Follower = Follower;
            Follower = null;
        }
    }
}
