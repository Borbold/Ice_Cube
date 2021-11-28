using UnityEngine;
using TMPro;

public class MoneyView : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Money _money;

    private void Start()
    {
        _text.text = _money.Amount.ToString();
    }
}
