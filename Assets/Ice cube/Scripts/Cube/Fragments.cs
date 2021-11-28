using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Fragments : MonoBehaviour
{
    [SerializeField] private float _hideTime;
    [SerializeField] [Min(1)] private float _hideTimeRandomness;

    private Fragment[] _fragments;

    public float HideTime => _hideTime;

    public event UnityAction<Fragments> Hided;

    private void Awake()
    {
        _hideTime *= Random.Range(1/_hideTimeRandomness, _hideTimeRandomness);
        _fragments = GetComponentsInChildren<Fragment>();
    }

    public void Activate()
    {
        foreach (var fragment in _fragments)
        {
            fragment.Activate();
            fragment.Hide(_hideTime);
        }
        Invoke(nameof(CallHided), _hideTime*2);
    }

    public void Reset()
    {
        foreach (var fragment in _fragments)
        {
            fragment.Reset();
        }
    }

    private void CallHided()
    {
        Hided?.Invoke(this);
    }
}
