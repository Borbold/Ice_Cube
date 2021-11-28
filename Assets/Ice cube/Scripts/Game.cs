using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    [SerializeField] private CubeGroup _group;
    [SerializeField] private LevelCreator _level;
    [SerializeField] private PlayerSliderMover _player;

    public State CurrentState  {get; private set;}

    public enum State
    {
        Menu,
        Level,
        Score
    }

    public event UnityAction Lose;
    public event UnityAction Win;
    public event UnityAction<int> LevelStarted;

    private void Awake()
    {
        CurrentState = State.Menu;
    }

    private void OnEnable()
    {
        _group.SizeChanged += OnGroupSizeChanged;
        _group.ScoreMultiplierEntered += OnScoreMultiplierEntered;
    }

    private void OnDisable()
    {
        _group.SizeChanged -= OnGroupSizeChanged;
        _group.ScoreMultiplierEntered -= OnScoreMultiplierEntered;
    }

    private void Start()
    {
        CubeMelt.TimeScale = 0;
    }

    public void StartNextLevel()
    {
        PlayerDataBase.SetGroup(_level.GetNextLevelGroup());
        PlayerDataBase.SetNumber(_level.GetNextLevelNumber());
        Restart();
    }

    public void Restart()
    {
        var scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void StartLevel()
    {
        _player.Activate();
        CubeMelt.TimeScale = 1f;
        _level.GetCurrentGroupLevelNumbers(out int currentLevel);
        LevelStarted?.Invoke(currentLevel);
        CurrentState = State.Level;
    }

    private void OnGroupSizeChanged(int size)
    {
        if (CurrentState == State.Level)
        {
            if (size == 0)
            {
                _player.Stop();
                Lose?.Invoke();
            }
        }
        if (CurrentState == State.Score)
        {
            if (size == 0)
            {
                _player.Stop();
                Win?.Invoke();
            }
        }
    }

    private void EnterScoreState()
    {
        CurrentState = State.Score;
    }

    private void OnScoreMultiplierEntered(ScoreMultiplier multiplier)
    {
        if (CurrentState == State.Level)
            EnterScoreState();
    }
}
