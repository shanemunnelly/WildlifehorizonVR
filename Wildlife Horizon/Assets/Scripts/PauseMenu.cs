using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject optionsMenu;
    public bool isPaused;

    void Start()
    {
        pauseMenu.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))  // Use GetKeyDown to check for the first press
        {
            pauseMenu.SetActive(true);
            Debug.Log("Escape key pressed");
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
    }

    public void PauseGame()
    {
        Cursor.visible = true;

        Time.timeScale = 0f;
        isPaused = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void ReturntoMenu()
    {
        Debug.Log("gotomain");

        SceneManager.LoadScene("MainMenu");
        Cursor.visible = true;
    }

    public void QuitGame()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }

    public void ResumeGame()
    {
        Cursor.visible = false;
        pauseMenu.SetActive(false);
        optionsMenu.SetActive(false);

        Time.timeScale = 1f;
        isPaused = false;
    }
}
