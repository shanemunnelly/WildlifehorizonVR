using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {



    public bool isPaused;

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);


    }


    public void QuitGame()
    {
        Debug.Log("Quit!");

        Application.Quit();
    }
    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MainMenu");


    }

    public void GameSpeed()
    {
        if (isPaused)
        {
            Debug.Log("Resuming game");
            ResumeGame();
        }
        else
        {
            Debug.Log("Pausing game");
            PauseGame();
        }

    

    }
    public void PauseGame()
    {

        Time.timeScale = 0f;
        isPaused = true;
    }
    public void ResumeGame()
    {
        Time.timeScale = 1f;
        isPaused = false;
    }
}
