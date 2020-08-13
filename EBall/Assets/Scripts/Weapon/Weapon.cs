using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(AudioSource))]
public abstract class Weapon : MonoBehaviour
{
    [SerializeField] private float _timeBetweenAttacks;
    [SerializeField] protected Transform ShootPoint;
    [SerializeField] protected Bullet Bullet;

    protected Animator Animator;
    protected AudioSource AudioSource;

    public float TimeBetweenAttacks => _timeBetweenAttacks;

    private void Start()
    {
        Animator = GetComponent<Animator>();
        AudioSource = GetComponent<AudioSource>();
    }

    public abstract void Attack();
}