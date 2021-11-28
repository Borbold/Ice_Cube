using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShatterPool : MonoBehaviour
{
    [SerializeField] private Fragments _template;
    [SerializeField] private int _baseSize;

    private List<Fragments> _items;

    private void Awake()
    {
        _items = new List<Fragments>();
        for (var i = 0; i < _baseSize; i++)
        {
            Fragments item = Instantiate(_template, transform);
            item.Hided += Add;
            Add(item);
        }
    }

    private void OnDestroy()
    {
        foreach (var item in _items)
        {
            item.Hided -= Add;
        }
    }

    public void PlayAt(Vector3 position)
    {
        Fragments item = _items[0];
        _items.Remove(item);
        item.transform.parent = null;
        item.transform.position = position;
        item.gameObject.SetActive(true);
        item.Activate();
    }

    private void Add(Fragments item)
    {
        item.gameObject.SetActive(false);
        item.Reset();
        item.transform.parent = transform;
        _items.Add(item);
    }
}
