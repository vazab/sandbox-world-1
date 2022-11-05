using UnityEngine;
using TMPro;

public class EnemyCounterStand : MonoBehaviour
{
    [SerializeField] private EnemySpawner _enemyspawner;
    [SerializeField] private EnemySpawningPlatform _enemySpawningPlatform;
    [SerializeField] private TMP_Text _text;

    private void OnEnable()
    {
        _enemyspawner.EnemyAmountChanged += OnEnemyAmountChanged;
    }

    private void OnDisable()
    {
        _enemyspawner.EnemyAmountChanged -= OnEnemyAmountChanged;
    }

    private void OnEnemyAmountChanged()
    {
        _text.text = $"Active enemy {_enemyspawner.CurrentAmount}/{_enemySpawningPlatform.MaxAmount}";
    }
}