using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _reward;
    
    private Player _target;
    private float _currentHealth;

    public event UnityAction<Enemy> Dying;
    
    public Player Target => _target;
    public float CurrentHealth => _currentHealth;
    public float MaxHealth => _maxHealth;

    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    public void Init(Player target)
    {
        _target = target;
    }

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;

        if (_currentHealth <= 0)
        {
            Dying.Invoke(this);
            Destroy(gameObject);
        }
    }
}