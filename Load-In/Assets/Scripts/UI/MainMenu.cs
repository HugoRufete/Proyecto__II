using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true;
        audioSource.Play();
    }
    public void Jugar()
    {
        SceneManager.LoadScene("Escena Principal");
    }

    public void Salir()
    {
        Application.Quit();
    }

    public void Controles()
    {
        SceneManager.LoadScene("Controles");
    }

    
}
