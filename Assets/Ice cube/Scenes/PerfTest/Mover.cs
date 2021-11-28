using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    private void Update()
    {
        transform.position += Vector3.forward * Time.deltaTime * 7;
    }
}
