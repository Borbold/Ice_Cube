using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpLine : MonoBehaviour
{
    [SerializeField] private GirlAnimator _girl;
    [SerializeField] private float _jumpHeight;
    [SerializeField] private float _jumpTime;

    private Player _player;

    private void OnEnable()
    {
        _girl.Hit += OnGirlHit;
    }

    private void OnDisable()
    {
        _girl.Hit -= OnGirlHit;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            _player = player;
            _girl.DoHit();
        }
    }

    private void OnGirlHit()
    {
        if (_player != null)
        {
            _player.Jump(_jumpHeight, _jumpTime);
            _player = null;
        }
    }
}
