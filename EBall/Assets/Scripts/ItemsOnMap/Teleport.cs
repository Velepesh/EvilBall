using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField] private Transform _teleportationPoint;
    [SerializeField] private float _delayTime;
    [SerializeField] private CameraFollow _cameraFollow;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
        {
            StartCoroutine(Delay(player));
        }
    }

    private IEnumerator Delay(Player player)
    {
        yield return new WaitForSeconds(_delayTime);
        player.transform.position = _teleportationPoint.position;
    }
}
