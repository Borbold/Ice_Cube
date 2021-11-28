using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeReset : MonoBehaviour
{
    [SerializeField] private Cube _cube;
    [SerializeField] private CubeMelt _melt;
    [SerializeField] private CubeJump _jump;
    [SerializeField] private CubeSpin _spin;


    public void Reset()
    {
        _cube.Reset();
        _melt.Reset();
        _jump.Reset();
        _spin.Reset();
    }
}
