using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] protected int MaxDamage;
    [SerializeField] protected int MinDamage;

    private float _detectDistance = 0.5f;

    private void Update()
    {
        transform.Translate(Vector2.right * _speed * Time.deltaTime);

        RaycastHit2D hit = Physics2D.Raycast(gameObject.transform.position, transform.right, _detectDistance, _layerMask);

        if (hit.transform != null)
        {
            DetectCollision(hit.transform.gameObject);
            Destroy(gameObject);
        }
    }

    protected abstract void DetectCollision(GameObject objectСontact);
}