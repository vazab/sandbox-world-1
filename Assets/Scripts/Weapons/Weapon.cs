using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected Bullet Bullet;
    [SerializeField] protected Transform ShootPoint;
    [SerializeField] protected float Damage;
    [SerializeField] private string _name;

    public abstract void Shoot();
}
