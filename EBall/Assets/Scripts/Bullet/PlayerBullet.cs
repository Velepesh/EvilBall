using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : Bullet
{
    protected override void DetectCollision(GameObject objectСontact)
    {
        if (objectСontact.TryGetComponent(out Enemy enemy))
        {
            enemy.TakeDamage(Random.Range(MinDamage, MaxDamage));
        }
    }
}
