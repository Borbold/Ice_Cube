using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class CupAttractor : MonoBehaviour
{
    [SerializeField] private float _maxSpeed;

    private float _range;

    private List<Cube> _cubes;

    private void Awake()
    {
        var collider = GetComponent<SphereCollider>();
        _range = collider.radius;
        _cubes = new List<Cube>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Cube cube))
        {
            if( _cubes.Contains(cube) == false)
            {
                _cubes.Add(cube);
                cube.Destroying += OnCubeDestroying;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Cube cube))
        {
            _cubes.Remove(cube);
            cube.Destroying -= OnCubeDestroying;
        }
    }

    private void Update()
    {
        ClearNulls();
        foreach (Cube cube in _cubes)
        {
            if (cube!=null)
                Attract(cube);
        }
    }

    private void Attract(Cube cube)
    {
        float distance = Vector3.Distance(cube.transform.position, transform.position);
        Debug.DrawLine(cube.transform.position, transform.position, Color.red, 0.1f);
        float speed = _maxSpeed * distance / _range;
        Vector3 direction = (transform.position - cube.transform.position).normalized;
        cube.transform.position += direction * speed * Time.deltaTime;
    }

    private void ClearNulls()
    {
        while (_cubes.Contains(null))
                _cubes.Remove(null);
    }

    private void OnCubeDestroying(Cube cube)
    {
        _cubes.Remove(cube);
        cube.Destroying -= OnCubeDestroying;
    }
}
