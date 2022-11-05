using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private float _maxHealth;
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private List<Weapon> _weapons;

    private Weapon _currentWeapon;
    private float _currentHealth;

    public event UnityAction<float, float> HealthChanged;
    public event UnityAction PlayerDying;

    private void Start()
    {
        _currentHealth = _maxHealth;
        _currentWeapon = _weapons[0];
        HealthChanged?.Invoke(_currentHealth, _maxHealth);
    }

    private void Update()
    {
        if (_playerInput.Shoot)
        {
            _currentWeapon.Shoot();
        }
    }

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;
        HealthChanged?.Invoke(_currentHealth, _maxHealth);

        if (_currentHealth <= 0)
        {
            PlayerDying?.Invoke();
            Destroy(gameObject);
        }
    }
}