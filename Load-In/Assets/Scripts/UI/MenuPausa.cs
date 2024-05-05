using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPausa : MonoBehaviour
{
    public GameObject menuPausa;
    public GameObject botonPausa;
    public void PausarJuego()
    {
        Time.timeScale = 0f;
        botonPausa.SetActive(false);
        menuPausa.SetActive(true);
    }

    public void ReanudarJuego()
    {
        Time.timeScale = 1.0f;
        botonPausa.SetActive(true);
        menuPausa.SetActive(false);
    }

    public void Restart()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Escena principal");
        botonPausa.SetActive(true);
        menuPausa.SetActive(false);
    }

    public void BackMenu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("MainMenu");
        botonPausa.SetActive(true);
        menuPausa.SetActive(false);
    }
}
