using UnityEngine;
using UnityEngine.Events;

public class Cube : MonoBehaviour
{
    [SerializeField] private CubeDeathAnimation _melt;
    [SerializeField] private CubeDeathAnimation _shatter;

    public event UnityAction<Cube> Destroyed;
    public event UnityAction<Cube> Destroying;
    public event UnityAction<Cube> EnteredCup;
    public event UnityAction<ScoreMultiplier> ScoreMultiplierEntered;

    public bool IsAlive { get; private set; } = true;

    private void OnEnable()
    {
        _melt.Ended += OnDeath;
        _shatter.Ended += OnDeath;
    }

    private void OnDisable()
    {
        _melt.Ended -= OnDeath;
        _shatter.Ended -= OnDeath;
    }

    public void Reset()
    {
        IsAlive = true;
    }

    public void KillbyMelt()
    {
        IsAlive = false;
        Destroying?.Invoke(this);
        _melt.Play();
    }

    public void KillbyShatter()
    {
        IsAlive = false;
        Destroying?.Invoke(this);
        _shatter.Play();
    }

    private void OnDeath()
    {
        Destroyed?.Invoke(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out CupEntrance cup))
        {
            EnteredCup?.Invoke(this);
        }
        if (other.TryGetComponent(out ScoreMultiplier multiplier))
        {
           ScoreMultiplierEntered?.Invoke( multiplier);
        }
    }   
}
