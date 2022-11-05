using UnityEngine;

public class Pistol : Weapon
{
    [SerializeField] private Animator _animator;

    public override void Shoot()
    {
        Bullet bullet = Instantiate(Bullet, ShootPoint.position, ShootPoint.rotation);
        bullet.Init(Damage);
        _animator.SetTrigger(PistolAnimator.Params.Shoot);
    }
}