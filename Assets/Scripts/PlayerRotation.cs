using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    [SerializeField] private MovementDirection _movementDirection;

    private float _rotationSpeed = 10f;

    private void FixedUpdate()
    {
        Rotate(_movementDirection.GetDirectionModifiedByCamera());
    }

    private void Rotate(Vector3 inputDirection)
    {
        if (inputDirection != Vector3.zero)
        {
            Quaternion desiredRotation = Quaternion.LookRotation(inputDirection, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, _rotationSpeed * Time.deltaTime);
        }
    }
}