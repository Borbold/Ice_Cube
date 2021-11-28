using UnityEngine;

public class AmplitudeEventHandler : MonoBehaviour
{
    [SerializeField] private Game _game;
    [SerializeField] private UI _ui;
    [SerializeField] private Money _money;

    private static AmplitudeSettings _instance;

    private AmplitudeSettings _amplitude;

    private void Awake()
    {
            _amplitude = FindObjectOfType<AmplitudeSettings>();
    }

    private void OnEnable()
    {
        _game.LevelStarted += OnLevelStart;
        _game.Lose += OnLevelLose;
        _game.Win += OnLevelWin;
        _ui.MainMenuEntered += OnMainMenu;
        _money.MoneyChanged += OnMoneyChanged;
    }

    private void OnDisable()
    {
        _game.LevelStarted -= OnLevelStart;
        _game.Lose -= OnLevelLose;
        _game.Win -= OnLevelWin;
        _ui.MainMenuEntered -= OnMainMenu;
        _money.MoneyChanged -= OnMoneyChanged;
    }

    private void OnLevelWin() => _amplitude.LevelWin();
    private void OnLevelLose() => _amplitude.LevelFail();
    private void OnLevelStart(int level) => _amplitude.LevelStart(level);

    private void OnMainMenu() => _amplitude.MainMenu();

    private void OnMoneyChanged(int money)
    {
        _amplitude.SetCurrentSoft(money);
    }
}
