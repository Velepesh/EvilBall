using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Pathfinding;

[RequireComponent(typeof(SpriteRenderer), typeof(Animator), typeof(AudioSource))]
[RequireComponent(typeof(Collider2D))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private float _shootingDistance;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private Money _money;
    [SerializeField] private Weapon _currentWeapon;
    [SerializeField] private AIDestinationSetter _aIDestinationSetter;

    private Player _target;
    private SpriteRenderer _sprite;
    private Collider2D _collider2D;
    private int _currentHealth;
    private float _timeBetweenAttacks;
    private AudioSource _audioSource;
    private Animator _animator;
    private bool _isPlayerVisible = false;
    private bool _isShootingDistance = false;

    public Player Target => _target;
    public Money Money => _money;
    public bool IsPlayerVisible => _isPlayerVisible;
    public bool IsShootingDistance => _isShootingDistance;

    public event UnityAction<float, float> HealthChanged;
    public event UnityAction<Enemy> Dying;

    private void Start()
    {
        _currentHealth = _health;
        _sprite = GetComponent<SpriteRenderer>();
        _collider2D = GetComponent<Collider2D>();
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>(); 
    }

    private void Update()
    {
        HandRotation();
        Attack();
    }

    private void HandRotation()
    {
        Vector3 targetPosition = _target.transform.position;
        var angle = Vector2.Angle(Vector2.right, targetPosition - transform.position);
        _currentWeapon.transform.eulerAngles = new Vector3(0f, 0f, transform.position.y < targetPosition.y ? angle : -angle);

        Vector3 localScale = Vector3.one;

        if (angle > 90f || angle < -90f)
        {
            localScale.y = -1f;
            _sprite.flipX = true;
        }
        else
        {
            localScale.y = +1f;
            _sprite.flipX = false;
        }

        _currentWeapon.transform.localScale = localScale;
    }

    private void Attack()
    {
        if (_timeBetweenAttacks <= 0f)
        {
            CheckAttackDirection();
            СheckDistanceToPlayer();

            if (IsShootingDistance && IsPlayerVisible)
            {
                _currentWeapon.Attack();
                _timeBetweenAttacks = _currentWeapon.TimeBetweenAttacks;
            }
        }
        else
        {
            _timeBetweenAttacks -= Time.deltaTime;
        }
    }

    private void CheckAttackDirection()
    {
        _isPlayerVisible = false;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, _target.transform.position - transform.position, _shootingDistance, _layerMask);

        if (hit.collider != null)
        {
            if (hit.collider.gameObject.TryGetComponent(out Player player) || hit.collider.gameObject.TryGetComponent(out Shield shield))
            {
                _isPlayerVisible = true;
            }
        }
    }

    private void СheckDistanceToPlayer()
    {
        _isShootingDistance = false;

        Vector3 diff = _target.transform.position - transform.position;
        float curDistance = diff.sqrMagnitude;

        if (curDistance <= _shootingDistance)
        {
            _isShootingDistance = true;
        }
    }

    public void Init(Player target)
    {
        _target = target;
        _aIDestinationSetter.Init(target.transform);
    }

    public void TakeDamage(int damage)
    {
        _animator.SetTrigger("TakeDamage");
        _audioSource.Play();

        _currentHealth -= damage;
        HealthChanged?.Invoke(_currentHealth, _health);

        if (_currentHealth <= 0)
        {
            Dying?.Invoke(this);
            StartCoroutine(EnemyDying(_audioSource.clip.length));
        }
    }

    private IEnumerator EnemyDying(float dyingTime)
    {
        _collider2D.enabled = false;
        _sprite.enabled = false;
        
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }

        yield return new WaitForSeconds(dyingTime);
        Destroy(gameObject);
    }
}