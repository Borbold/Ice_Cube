using UnityEngine;

[RequireComponent(typeof(FormationPoint))]
public class PointRotation : MonoBehaviour
{
    private Vector3 _lastPosition;
    private FormationPoint _point;

    private void Awake()
    {
        _point = GetComponent<FormationPoint>();
        _lastPosition = transform.position;
    }

    private void Update()
    {
        if (_point.IsBysy)
        {
            Vector3 movement = transform.position - _lastPosition;
            if (movement != Vector3.zero)
            {
                var targetRotation = Quaternion.Lerp(_point.Follower.transform.rotation, Quaternion.LookRotation(movement), 0.1f);
                _point.Follower.transform.rotation = targetRotation;
            }
            _lastPosition = transform.position;
        }
    }
}
