using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPopUp : MonoBehaviour
{
    [SerializeField] private UiScreen _screen;

    private Renderer[] _renderers;

    private void Awake()
    {
        _renderers = GetComponentsInChildren<Renderer>();
        Hide();
    }

    private void OnEnable()
    {
        _screen.Showing += Show;
        _screen.Hiding += Hide;
    }

    private void OnDisable()
    {
        _screen.Showing -= Show;
        _screen.Hiding -= Hide;
    }

    private void Show()
    {
        foreach (var render in _renderers)
        {
            render.enabled = true;
        }
    }

    private void Hide()
    {
        foreach (var render in _renderers)
        {
            render.enabled = false;
        }
    }
}
