using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelGroup", menuName = "NewLevelGroup", order = 51)]
public class LevelGroup : ScriptableObject
{
    [SerializeField] private List<Level> _levels;

    public int Count => _levels.Count;

    public Level GetLevel(int index)
    {
        return _levels[index];
    }
}
