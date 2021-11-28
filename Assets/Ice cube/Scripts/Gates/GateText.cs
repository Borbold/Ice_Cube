using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class GateText : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Gate _calculator;

    private void Awake()
    {
        _text.text = _calculator.TypeName + _calculator.Value.ToString();
    }
}
