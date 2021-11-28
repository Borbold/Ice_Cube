using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trail : MonoBehaviour
{
    [SerializeField] private ParticleSystem _trail;
    [SerializeField] private Formation _formation;
    [SerializeField] private Player _player;
    [Header("Settings")]
    [SerializeField] private float _shapeSizeMult;
    [SerializeField] private float _startSizeMult;

    private ParticleSystem.ShapeModule _shape;
    private ParticleSystem.MainModule _main;

    private void Awake()
    {
        _shape = _trail.shape;
        _main = _trail.main;
    }

    private void OnEnable()
    {
        _formation.ActiveRangeChanged += OnRangeChanged;
        _player.Jumping += OnPlayerJimping;
    }

    private void OnDisable()
    {
        _formation.ActiveRangeChanged -= OnRangeChanged;
        _player.Jumping -= OnPlayerJimping;
    }

    private void OnRangeChanged(Vector2 sizes)
    {
        float size = (-sizes.x + sizes.y) / 2;
        _shape.radius = size * _shapeSizeMult;
        _main.startSizeMultiplier = size * _startSizeMult;
    }

    private void OnPlayerJimping(float height, float time)
    {
        Pause(time);
        _main.gravityModifier = 0.1f;
    }

    private void Pause(float time)
    {
        //_trail.Pause();
        Invoke(nameof(Resume), time);
    }

    private void Resume()
    {
        //_trail.Play();
        _main.gravityModifier = 0f;
    }
}
