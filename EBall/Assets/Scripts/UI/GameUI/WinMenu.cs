using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinMenu : MonoBehaviour
{
    [SerializeField] private GameObject _winMenu;
    [SerializeField] private Timer _timer;
    [SerializeField] private TMP_Text _timerText;
    [SerializeField] private WinInGameTrigger _winInGameTrigger;

    private void OnEnable()
    {
        _winInGameTrigger.PlayerWon += OnPlayerWon;
    }

    private void OnDisable()
    {
        _winInGameTrigger.PlayerWon -= OnPlayerWon;
    }

    private void OnPlayerWon(int minute, int seconds)
    {
        OpenPanel();
        GetPlayingTimeText(minute, seconds);
    }

    private void OpenPanel()
    {
        _winMenu.SetActive(true);
        Time.timeScale = 0;
    }

    private void GetPlayingTimeText(int minute, int seconds)
    {
        _timerText.text = $"Your time {minute} : {seconds}";
    }

    public void Menu()
    {
        int menuSceneIndex = SceneManager.GetActiveScene().buildIndex - 1;
        SceneManager.LoadScene(menuSceneIndex);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
