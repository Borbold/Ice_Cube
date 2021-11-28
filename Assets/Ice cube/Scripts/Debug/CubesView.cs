using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CubesView : MonoBehaviour
{

    [SerializeField] private TMP_Text _text;
    [SerializeField] private CubeGroup _cubes;

    private void Awake()
    {
        _cubes.SizeChanged += OnChange;
    }

    private void OnChange(int amount)
    {
        _text.text = amount.ToString();
    }
}
