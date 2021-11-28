using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupEntrance : MonoBehaviour
{
    [SerializeField] ParticleSystem _effect;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Cube cube))
        {
            Instantiate(_effect, cube.transform.position, Quaternion.LookRotation(Vector3.up));
        }
    }
}
