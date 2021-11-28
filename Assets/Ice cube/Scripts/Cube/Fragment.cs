using System.Collections;
using UnityEngine;

public class Fragment : MonoBehaviour
{
    private Rigidbody _rigibody;
    private Collider _collider;

    private Vector3 _baseScale;
    private Vector3 _baseOffset;


    private void Awake()
    {
        _rigibody = GetComponent<Rigidbody>();
        _rigibody.isKinematic = true;
        _collider = GetComponent<Collider>();
        _collider.enabled = false;
        _baseScale = transform.localScale;
        _baseOffset = transform.position - transform.parent.position;
    }

    public void Hide(float time)
    {
        StartCoroutine(ScaleToO(time));
    }

    public void Reset()
    {
        _rigibody.isKinematic = true;
        _collider.enabled = false;
        transform.localPosition = _baseScale;
        transform.position = transform.parent.position + _baseOffset;
    }

    public void Activate()
    {
        _rigibody.isKinematic = false;
        _collider.enabled = true;
    }

    private IEnumerator ScaleToO(float time)
    {
        float timer = 0;
        Vector3 startScale = transform.localScale;
        while (timer < time)
        {
            timer += Time.deltaTime;
            transform.localScale = Vector3.Lerp(startScale, Vector3.zero, timer / time);
            yield return null;
        }
    }
}
