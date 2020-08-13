using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] private TMP_Text _timerText;

    private float _second = 0f;
    private int _minute = 0;

    public float Second => _second;
    public int Minute => _minute;

    private void Update()
    {
        _second += Time.deltaTime;

        if (_second >= 60)
        {
            _minute++;
            _second = 0f;
        }

        _timerText.text = $"{_minute} : {(int)_second}";
    }
}
