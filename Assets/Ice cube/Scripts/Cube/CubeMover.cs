using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMover : MonoBehaviour
{
    [SerializeField] private float _speed;

    private List<Blocker> _blockers;
    private Transform _target;

    private bool _blocked => _blockers.Count > 0;
    public Transform Target => _target;

    private void Awake()
    {
        _blockers = new List<Blocker>();
    }

    private void Update()
    {
        if (_blocked == false && _target != null)
        {
            //Move();
            //Rotate();
        }
    }

    public void Reset()
    {
        _blockers = new List<Blocker>();
        _target = null;
    }

    public void Continue()
    {
        if(Target != null)
            transform.position = _target.position;
            transform.parent = Target;
    }

    public void Init(Transform target)
    {
        _target = target;
        transform.position = _target.position;
        transform.parent = Target;
    }

    public void Block(Blocker block)
    {
        if (_blockers.Contains(block) == false)
            _blockers.Add(block);
    }

    public void Unblock(Blocker block)
    {
        while (_blockers.Contains(block))
            _blockers.Remove(block);
    }

    public void Stop()
    {
        _target = null;
        transform.parent = null;
    }

    private void Move()
    {
        Vector3 path = _target.position - transform.position;
        float stepDistace = Mathf.Clamp(_speed * Time.deltaTime, 0, path.magnitude);
        Vector3 direction = path.normalized;
        transform.position += direction * stepDistace;
    }

    private void Rotate()
    {
        Vector3 path = _target.position - transform.position;
        if (Quaternion.Angle(Quaternion.LookRotation(path), transform.rotation) > 1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(path);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 5f);
        }
    }
}