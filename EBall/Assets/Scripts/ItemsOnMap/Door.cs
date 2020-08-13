using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]
public class Door : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;
    [SerializeField] private CameraFollow _cameraFollow;
    [SerializeField] private float _battleCameraSize;

    private List<GameObject> _doors = new List<GameObject>();
    private AudioSource _audioSource;

    public event UnityAction BattleStarted;
    public event UnityAction BattleEnded;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();

        foreach (Transform door in gameObject.transform)
        {
            _doors.Add(door.gameObject);
            door.gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        _spawner.AllEnemyDied += OnAllEnemyDied;
    }

    private void OnDisable()
    {
        _spawner.AllEnemyDied -= OnAllEnemyDied;
    }

    private void OnAllEnemyDied()
    {
        BattleEnded?.Invoke();
        _cameraFollow.ChangeCameraSize(_cameraFollow.StartCameraSize);
        OpenDoor();
        Destroy(_spawner.gameObject);
    }

    private void OpenDoor()
    {
        Destroy(gameObject);
    }

    private void CloseDoor()
    {
        _audioSource.Play();

        foreach (var door in _doors)
        {
            door.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent(out Player player))
        {
            CloseDoor();
            BattleStarted?.Invoke();
            _cameraFollow.ChangeCameraSize(_battleCameraSize);
            _spawner.gameObject.SetActive(true);
        }
    }
}
