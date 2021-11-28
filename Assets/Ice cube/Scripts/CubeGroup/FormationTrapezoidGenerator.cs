using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormationTrapezoidGenerator : MonoBehaviour
{
    [SerializeField] private float _positionStep;
    [SerializeField] private int _rawsAmount;
    [Range(0, 1)]
    [SerializeField] private float _frontSize;
    [Range(0, 1)]
    [SerializeField] private float _backSize;

    public float PositionRadius => _positionStep;

    private int CalculateRawSize(float rawRadius)
    {
        if (rawRadius <= _positionStep)
            return 1;
        var angle = 2 * Mathf.Asin(_positionStep / rawRadius) * Mathf.Rad2Deg;
        var amount = 360 / angle;
        return (int)amount;
    }

    public List<FormationPoint> Build(Transform parent)
    {
        var formation = new List<FormationPoint>();
        List<Vector3> takenPositions = new List<Vector3>();
        var holder = new GameObject("Formation").transform;
        for (var i = 0; i < _rawsAmount; i++)
        {
            int maxSize = CalculateRawSize(i);
            for (var raw = 0; raw < maxSize; raw++)
                for (var column = 0; column < maxSize; column++)
                {
                    if (IsPositionIsnsideForm(column, raw, maxSize))
                    {
                        var position = CalculatePosition(raw, column, maxSize);
                        if (takenPositions.Contains(position) == false)
                        {
                            var newPoint = CreatePoint(position, holder, "raw " + i.ToString() + " / number " + raw.ToString() + "/" + column.ToString());
                            newPoint.Init(i, FindNearestPoint(formation, newPoint.transform.position, i - 1));
                            formation.Add(newPoint);
                            takenPositions.Add(position);
                        }
                        
                    }
                }

        }
        holder.position = parent.position;
        holder.rotation = parent.rotation;
        foreach (var point in formation)
            point.transform.parent = parent;
        Destroy(holder.gameObject);
        return formation;
    }

    private int CalculateRawSize(int raw) => 2 * raw + 1;

    private Vector3 CalculatePosition(int raw, int column, int maxSize)
    {
        int center = maxSize / 2;
        float x = (column - center) * _positionStep;
        float z = (raw - center) * _positionStep;
        return new Vector3(x, 0, z);
    }

    private bool IsPositionIsnsideForm(int column, int rawNumber, int maxRaws)
    {
        int indexOffset = Mathf.RoundToInt((1-Mathf.Lerp(_backSize, _frontSize, (float)rawNumber / maxRaws)) / 2 * maxRaws);
        int min = indexOffset;
        int max = maxRaws - indexOffset;
        return column >= min && column <= max;
    }

    private FormationPoint CreatePoint(Vector3 position, Transform parent, string name)
    {
        var newObject = new GameObject();
        var newPoint = newObject.AddComponent<FormationPoint>();
        newPoint.gameObject.AddComponent<PointRotation>();
        newPoint.transform.position = position;
        newPoint.transform.parent = parent;
        newPoint.gameObject.name = name;
        return newPoint;
    }

    private FormationPoint FindNearestPoint(List<FormationPoint> points, Vector3 position, int raw)
    {
        if (points.Count == 0)
            return null;
        FormationPoint nearestPoint = null;
        foreach (var point in points)
        {
            var list = new List<Vector3>();
            if (point.Raw == raw)
            {
                if (nearestPoint == null)
                {
                    nearestPoint = point;
                }
                else
                {
                    float nearest = Vector3.Distance(position, nearestPoint.transform.position);
                    float current = Vector3.Distance(point.transform.position, position);
                    if (nearest > current)
                    {
                        nearestPoint = point;
                    }
                }
            }
        }
        return nearestPoint;
    }
}
