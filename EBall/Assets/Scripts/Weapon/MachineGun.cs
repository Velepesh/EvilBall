using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGun : Weapon
{
    [SerializeField] private float _deflectionAngle;

    public override void Attack()
    {
        Animator.SetTrigger("Attack");
        AudioSource.Play();
        
        float minAngle = ShootPoint.eulerAngles.z - _deflectionAngle;
        float maxAngle = ShootPoint.eulerAngles.z + _deflectionAngle;
        float spread = Random.Range(minAngle, maxAngle);

        Instantiate(Bullet, ShootPoint.position, Quaternion.Euler(0f, 0f, spread));
    }
}
