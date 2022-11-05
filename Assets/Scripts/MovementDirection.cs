using UnityEngine;

public class MovementDirection : MonoBehaviour
{
    [SerializeField] private Camera _followCamera;
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private CapsuleCollider _capsuleCollider;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _minValueForSlidingAlongObstacle = 0.8f;

    private bool _onGround;
    private RaycastHit _hitInfo;
    private Vector3 _currentNormal;
    private Vector3 _originSurfaceNormal;
    private float _minSlopeNormalY = 0.7f; // 0.7f it's 45 degrees

    public bool OnGround => _onGround;
    public Vector3 OriginSurfaceNormal => _originSurfaceNormal;
    public float MinSlopeNormalY => _minSlopeNormalY;
   
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

    public Vector3 GetFinalDirection()
    {
        Vector3 currentDirection = GetDirectionAlongSurface();

        if (CheckObstacle())
        {
            Vector3 directionOption1 = new Vector3(_hitInfo.normal.z, 0, -_hitInfo.normal.x).normalized;
            Vector3 directionOption2 = -directionOption1;

            float distanceToOption1 = (directionOption1 - currentDirection).magnitude;
            float distanceToOption2 = (directionOption2 - currentDirection).magnitude;
            float deltaOfDistances = Mathf.Abs(distanceToOption1 - distanceToOption2);

            if (deltaOfDistances <= _minValueForSlidingAlongObstacle)
                return Vector3.zero;
            else if (distanceToOption1 < distanceToOption2)
                return directionOption1;
            else
                return directionOption2;
        }

        return currentDirection;
    }

    public Vector3 GetDirectionModifiedByCamera()
    {
        Vector3 newDirection = Quaternion.Euler(0, _followCamera.transform.eulerAngles.y, 0) * _playerInput.Direction;
        newDirection = newDirection.normalized;
        return newDirection;
    }

    private Vector3 GetDirectionAlongSurface()
    {
        if (_currentNormal.y < _minSlopeNormalY)
            _currentNormal = Vector3.up;

        Vector3 inputDirection = GetDirectionModifiedByCamera();
        Vector3 directionAlongSurface = inputDirection - Vector3.Dot(inputDirection, _currentNormal) * _currentNormal;
        return directionAlongSurface;
    }

    private bool CheckObstacle()
    {
        float castDistance = 0.55f;

        float boxCastScaleX = _capsuleCollider.radius;
        float boxCastScaleY = _capsuleCollider.height * 0.5f - 0.025f;
        float boxCastScaleZ = 0.1f;
        Vector3 boxCastSize = new Vector3(boxCastScaleX, boxCastScaleY, boxCastScaleZ);
        Vector3 boxCastCenter = transform.position + (Vector3.up * 0.025f);


        bool hit = Physics.BoxCast(boxCastCenter, boxCastSize, GetDirectionAlongSurface(), out _hitInfo, transform.rotation, castDistance, _layerMask);
        BoxCastDebug.DrawBoxCastOnHit(boxCastCenter, boxCastSize, GetDirectionAlongSurface(), transform.rotation, castDistance, Color.green);

        bool isObstacle = false;

        if (hit)
        {
            if (_hitInfo.normal.y < _minSlopeNormalY)
                isObstacle = true;
        }

        return isObstacle;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(transform.position, transform.position + _currentNormal * 3);
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(transform.position, transform.position + GetDirectionAlongSurface() * 2);
    }
#endif
}