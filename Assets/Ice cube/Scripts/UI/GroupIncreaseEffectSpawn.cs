using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupIncreaseEffectSpawn : MonoBehaviour
{
    [SerializeField] private CubeGroup _cubes;
    [SerializeField] private InGamePopUp _effect;
    [SerializeField] private Game _game;

    private void OnEnable()
    {
        _cubes.CubesCreated += Spawn;
    }

    private void OnDisable()
    {
        _cubes.CubesCreated -= Spawn;
    }

    private void Spawn(int increaseAmount)
    {
        if (increaseAmount <= 0)
            return;
        if (_game.CurrentState == Game.State.Menu)
            return;
        var effect = Instantiate(_effect, transform.position, Quaternion.identity);
        string text = "+" + increaseAmount.ToString();
        effect.Init(Camera.main.transform, text);
    }
}
