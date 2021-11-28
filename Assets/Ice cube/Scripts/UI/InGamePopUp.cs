using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InGamePopUp : MonoBehaviour
{
    [SerializeField] private float _fadeTime;
    [SerializeField] private float _fadeStart;
    [SerializeField] private float _flySpeed;
    [SerializeField] private CanvasGroup _canvas;
    [SerializeField] private TMP_Text _text;

    private Transform _camera;
    private float _timer;

    private void Awake()
    {
        enabled = false;
    }

    public void Init(Transform camera, string text)
    {
        _timer = 0;
        _camera = camera;
        enabled = true;
        _text.text = text;
    }

    private void Update()
    {
        if (_camera != null)
            Work();
    }

    private void Work()
    {
        Move();
        Fade();
        if (_timer > _fadeStart + _fadeTime)
            Destroy(gameObject);
        _timer += Time.deltaTime;
    }

    private void Fade()
    {
        var timePassed = Mathf.Clamp((_timer - _fadeStart), 0, float.MaxValue);
        _canvas.alpha = 1 - (timePassed / _fadeTime);
    }

    private void Move()
    {
        transform.Translate(Vector3.up * Time.deltaTime * _flySpeed);
        transform.rotation = _camera.rotation;   
    }
}
