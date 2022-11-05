using UnityEngine;

public class DirectionAlongSurfaceAndObstacle : MonoBehaviour
{
    [SerializeField] private DirectionAlongSurface _directionAlongSurface;
    [SerializeField] private CapsuleCollider _capsuleCollider;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _minValueForSlidingAlongObstacle = 0.8f;

    private RaycastHit _hitInfo;

    public Vector3 GetDirection()
    {
        float castDistance = 0.55f;

        float boxCastScaleX = _capsuleCollider.radius;
        float boxCastScaleY = _capsuleCollider.height * 0.5f - 0.025f;
        float boxCastScaleZ = 0.1f;
        Vector3 boxCastSize = new Vector3(boxCastScaleX, boxCastScaleY, boxCastScaleZ);
        Vector3 boxCastCenter = transform.position + (Vector3.up * 0.025f);

        bool hit = Physics.BoxCast(boxCastCenter, boxCastSize, _directionAlongSurface.GetDirection(), out _hitInfo, transform.rotation, castDistance, _layerMask);
        BoxCastDebug.DrawBoxCastOnHit(boxCastCenter, boxCastSize, _directionAlongSurface.GetDirection(), transform.rotation, castDistance, Color.green);

        if (hit)
        {
            if (_hitInfo.normal.y < _directionAlongSurface.MinSlopeNormalY)
            {
                Vector3 directionOption1 = new Vector3(_hitInfo.normal.z, 0, -_hitInfo.normal.x).normalized;
                Vector3 directionOption2 = -directionOption1;

                float distanceToOption1 = (directionOption1 - _directionAlongSurface.GetDirection()).magnitude;
                float distanceToOption2 = (directionOption2 - _directionAlongSurface.GetDirection()).magnitude;
                float deltaOfDistances = Mathf.Abs(distanceToOption1 - distanceToOption2);

                if (deltaOfDistances <= _minValueForSlidingAlongObstacle)
                    return Vector3.zero;
                else if (distanceToOption1 < distanceToOption2)
                    return directionOption1;
                else
                    return directionOption2;
            }
        }

        return _directionAlongSurface.GetDirection();
    }
}