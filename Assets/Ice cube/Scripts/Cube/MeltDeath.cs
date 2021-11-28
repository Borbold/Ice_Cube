using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeltDeath : CubeDeathAnimation
{
    [SerializeField] private float _time;

    protected override void OnPlay()
    {
        StartCoroutine( Melt(_time));
    }

    private IEnumerator Melt(float time)
    {
        float timer = 0;
        Vector3 startScale = transform.localScale;
        while (timer < time)
        {
            timer += Time.deltaTime;
            transform.localScale = Vector3.Lerp(startScale, Vector3.zero, timer / time);
            yield return null;
        }
        End();
    }
}
