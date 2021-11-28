using UnityEngine;

public class TrapAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] [Range(0, 1)] private float _hitEventTime;

    private const string _barHitAnimationName = "FBX_Temp";

    private float _duration;
    private PlayerSliderMover _player;

    private void Awake()
    {
        var animatorOverrideController = new AnimatorOverrideController(_animator.runtimeAnimatorController);
        _animator.runtimeAnimatorController = animatorOverrideController;
        _duration = animatorOverrideController[_barHitAnimationName].length;
        _animator.speed = 0;
    }

    private void OnEnable()
    {
        if (_player != null)
            _player.Activated += StartPlay;
    }

    private void OnDisable()
    {
        if (_player != null)
            _player.Activated -= StartPlay;
    }

    public void Init(PlayerSliderMover player)
    {
        _player = player;
        _player.Activated += StartPlay;
    }



    private void Resume()
    {
        _animator.speed = 1;
    }

    private void StartPlay()
    {
        if (_player != null)
        {
            float arrival = CalculatePlayerArrivalTime(_player);
            float animationSynhDelta = (arrival + _hitEventTime * _duration) % _duration;
            Invoke(nameof(Resume), animationSynhDelta);
        }
    }
    private float CalculatePlayerArrivalTime(PlayerSliderMover player)
    {
        var moveVector = Vector3.Project(player.transform.position - transform.position, _player.transform.forward);
        var distance = moveVector.magnitude;
        return distance / player.ForwardSpeed;
    }
}
