using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private float _length;

    public float Length => _length;

    public void Init(PlayerSliderMover player)
    {
        foreach (var trap in FindObjectsOfType<TrapAnimator>())
        {
            trap.Init(player);
        }
    }
}
