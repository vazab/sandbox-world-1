using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    [SerializeField] private Transform _followTarget;
    [SerializeField] private float _distanceFromTarget = 4f;
    [SerializeField] private float _mouseSensitivity = 2f;
    [SerializeField] private Vector2 _rotationYMinMax = new Vector2(-25, 40);
    
    private float _smoothTime = 0.1f;
    private float _rotationX;
    private float _rotationY;
    private Vector3 _currentRotation;
    private Vector3 _smoothVelocity = Vector3.zero;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void LateUpdate()
    {
        if (_followTarget == null)
            return;

        float mouseX = Input.GetAxis(Axis.MouseX) * _mouseSensitivity;
        float mouseY = Input.GetAxis(Axis.MouseY) * _mouseSensitivity;
        _rotationX += mouseX;
        _rotationY -= mouseY;

        _rotationY = Mathf.Clamp(_rotationY, _rotationYMinMax.x, _rotationYMinMax.y);

        Vector2 nextRotation = new Vector2(_rotationY, _rotationX);
        _currentRotation = Vector3.SmoothDamp(_currentRotation, nextRotation, ref _smoothVelocity, _smoothTime);
        transform.eulerAngles = _currentRotation;

        transform.position = _followTarget.position - transform.forward * _distanceFromTarget;
    }
}