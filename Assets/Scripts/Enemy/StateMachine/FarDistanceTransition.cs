using UnityEngine;

public class FarDistanceTransition : Transition
{
    [SerializeField] private CloseDistanceTransition _closeDistanceTransition;

    private float _transitionRange;

    private void Start()
    {
        _transitionRange = _closeDistanceTransition.TransitionRange;
    }

    private void Update()
    {
        if (Target != null)
        {
            if (Vector3.Distance(transform.position, Target.transform.position) > _transitionRange)
                NeedTransit = true;
        }
    }
}