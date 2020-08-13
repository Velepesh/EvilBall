using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void Play()
    {
        int gameSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(gameSceneIndex);
        Time.timeScale = 1;
    }

    public void Exit()
    {
        Application.Quit();
    }
}
