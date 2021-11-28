using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CubeGroup : MonoBehaviour
{
    [SerializeField] private CubePool _pool;
    [SerializeField] private Player _player;

    private List<Cube> _cubes;

    public int Size => _cubes.Count;

    public event UnityAction<Cube> CubeRemoved;
    public event UnityAction<Cube> CubeAdded;
    public event UnityAction<int> CubesCreated;
    public event UnityAction<int> SizeChanged;
    public event UnityAction CupEntered;
    public event UnityAction<ScoreMultiplier> ScoreMultiplierEntered;

    private void Awake()
    {
        _cubes = new List<Cube>();
    }

    private void OnEnable()
    {
        _player.Jumping += OnPlayerJumping;
        _player.GateHit += OnGateHit;
    }

    private void OnDisable()
    {
        _player.Jumping -= OnPlayerJumping;
        _player.GateHit -= OnGateHit;
    }

    public void Add(Cube cube)
    {
        cube.Destroying += OnCubeDestroying;
        cube.EnteredCup += OnEnteredCup;
        cube.ScoreMultiplierEntered += OnScoreMultiplierEntered;
        _cubes.Add(cube);
        CubeAdded?.Invoke(cube);
        SizeChanged?.Invoke(Size);
    }

    public void Remove(Cube cube)
    {
        cube.Destroying -= OnCubeDestroying;
        cube.EnteredCup -= OnEnteredCup;
        cube.ScoreMultiplierEntered -= OnScoreMultiplierEntered;
        _cubes.Remove(cube);
        CubeRemoved?.Invoke(cube);
        SizeChanged?.Invoke(Size);
    }

    public void AddNew(int amount = 1)
    {
        CubesCreated?.Invoke(amount);
        for (var i = 0; i < amount; i++)
        {
            Add(_pool.Take());
        }
    }

    public void KillOldest()
    {
        var cube = _cubes[0];
        cube.KillbyMelt();
    }

    public void KillOldest(int amount)
    {
        for (var i = 0; i < amount; i++)
        {
            if (Size > 0)
                KillOldest();
        }
    }

    private void OnCubeDestroying(Cube cube)
    {
        Remove(cube);
    }

    private void OnPlayerJumping(float height, float time)
    {
            foreach (var cube in _cubes)
            {
                cube.GetComponent<CubeJump>().Jump(time, height);
            }
    }

    private void OnEnteredCup(Cube cube)
    {
        cube.KillbyMelt();
        CupEntered?.Invoke();
    }

    private void OnGateHit(Gate gate)
    {
        var groupChange = gate.CalculateAdditionAmount(Size);
        if (groupChange > 0)
            AddNew(groupChange);
        else
            KillOldest(Mathf.Abs(groupChange));
    }

    private void OnScoreMultiplierEntered(ScoreMultiplier multiplier)
    {
        KillOldest(multiplier.CubeCost);
        ScoreMultiplierEntered?.Invoke(multiplier);

    }
}
