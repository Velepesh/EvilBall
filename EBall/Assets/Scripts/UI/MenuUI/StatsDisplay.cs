using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatsDisplay : MonoBehaviour
{
    [SerializeField] private StatsData _statsData;
    [SerializeField] private TMP_Text _winsText;
    [SerializeField] private TMP_Text _killsText;
    [SerializeField] private TMP_Text _bestTimeText;
    [SerializeField] private TMP_Text _currentTimeText;

    private void Start()
    {
        ShowNumberOfWins();
        ShowNumberOfKills();
        ShowBestTime();
    }

    private void Update()
    {
        ShowCurrentTime();
    }

    private void ShowNumberOfWins()
    {
        _winsText.text = _statsData.NumberOfWins.ToString();
    }

    private void ShowNumberOfKills()
    {
        _killsText.text = _statsData.NumberOfkills.ToString();
    }

    private void ShowBestTime()
    {
        _bestTimeText.text = _statsData.BestTime;
    }

    private void ShowCurrentTime()
    {
        _currentTimeText.text = System.DateTime.Now.ToLongTimeString();
    }
}
