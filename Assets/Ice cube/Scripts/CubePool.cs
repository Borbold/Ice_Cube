using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubePool : MonoBehaviour
{
    [SerializeField] private CubeSpawner _spawner;
    [SerializeField] private CubeGroup _group;
    [SerializeField] private int _size;

    private List<Cube> _freeCubes;
    private List<Cube> _allCubes;

    private void Awake()
    {
        _freeCubes = new List<Cube>();
        _allCubes = new List<Cube>();
        while (_freeCubes.Count < _size)
        {
            AddNew();
        }
    }

    private void OnDestroy()
    {
        foreach (Cube cube in _allCubes) 
        {
            cube.Destroyed -= OnCubeDeath;
        }
    }

    public Cube Take()
    {
        if (_freeCubes.Count == 0)
        {
            AddNew();
        }
        var cube = _freeCubes[0];
        _freeCubes.Remove(cube);
        cube.transform.position = _spawner.Point;
        cube.gameObject.SetActive(true);
        return cube;
    }

    private void AddNew()
    {
        var newcube = _spawner.Spawn();
        _allCubes.Add(newcube);
        newcube.Destroyed += OnCubeDeath;
        _freeCubes.Add(newcube);
        newcube.gameObject.SetActive(false);
    }

    private void OnCubeDeath(Cube cube)
    {
        _freeCubes.Add(cube);
        cube.gameObject.SetActive(false);
        cube.GetComponent<CubeReset>().Reset();
    }
}
