using UnityEngine;
using TMPro;

public class MultiplierView : MonoBehaviour
{
    [SerializeField] private ScoreMultiplier _multiplier;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private MeshRenderer _renderer;
    [SerializeField] private Material _off;
    [SerializeField] private Material _on;

    private void Awake()
    {
        _text.text = "X" + _multiplier.Value.ToString();
        _renderer.material = _off;
    }

    private void OnEnable()
    {
        _multiplier.Reached += OnMultiplierReached;
    }

    private void OnDisable()
    {
        _multiplier.Reached += OnMultiplierReached;
    }

    private void OnValidate()
    {
        _text.text = "X" + _multiplier.Value.ToString();
        _renderer.material = _off;
    }

    private void OnMultiplierReached()
    {
        _renderer.material = _on;
    }
}
