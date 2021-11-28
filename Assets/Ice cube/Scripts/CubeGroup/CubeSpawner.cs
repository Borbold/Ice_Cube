using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private Cube _template;
    [SerializeField] private Transform _player;
    [SerializeField] private float _radius;
    [SerializeField] private ShatterPool _effectPool;

    public Vector3 Point => _player.position + Vector3.forward * Random.Range(0f,1f) * _radius + Vector3.right * Random.Range(0f, 1f) * _radius;

    public Cube Spawn()
    {
        Cube cube = Instantiate(_template, Point, Quaternion.identity, transform);
        if (cube.TryGetComponent(out ShatterDeath death))
        {
            death.Init(_effectPool);
        }
        return cube;
    }
}
