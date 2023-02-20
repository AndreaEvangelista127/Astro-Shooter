using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public AudioSource audio;



    public void openClosePauseMenu()
    {
        if (GameIsPaused)
        {
            Resume();
            audio.Play();
        }
        else
        {
            Pause();
            audio.Pause();
        }
    }
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true); //spunto il game object relativo al menu
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
        //Resume();
        Time.timeScale = 1f;//devo far ripartire il gioco normalmente, altrimenti le particelle non partirebbero
    }

    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }
}
