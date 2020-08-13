using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WinInGameTrigger : MonoBehaviour
{
    [SerializeField] private StatsData _statsData;
    [SerializeField] private Timer _timer;

    public event UnityAction<int, int> PlayerWon;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent(out Player player))
        {
            PlayerWon?.Invoke(_timer.Minute, (int)_timer.Second);
            _statsData.CountingWins();
            _statsData.СomparePlayingTime(_timer.Minute, (int)_timer.Second);
        }
    }
}
