using UnityEngine;
using UnityEngine.Events;

public class UiScreen : MonoBehaviour
{
    public event UnityAction Showing;
    public event UnityAction Hiding;

    public void Show()
    {
        Showing?.Invoke();
    }

    public void Hide()
    {
        Hiding?.Invoke();
    }
}
