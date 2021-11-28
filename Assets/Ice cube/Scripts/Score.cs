using UnityEngine;
using UnityEngine.Events;

public class Score : MonoBehaviour
{
    [SerializeField] private CubeGroup _cubes;

    private int _value;
    private float _multiplier = 1;

    public int MultipliedScore => (int)(_value * _multiplier);

    public event UnityAction<int> MultipliedScoreChanged;
    public event UnityAction<int> ValueChanged;

    private void OnEnable()
    {
        _cubes.CupEntered += OnScoreGainded;
        _cubes.ScoreMultiplierEntered += OnScoreMultiplierEntered;
        ValueChanged += OnValueChanged;
    }

    private void OnDisable()
    {
        _cubes.CupEntered -= OnScoreGainded;
        _cubes.ScoreMultiplierEntered -= OnScoreMultiplierEntered;
        ValueChanged -= OnValueChanged;
    }

    private void OnScoreGainded()
    {
        _value++;
        ValueChanged?.Invoke(_value);
    }

    private void OnScoreMultiplierEntered(ScoreMultiplier multiplier)
    {
        _multiplier = multiplier.Value;
        MultipliedScoreChanged?.Invoke(MultipliedScore);
    }

    private void OnValueChanged(int value)
    {
        MultipliedScoreChanged?.Invoke(MultipliedScore);
    }
}
