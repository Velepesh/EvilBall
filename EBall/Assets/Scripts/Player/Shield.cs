using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SpriteRenderer), typeof(Collider2D))]
public class Shield : MonoBehaviour
{
    [SerializeField] private float _shieldDurationTime;
    [SerializeField] private float _shieldReloadTime;

    private SpriteRenderer _sprite;
    private Collider2D _collider;

    public float ShieldDurationTime => _shieldDurationTime;
    public float ShieldReloadTime => _shieldReloadTime;

    public event UnityAction<float, float> ValueChanged;

    private void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
        _collider = GetComponent<Collider2D>();
    }

    public void IsUseShield(bool isUsed)
    {
        _sprite.enabled = isUsed;
        _collider.enabled = isUsed;
    }
}