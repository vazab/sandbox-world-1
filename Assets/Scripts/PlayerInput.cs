using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public Vector3 Direction { get; private set; }
    public bool Jump { get; private set; }
    public bool Shoot { get; private set; }

    private void Update()
    {
        float horizontal = Input.GetAxisRaw(Axis.Horizontal);
        float vertical = Input.GetAxisRaw(Axis.Vertical);
        Direction = new Vector3(horizontal, 0, vertical);

        Jump = Input.GetKeyDown(KeyCode.Space);
        Shoot = Input.GetMouseButtonDown(0);
    }
}