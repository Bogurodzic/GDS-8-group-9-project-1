using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    private AudioSource _audioSource;
    
    public void Update()
    {
        if (Input.GetKey("escape"))
        {
            QuitGame();
        }
    }

    public void Start()
    {
        LoadComponents();
    }
    
    private void LoadComponents()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    
    public void PlayGame()
    {
        _audioSource.Play();
        SceneManager.LoadScene("Scenes/Levels/Level1");
    }



    public void QuitGame()
    {
        Application.Quit();
    }
}
