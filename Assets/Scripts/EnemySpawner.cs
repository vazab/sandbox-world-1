using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy _prefab;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Player _player;

    public event UnityAction EnemyAmountChanged;

    public int CurrentAmount { get; private set; }

    private void Start()
    {
        EnemyAmountChanged?.Invoke();
    }

    public void Spawn()
    {
        Enemy enemy = Instantiate(_prefab, _spawnPoint.position, Quaternion.identity, _spawnPoint);
        enemy.Init(_player);
        enemy.Dying += OnEnemyDying;
        CurrentAmount++;
        EnemyAmountChanged?.Invoke();
    }

    private void OnEnemyDying(Enemy enemy)
    {
        enemy.Dying -= OnEnemyDying;
        CurrentAmount--;
        EnemyAmountChanged?.Invoke();
    }
}