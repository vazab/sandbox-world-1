using System.Collections;
using UnityEngine;

public class EnemySpawningPlatform : MonoBehaviour
{
    [SerializeField] private int _maxAmount;
    [SerializeField] private float _delayTime;
    [SerializeField] private EnemySpawner _enemySpawner;

    private Coroutine _spawnRoutine;

    public int MaxAmount => _maxAmount;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent<Player>(out Player player))
        {
            _spawnRoutine = StartCoroutine(Spawn());
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.TryGetComponent<Player>(out Player player))
        {
            if (_spawnRoutine != null)
                StopCoroutine(_spawnRoutine);
        }
    }

    private IEnumerator Spawn()
    {
        WaitForSeconds waitSeconds = new WaitForSeconds(_delayTime);

        while (_enemySpawner.CurrentAmount < _maxAmount)
        {
            _enemySpawner.Spawn();
            yield return waitSeconds;
        }
    }
}