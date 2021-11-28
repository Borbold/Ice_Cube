using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelsView : MonoBehaviour
{
    [SerializeField] private LevelCreator _level;
    [SerializeField] private LevelView _template;

    private void Start()
    {
        foreach (int i in _level.GetCurrentGroupLevelNumbers(out int currentLevel))
        {
            LevelView level =  Instantiate(_template, transform);
            level.Init(i+1);
            if (i == currentLevel)
                level.Select();
        }
    }
}
