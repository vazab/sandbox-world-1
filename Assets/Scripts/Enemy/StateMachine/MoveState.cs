using UnityEngine;
using UnityEngine.AI;

public class MoveState : State
{
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private Animator _animator;

    private void OnEnable()
    {
        _animator.SetBool(EnemyAnimator.Params.Walk, true);
    }

    private void OnDisable()
    {
        _animator.SetBool(EnemyAnimator.Params.Walk, false);
    }

    private void Update()
    {
        if (Target != null)
            _navMeshAgent.destination = Target.transform.position;
    }
}