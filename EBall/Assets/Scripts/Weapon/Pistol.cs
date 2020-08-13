using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapon
{
    public override void Attack()
    {
        Animator.SetTrigger("Attack");
        AudioSource.Play();

        Instantiate(Bullet, ShootPoint.position, ShootPoint.rotation);
    }
}
