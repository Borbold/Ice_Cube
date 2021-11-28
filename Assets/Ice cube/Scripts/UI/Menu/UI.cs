using UnityEngine;
using UnityEngine.Events;

public class UI : MonoBehaviour
{
    [SerializeField] private Game _game;
    [Header("Screens")]
    [SerializeField] private UiScreen _inGame;
    [SerializeField] private UiScreen _mainMenu;
    [SerializeField] private UiScreen _lose;
    [SerializeField] private UiScreen _win;

    public event UnityAction MainMenuEntered;

    private void Start()
    {
        MoveToMainMenu();
    }

    private void OnEnable()
    {
        _game.Win += MoveToWin;
        _game.Lose += MoveToLose;
        _game.LevelStarted += OnLevelStarted;
    }

    private void OnDisable()
    {
        _game.Win -= MoveToWin;
        _game.Lose -= MoveToLose;
        _game.LevelStarted -= OnLevelStarted;
    }

    private void MoveToInGame()
    {
        _inGame.Show();
        _mainMenu.Hide();
        _lose.Hide();
        _win.Hide();
    }

    private void MoveToMainMenu()
    {
        _inGame.Hide();
        _mainMenu.Show();
        _lose.Hide();
        _win.Hide();
        MainMenuEntered?.Invoke();
    }

    private void MoveToLose()
    {
        _inGame.Hide();
        _mainMenu.Hide();
        _lose.Show();
        _win.Hide();
    }

    private void MoveToWin()
    {
        _inGame.Hide();
        _mainMenu.Hide();
        _lose.Hide();
        _win.Show();
    }

    private void OnLevelStarted(int level)
    {
        MoveToInGame();
    }
}
