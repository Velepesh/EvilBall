using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator), typeof(SpriteRenderer), typeof(AudioSource))]
public class Player : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private Inventory _inventory;
    [SerializeField] private Wallet _wallet;
    [SerializeField] private Shield _shield;
    [SerializeField] private GameObject _inventoryContainer;

    private List<Weapon> _weapons = new List<Weapon>();
    private Weapon _currentWeapon;
    private int _currentWeaponNumber = 0;
    private int _currentHealth;
    private float _timeBetweenAttacks;
    private float _elapsedDurationShieldTime = 0f;
    private float _elapsedReloadShieldTime = 0f;
    private SpriteRenderer _sprite;
    private AudioSource _audioSource;
    private Animator _animator;

    public event UnityAction<float, float> HealthChanged;
    public event UnityAction<float, float> ShieldValueChanged;
    public event UnityAction PlayerDied;

    private void Awake()
    {
        GetPlayerWeapons();
        ChangeWeapon(_weapons[_currentWeaponNumber]);
        ReplenishHealth();
        _elapsedDurationShieldTime = _shield.ShieldDurationTime;
        _sprite = GetComponent<SpriteRenderer>();
        _audioSource = GetComponent<AudioSource>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        HandRotation();

        if (_timeBetweenAttacks <= 0f)
        {
            if (Input.GetMouseButton(0))
            {
                _currentWeapon.Attack();
                _timeBetweenAttacks = _currentWeapon.TimeBetweenAttacks;
            }
        }
        else
        {
            _timeBetweenAttacks -= Time.deltaTime;
        }

        if (_elapsedReloadShieldTime >= _shield.ShieldReloadTime)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                _shield.IsUseShield(true);
                _elapsedDurationShieldTime -= Time.deltaTime;
            }

            if (Input.GetKeyUp(KeyCode.Space) || _elapsedDurationShieldTime <= 0f)
            {
                _shield.IsUseShield(false);
                _elapsedDurationShieldTime = _shield.ShieldDurationTime;
                _elapsedReloadShieldTime = 0f;
            }

            ShieldValueChanged?.Invoke(_elapsedDurationShieldTime, _shield.ShieldDurationTime);
        }
        else
        {
            ShieldValueChanged?.Invoke(_elapsedReloadShieldTime, _shield.ShieldReloadTime);
            _elapsedReloadShieldTime += Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            NextWeapon();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            PreviousWeapon();
        }
    }

    private void GetPlayerWeapons()
    {
        for (int i = 0; i < _inventory.GetCountOfWeapon(); i++)
        {
            Gun gun = _inventory.GetGun(i);

            if (gun.IsBuyed)
            {
                Instantiate(gun.Weapon, _inventoryContainer.transform).TryGetComponent(out Weapon weapon);
                _weapons.Add(weapon);
                weapon.gameObject.SetActive(false);
            }
        }
    }

    private void OnEnable()
    {
        _wallet.WeaponBought += OnWeaponBought;
    }

    private void OnDisable()
    {
        _wallet.WeaponBought -= OnWeaponBought;
    }

    private void OnWeaponBought(Gun gun)
    {
        if (Instantiate(gun.Weapon, _inventoryContainer.transform).TryGetComponent(out Weapon weapon))
        {
            _weapons.Add(weapon);
            weapon.gameObject.SetActive(false);
        }
    }

    private void HandRotation()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var angle = Vector2.Angle(Vector2.right, mousePosition - transform.position);
        _currentWeapon.transform.eulerAngles = new Vector3(0f, 0f, transform.position.y < mousePosition.y ? angle : -angle);

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

    private void ChangeWeapon(Weapon weapon)
    {
        _currentWeapon = weapon;
        _currentWeapon.gameObject.SetActive(true);
    }

    public void ReplenishHealth()
    {
        _currentHealth = _health;
        HealthChanged?.Invoke(_currentHealth, _health);
    }

    public void TakeDamage(int damage)
    {
        _animator.SetTrigger("TakeDamage");
        _audioSource.Play();

        _currentHealth -= damage;
        HealthChanged?.Invoke(_currentHealth, _health);

        if (_currentHealth <= 0)
        {
            gameObject.SetActive(false);
            PlayerDied?.Invoke();
        }
    }

    public void NextWeapon()
    {
        if (_currentWeaponNumber == _weapons.Count - 1)
            _currentWeaponNumber = 0;
        else
            _currentWeaponNumber++;

       _currentWeapon.gameObject.SetActive(false);
       ChangeWeapon(_weapons[_currentWeaponNumber]);
    }

    public void PreviousWeapon()
    {
        if (_currentWeaponNumber == 0)
            _currentWeaponNumber = _weapons.Count - 1;
        else
            _currentWeaponNumber--;

        _currentWeapon.gameObject.SetActive(false);
        ChangeWeapon(_weapons[_currentWeaponNumber]);
    }
}