using UnityEngine;
using TMPro;

public class ScoreView : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Score _score;

    private void OnEnable()
    {
        _score.ValueChanged += OnScoreChanged;
    }

    private void OnDisable()
    {
        _score.ValueChanged -= OnScoreChanged;
    }

    private void OnScoreChanged(int score)
    {
        _text.text = score.ToString();
    }

}
