
using System;
using UnityEngine;

public class AudioMenager : MonoBehaviour
{

    private AudioSource _audioSource;
    private void Awake() {
        
    }

    public void Start()
    {
        LoadComponents();
    }

    private void LoadComponents()
    {
        _audioSource = GetComponent<AudioSource>();
    }
}
