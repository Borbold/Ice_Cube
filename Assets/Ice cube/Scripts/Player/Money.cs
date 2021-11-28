using UnityEngine;
using UnityEngine.Events;

public class Money : MonoBehaviour
{
    [SerializeField] private Game _game;
    [SerializeField] private Score _score;

    private int _amount;

    public int Amount => _amount;

    public event UnityAction<int> MoneyChanged;

    private void Awake()
    {
        _amount = PlayerDataBase.GetMoney();
        MoneyChanged?.Invoke(_amount);
    }

    private void OnEnable()
    {
        _game.Win += OnWin;
    }


    private void OnDisable()
    {
        _game.Win -= OnWin;
    }

    private void Add(int amount)
    {
        if (amount < 0)
            throw new System.Exception("Amount cant be negative");
        _amount += amount;
        PlayerDataBase.SetMoney(_amount);
        MoneyChanged?.Invoke(amount);
    }

    private void OnWin()
    {
        Add(_score.MultipliedScore);
    }
}
