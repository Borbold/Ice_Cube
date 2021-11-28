using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class LevelProgressView : MonoBehaviour
{
    [SerializeField] private LevelCreator _level;
    [SerializeField] private Transform _player;

    private Slider _slider;
    private Vector3 _levelEnd;

    private float _levelLenght => _levelEnd.magnitude;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    private void OnEnable()
    {
        _level.LevelCreated += OnLevelCreated;
    }

    private void OnDisable()
    {
        _level.LevelCreated -= OnLevelCreated;
    }

    private void OnLevelCreated(Level level)
    {
        _levelEnd = _player.transform.forward * level.Length + _player.transform.position;
    }

    private float DistanceLeft()
    {
        Vector3 target = Vector3.Project(_levelEnd, _player.transform.forward);
        var distance = Vector3.Distance(target, _player.transform.position);
        return distance;
    }

    private void Update()
    {
        float newValue = (_levelLenght - DistanceLeft()) / _levelLenght;
        if (newValue > _slider.value)
            _slider.value = Mathf.Lerp(_slider.value, newValue, 0.01f);
    }


}
