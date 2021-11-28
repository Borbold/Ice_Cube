using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormationCircleGenerator : MonoBehaviour
{
    [SerializeField] private float _positionRadius;
    [SerializeField] private int _rawsAmount;

    public float PositionRadius => _positionRadius;

    private int CalculateRawSize(float rawRadius)
    {
        if (rawRadius <= _positionRadius)
            return 1;
        var angle = 2 * Mathf.Asin(_positionRadius / rawRadius) * Mathf.Rad2Deg;
        var amount = 360 / angle;
        return (int)amount;
    }

    public List<FormationPoint> Build(Transform parent)
    {
        var formation = new List<FormationPoint>();
        var holder = new GameObject("Formation").transform;
        for(var i=0;  i< _rawsAmount; i++)
        {
            var radius = i * _positionRadius * 2;
            var rawSize = CalculateRawSize(radius);
            float angleSection = Mathf.PI * 2f / rawSize;
            for (int j = 0; j < rawSize; j++)
            {
                float angle = j * angleSection;
                var position = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * radius;
                var newPoint = CreatePoint(position, holder, "raw " + i.ToString() + " / number " + j.ToString());
                newPoint.Init(i, FindNearestPoint(formation, newPoint.transform.position, i - 1));
                formation.Add(newPoint);
            }
        }
        holder.position = parent.position;
        holder.rotation = parent.rotation;
        foreach (var point in formation)
            point.transform.parent = parent;
        Destroy(holder.gameObject);
        return formation;
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

    private FormationPoint FindNearestPoint(List<FormationPoint> points ,Vector3 position, int raw)
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
