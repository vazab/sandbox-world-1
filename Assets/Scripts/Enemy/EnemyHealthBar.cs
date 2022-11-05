using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class EnemyHealthBar : MonoBehaviour
{
    private Camera _mainCamera;
    private Enemy _enemy;
    private MaterialPropertyBlock _matBlock;
    private MeshRenderer _meshRenderer;

    private void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _matBlock = new MaterialPropertyBlock();
        _enemy = GetComponentInParent<Enemy>();
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        if (_enemy.CurrentHealth < _enemy.MaxHealth)
        {
            _meshRenderer.enabled = true;
            AlignCamera();
            UpdateParams();
        }
        else
        {
            _meshRenderer.enabled = false;
        }
    }

    private void UpdateParams()
    {
        _meshRenderer.GetPropertyBlock(_matBlock);
        _matBlock.SetFloat("_Fill", _enemy.CurrentHealth / (float)_enemy.MaxHealth);
        _meshRenderer.SetPropertyBlock(_matBlock);
    }

    private void AlignCamera()
    {
        if (_mainCamera != null)
        {
            Transform camXform = _mainCamera.transform;
            Vector3 forward = transform.position - camXform.position;
            forward.Normalize();
            Vector3 up = Vector3.Cross(forward, camXform.right);
            transform.rotation = Quaternion.LookRotation(forward, up);
        }
    }
}