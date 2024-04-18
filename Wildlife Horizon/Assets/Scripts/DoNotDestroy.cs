using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoNotDestroy : MonoBehaviour
{
    private void Awake()
    {
        // Check the current scene name
        string currentScene = SceneManager.GetActiveScene().name;

        // Only apply the logic for the first and second scenes
        if (currentScene == "MainMenu" || currentScene == "Inbetween")
        {
            GameObject[] musicObj = GameObject.FindGameObjectsWithTag("Audio");

            // Check if there are more than one objects with the "Audio" tag
            if (musicObj.Length > 1)
            {
                Destroy(this.gameObject);
            }

            // Make sure the object is not destroyed when loading other scenes
            DontDestroyOnLoad(this.gameObject);
        }
    }

    private void OnEnable()
    {
        // Subscribe to the scene loaded event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // Unsubscribe from the scene loaded event to avoid memory leaks
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Check if it's the third scene
        if (scene.name == "MainScreen")
        {
            // Destroy the object when changing to the third scene
            Destroy(this.gameObject);
        }
    }
}
