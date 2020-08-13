using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : Bullet
{
    protected override void DetectCollision(GameObject objectСontact)
    {
        if (objectСontact.TryGetComponent(out Player player))
        {
            player.TakeDamage(Random.Range(MinDamage, MaxDamage));
        }
    }
}
