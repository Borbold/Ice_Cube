using UnityEngine;

[RequireComponent(typeof(Collider))]
public class TrapHitCollider : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Cube cube))
        {
            if (cube.IsAlive)
                cube.KillbyShatter();
        }
    }
}
