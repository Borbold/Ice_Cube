using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpActivator : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private CubeJump _jump;

    private void OnEnable()
    {
        _player.Jumping += OnPlayerJump;
    }

    private void OnDisable()
    {
        _player.Jumping -= OnPlayerJump;
    }

    private void OnPlayerJump(float height, float time)
    {
        _jump.Jump(time, height);
    }
}
