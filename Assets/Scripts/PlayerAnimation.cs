using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private MovementDirection _movementDirection;

    private void Update()
    {
        bool walking = _playerInput.Direction != Vector3.zero;
        _animator.SetBool(PlayerAnimator.Params.Walk, walking);

        if (_playerMovement.JustJumped)
        {
            _animator.SetTrigger(PlayerAnimator.Params.Jump);
        }

        _animator.SetBool(PlayerAnimator.Params.OnGround, _movementDirection.OnGround);
    }
}