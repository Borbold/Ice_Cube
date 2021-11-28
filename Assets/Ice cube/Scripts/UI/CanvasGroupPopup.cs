using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class CanvasGroupPopup : MonoBehaviour
{
    [SerializeField] private UiScreen _screen;

    [SerializeField] private float _time;
    [SerializeField] private bool _changeInteractions;

    private bool _active = false;
    private CanvasGroup _canvas;
    private Coroutine _changingAlpha;

    private void Awake()
    {
        _canvas = GetComponent<CanvasGroup>();
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
        _active = true;
        if (_changingAlpha != null)
            StopCoroutine(_changingAlpha);
        _changingAlpha = StartCoroutine(ChangeAlpha(1));
    }

    private void Hide()
    {
        _active = false;
        if (_changingAlpha != null)
            StopCoroutine(_changingAlpha);
        _changingAlpha = StartCoroutine(ChangeAlpha(0));
    }

    private IEnumerator ChangeAlpha(float target)
    {
        TrySetInteractions(false);
        var timer = 0f;
        var startAlpha = _canvas.alpha;
        while (timer < _time)
        {
            timer += Time.deltaTime;
            _canvas.alpha = Mathf.Lerp(startAlpha, target, timer / _time);
            yield return null;
        }
        TrySetInteractions(_active);
    }

    private void TrySetInteractions(bool isON)
    {
        if (_changeInteractions)
        {
            _canvas.interactable = isON;
            _canvas.blocksRaycasts = isON;
        }
    }
}
