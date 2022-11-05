using UnityEngine;
using UnityEngine.AI;

public class AttackState : State
{
    [SerializeField] private float _damage;
    [SerializeField] private float _delay;
    [SerializeField] private Animator _animator;
    [SerializeField] private NavMeshAgent _navMeshAgent;

    private float _lastAttackTime;

    private void OnEnable()
    {
        _navMeshAgent.ResetPath();
        _lastAttackTime = 0;
    }

    private void Update()
    {
        if (_lastAttackTime <= 0)
        {
            Attack();
            _lastAttackTime = _delay;
        }

        _lastAttackTime -= Time.deltaTime;
    }

    private void Attack()
    {
        _animator.SetTrigger(EnemyAnimator.Params.Attack);
        Target.TakeDamage(_damage);
    }
}