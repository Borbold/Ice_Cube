using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class FPSView : MonoBehaviour
{
    private const int _framesPool = 5;

    [SerializeField] private TMP_Text _text;

    private List<float> _frames;

    private float FPS => (int)(_frames.Count/_frames.Sum());

    private void Awake()
    {
        //QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 500;
        _frames = new List<float>();
    }

    private void Update()
    {
        UpdateFPS();
        _text.text = FPS.ToString();
    }

    private void UpdateFPS()
    {
        float current = Time.unscaledDeltaTime;
        if (current > 0.1f)
            Debug.Log("Freeze " + current);
        _frames.Add(current);
        if (_frames.Count > _framesPool)
        {
            _frames.RemoveAt(0);
        }
    }
}
