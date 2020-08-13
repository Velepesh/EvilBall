using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public void Spawn(Vector3 position, Quaternion rotation)
    {
        Instantiate(gameObject, position, rotation);
    }
}