using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 4f;
    [SerializeField] private float _jumpForce = 5f;
    [SerializeField] private float _slidingDownSpeed = 5f;
    [SerializeField] private MovementDirection _movementDirection;
    [SerializeField] private PlayerInput _playerInput;

    private Rigidbody _rigidbody;
    
    public bool JustJumped { get; private set; } = false;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.freezeRotation = true;
    }

    private void FixedUpdate()
    {
        SlideDownAlongSlope();
        Move();

        if (JustJumped)
            Jump();
    }

    private void Update()
    {
        if (JustJumped == false && _movementDirection.OnGround && _playerInput.Jump)
            JustJumped = true;
    }

    private void Move()
    {
        Vector3 offset = _movementDirection.GetFinalDirection() * _speed * Time.deltaTime;
        _rigidbody.MovePosition(_rigidbody.position + offset);
    }

    private void Jump()
    {
        JustJumped = false;
        _rigidbody.AddForce(transform.up * _jumpForce, ForceMode.Impulse);
    }

    private void SlideDownAlongSlope()
    {
        Vector3 normal = _movementDirection.OriginSurfaceNormal;
        if (normal.y < _movementDirection.MinSlopeNormalY && normal.y > 0)
        {
            Vector3 offset = new Vector3(normal.x, -normal.y, normal.z) * _slidingDownSpeed * Time.deltaTime;
            _rigidbody.MovePosition(_rigidbody.position + offset);
        }
    }
}