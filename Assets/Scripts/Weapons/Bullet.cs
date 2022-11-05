using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Rigidbody _rigidbody;
    private float _speedMultiply = 100f;
    private float _damage;
    private float _lifeTime = 3f;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        Destroy(gameObject, _lifeTime);
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = transform.forward * _speed * _speedMultiply * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Enemy>(out Enemy enemy))
            enemy.TakeDamage(_damage);
        
        Destroy(gameObject);
    }

    public void Init(float damage)
    {
        _damage = damage;
    }
}