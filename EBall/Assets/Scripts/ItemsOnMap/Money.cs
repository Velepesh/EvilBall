using UnityEngine;
using System.Collections;

public class Money : MonoBehaviour
{
    [SerializeField] private int _minPrice;
    [SerializeField] private int _maxPrice;
    [SerializeField] private float _minSmoothSpeed;
    [SerializeField] private float _maxSmoothSpeed;
    [SerializeField] private float _delayTimeBeforeFollowing;

    private Vector3 _velocity = Vector3.zero;
    private Player _target;
    private bool _isFollowing = false;

    public void Init(Player target)
    {
        _target = target;
    }

    private void FixedUpdate()
    {
        if (_isFollowing == false)
        {
            StartCoroutine(DelayBeforeFollowing());
        }
        else
        {
            transform.position = Vector3.SmoothDamp(transform.position, _target.transform.position, ref _velocity,
                    Random.Range(_minSmoothSpeed, _maxSmoothSpeed) * Time.fixedDeltaTime);
        }
    }

    private IEnumerator DelayBeforeFollowing()
    {
        yield return new WaitForSeconds(_delayTimeBeforeFollowing);
        _isFollowing = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Wallet wallet))
        {
            wallet.AddMoney(Random.Range(_minPrice, _maxPrice));
            Destroy(gameObject);
        }
    }
}
