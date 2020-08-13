using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBar : Bar
{
    [SerializeField] private Player _player;

    private void OnEnable()
    {
        _player.ShieldValueChanged += OnValueChanged;
        Slider.value = 1;
    }

    private void OnDisable()
    {
        _player.ShieldValueChanged -= OnValueChanged;
    }
}
