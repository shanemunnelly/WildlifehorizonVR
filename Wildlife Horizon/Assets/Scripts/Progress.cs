using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Progress : MonoBehaviour
{
  
    public float progress = 0.0f;
    public Image ProgressBar;

    void Start()
    {

    }

    void Update()
    {
        ProgressBar.fillAmount = Mathf.Clamp(progress / 5, 0, 1);

        if (progress >= 5)
        {
            SceneManager.LoadScene("Victory");
        }
        else
        {
        }
    }

    // Method to increase the Style variable
    public void IncreaseProgresse(float amount)
    {
        progress += amount;
    }
}