using UnityEngine;

public class DirectionAlongSurface : MonoBehaviour
{
    [SerializeField] private Camera _followCamera;
    [SerializeField] private bool _onGround;

    private Vector3 _inputDirection;
    private Vector3 _currentNormal;
    private Vector3 _originSurfaceNormal;
    private float _minSlopeNormalY = 0.7f; // 0.7f it's 45 degrees

    public Vector3 InputDirection => _inputDirection;
    public float MinSlopeNormalY => _minSlopeNormalY;
    public bool OnGround => _onGround;
    public Vector3 OriginSurfaceNormal => _originSurfaceNormal;

    private void Update()
    {
        float horizontal = Input.GetAxisRaw(Axis.Horizontal);
        float vertical = Input.GetAxisRaw(Axis.Vertical);

        _inputDirection = Quaternion.Euler(0, _followCamera.transform.eulerAngles.y, 0) * new Vector3(horizontal, 0, vertical);
        _inputDirection = _inputDirection.normalized;
    }

    private void OnCollisionStay(Collision collision)
    {
        _originSurfaceNormal = collision.contacts[0].normal;
        _currentNormal = _originSurfaceNormal;
        _onGround = _currentNormal.y >= _minSlopeNormalY;
    }

    private void OnCollisionExit(Collision other)
    {
        _onGround = false;
        _currentNormal = Vector3.up;
    }

    public Vector3 GetDirection()
    {
        if (_currentNormal.y < _minSlopeNormalY)
            _currentNormal = Vector3.up;

        Vector3 directionAlongSurface = _inputDirection - Vector3.Dot(_inputDirection, _currentNormal) * _currentNormal;
        return directionAlongSurface;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(transform.position, transform.position + _currentNormal * 3);
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(transform.position, transform.position + GetDirection() * 2);
    }
#endif
}