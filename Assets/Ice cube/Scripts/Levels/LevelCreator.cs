using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelCreator : MonoBehaviour
{
    [SerializeField] private List<LevelGroup> _groups;
    [SerializeField] private PlayerSliderMover _player;

    private int _currentGroup;
    private int _currentLevelIndex;

    public event UnityAction<Level> LevelCreated;

    private void Awake()
    {
        _currentGroup = PlayerDataBase.GetGroup();
        _currentLevelIndex = PlayerDataBase.GetLevel();
    }

    private void Start()
    {
        var level = GetLevel(_currentGroup, _currentLevelIndex);
        StartNew(level);
        LevelCreated?.Invoke(level);
    }

    public List<int> GetCurrentGroupLevelNumbers(out int currentLevel)
    {
        var numbers = new List<int>();
        var globalIndex = 0;
        for (int i = 0; i<_groups.Count; i++)
        {
            if (i == _currentGroup)
            {
                for (int j = 0; j < _groups[i].Count; j++)
                {
                    numbers.Add(j + globalIndex);
                }
                currentLevel = globalIndex + _currentLevelIndex;
                return numbers;
            }
            else
            {
                globalIndex += _groups[i].Count;
            }
        }
        currentLevel = globalIndex + _currentLevelIndex;
        return numbers;
    }

    public int GetNextLevelGroup()
    {
        bool isLastNumber = _currentLevelIndex == _groups[_currentGroup].Count-1;
        int nextGroup = _currentGroup;
        if (isLastNumber)
            nextGroup++;
        if (nextGroup > _groups.Count - 1)
            nextGroup = 0;
        return nextGroup;
    }

    public int GetNextLevelNumber()
    {
        bool isLastNumber = _currentLevelIndex == _groups[_currentGroup].Count - 1;
        int nextNumber = _currentLevelIndex;
        if (isLastNumber)
            nextNumber = 0;
        else
            nextNumber++;
        return nextNumber;
    }
    private void StartNew(Level level)
    {
        Level newLevel = Instantiate(level, transform);
        newLevel.Init(_player);
    }

    private Level GetLevel(int groupIndex, int levelIndex)
    {
        return _groups[groupIndex].GetLevel(levelIndex);
    }
}
