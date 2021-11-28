using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[RequireComponent(typeof(Animator))]
public class GirlAnimator : MonoBehaviour
{
    private const string _hit = "Hit";

    private Animator _animator;
    private HitEndCaller _caller;

    public event UnityAction Hit;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _caller = _animator.GetBehaviour<HitEndCaller>();
    }

    private void OnEnable()
    {
        _caller.Ended += Invoke;
    }

    private void OnDisable()
    {
        _caller.Ended -= Invoke;
    }

    public void DoHit()
    {
        _animator.SetTrigger(_hit);
    }

    private void Invoke()
    {
        Hit?.Invoke();
    }
}
