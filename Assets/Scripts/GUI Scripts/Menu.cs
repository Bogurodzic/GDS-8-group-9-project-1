using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    
    public void Update()
    {
        if (Input.GetKey("escape"))
        {
            QuitGame();
        }
    }

    public void Start()
    {
    }
    
    private void LoadComponents()
    {
    }

    
    public void PlayGame()
    {
        SceneManager.LoadScene("Scenes/Levels/Level1");
    }



    public void QuitGame()
    {
        Application.Quit();
    }
}
