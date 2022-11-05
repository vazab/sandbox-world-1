using UnityEngine;
using UnityEngine.Events;

public class Speedometer : MonoBehaviour
{
    private float _speed;
    private float _maxSpeed;
    private float _resetTime = 0.5f;
    private float _movementPerFrame;
    private Vector3 _oldPosition;
    private float _timer;

    public event UnityAction<float> MaxSpeedChanged;

    private void Start()
    {
        _oldPosition = transform.position;
    }

    private void FixedUpdate()
    {
        _movementPerFrame = Vector3.Distance(_oldPosition, transform.position);
        _speed = _movementPerFrame / Time.deltaTime;
        _oldPosition = transform.position;
    }

    private void Update()
    {
        _timer = _speed == 0f ? _timer + Time.deltaTime : 0f;

        if (_speed > _maxSpeed)
        {
            _maxSpeed = _speed;
            MaxSpeedChanged?.Invoke(_maxSpeed);
        }

        if (_timer >= _resetTime)
        {
            _maxSpeed = 0;
            MaxSpeedChanged?.Invoke(_maxSpeed);
        }
    }
}