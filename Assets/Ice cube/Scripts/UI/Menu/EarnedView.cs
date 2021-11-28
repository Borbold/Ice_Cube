using UnityEngine;
using TMPro;

public class EarnedView : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Score _score;

    private void OnEnable()
    {
        _score.MultipliedScoreChanged += OnMultipliedScoreChanged;
    }

    private void OnDisable()
    {
        _score.MultipliedScoreChanged -= OnMultipliedScoreChanged;
    }

    private void OnMultipliedScoreChanged(int value)
    {
        _text.text = "+" + value.ToString();
    }
}
