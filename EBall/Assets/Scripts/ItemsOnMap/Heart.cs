using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D), typeof(SpriteRenderer), typeof(AudioSource))]
public class Heart : MonoBehaviour
{
    private Collider2D _collider2D;
    private SpriteRenderer _sprite;
    private AudioSource _audioSource;

    private void Start()
    {
        _collider2D = GetComponent<Collider2D>();
        _sprite = GetComponent<SpriteRenderer>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent(out Player player))
        {
            _audioSource.Play();
            player.ReplenishHealth();
            StartCoroutine(DestroyHeart(_audioSource.clip.length));
        }
    }

    private IEnumerator DestroyHeart(float destroyTime)
    {
        _collider2D.enabled = false;
        _sprite.enabled = false;
        yield return new WaitForSeconds(destroyTime);

        Destroy(gameObject);
    }
}
