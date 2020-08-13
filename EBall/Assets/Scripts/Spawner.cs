using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<Wave> _waves;
    [SerializeField] private List<Transform> _spawnPoints;
    [SerializeField] private Player _player;

    private Wave _currentWave;
    private Transform _spawnPoint;
    private int _currentWaveNumber = 0;
    private float _timeAfterLastSpawn;
    private int _spawned;
    private int _countDiedEnemiesInWave = 0;
    private int _indexOfEnemyInWave = 0;

    [SerializeField] private UnityEvent _enemyKilled = new UnityEvent();

    public event UnityAction EnemyKilled
    {
        add => _enemyKilled.AddListener(value);
        remove => _enemyKilled.RemoveListener(value);
    }
    
    public event UnityAction AllEnemyDied;

    private void Start()
    {
        SetWave(_currentWaveNumber);
    }

    private void Update()
    {
        if (_currentWave == null)
            return;

        _timeAfterLastSpawn += Time.deltaTime;

        if (_timeAfterLastSpawn >= _currentWave.Delay && _currentWave.Templates.Count > _spawned) 
        {
            InstantiateEnemy();
            _spawned++;
            _timeAfterLastSpawn = 0;
        }

        if (_currentWave.Templates.Count <= _countDiedEnemiesInWave)
        {
            if (_waves.Count <= _currentWaveNumber + 1)
            {
                AllEnemyDied?.Invoke();
            }
            else
            {
                _currentWave = null;
                NextWave();
            }
        }
    }

    private void InstantiateEnemy()
    {
        int indexOfSpawnPoint = Random.Range(0, _spawnPoints.Count);
        _spawnPoint = _spawnPoints[indexOfSpawnPoint];

        GameObject template = _currentWave.Templates[_indexOfEnemyInWave++];
        Enemy enemy = Instantiate(template, _spawnPoint.position, _spawnPoint.rotation, _spawnPoint).GetComponent<Enemy>();
        enemy.Init(_player);
        enemy.Dying += OnEnemyDying;
    }

    private void NextWave()
    {
        SetWave(++_currentWaveNumber);
        _indexOfEnemyInWave = 0;
        _spawned = 0;
        _countDiedEnemiesInWave = 0;
    }

    private void SetWave(int index)
    {
        _currentWave = _waves[index];
    }

    private void OnEnemyDying(Enemy enemy)
    {
        _enemyKilled.Invoke();

        Money money = Instantiate(enemy.Money, enemy.transform.position, Quaternion.identity).GetComponent<Money>();
        money.Init(_player);
        _countDiedEnemiesInWave++;
        enemy.Dying -= OnEnemyDying;
    }
}

[System.Serializable]
public class Wave
{
    public List<GameObject> Templates;
    public float Delay;
}