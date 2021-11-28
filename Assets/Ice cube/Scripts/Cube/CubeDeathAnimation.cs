using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Cube))]
public abstract class CubeDeathAnimation : MonoBehaviour
{
    public event UnityAction Ended;

    public void Play()
    {
        OnPlay();
    }

    public void End()
    {
        Ended?.Invoke();
    }

    protected abstract void OnPlay();
}
