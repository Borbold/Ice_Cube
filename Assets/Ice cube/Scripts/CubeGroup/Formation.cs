using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Formation : MonoBehaviour
{
    [SerializeField] private FormationTrapezoidGenerator _generator;
    [SerializeField] private CubeGroup _group;

    private List<FormationPoint> _allPoints;

    public Vector2 ActiveRange { get; private set; }

    public event UnityAction<Vector2> ActiveRangeChanged;

    private void Awake()
    {
        _allPoints = _generator.Build(transform);
    }

    private void OnEnable()
    {
        _group.CubeAdded += Add;
        _group.CubeRemoved += Remove;
        _group.SizeChanged += OnGroupSizeChanged;
    }

    private void OnDisable()
    {
        _group.CubeAdded -= Add;
        _group.CubeRemoved -= Remove;
        _group.SizeChanged -= OnGroupSizeChanged;
    }

    private void Add(Cube cube)
    {
        var point = FreePoint();
        point.Follower = cube;
        cube.transform.parent = point.transform;
    }

    private void Remove(Cube cube)
    {
        var point = FindPoint(cube);
        if(point!=null)
            point.Follower = null;
        cube.transform.parent = null;
    }

    private FormationPoint FreePoint()
    {
        foreach (var point in _allPoints)
        {
            if (point.IsBysy == false)
            {
                return point;
            }
        }
        return null;
    }

    private Vector2 CalculateRange()
    {
        float left = 0;
        float right = 0;
        foreach (FormationPoint point in _allPoints)
        {
            if (point.IsBysy)
            {
                float xOffset = point.transform.position.x - transform.position.x;
                if (xOffset < left)
                    left = xOffset;
                if (xOffset > right)
                    right = xOffset;
            }
        }
        return new Vector2(left, right);
    }

    private FormationPoint FindPoint(Cube cube)
    {
        foreach (var point in _allPoints)
        {
            if (point.Follower == cube)
            {
                return point;
            }
        }
        return null;
    }

    private void OnGroupSizeChanged(int size)
    {
        ActiveRange = CalculateRange();
        ActiveRangeChanged?.Invoke(ActiveRange);
    }

}
