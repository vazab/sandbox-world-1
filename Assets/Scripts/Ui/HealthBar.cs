using UnityEngine;

public class HealthBar : Bar
{
    [SerializeField] private Player _player;

    private void OnEnable()
    {
        _player.HealthChanged += OnValueChange;
    }

    private void OnDisable()
    {
        _player.HealthChanged -= OnValueChange;
    }
}