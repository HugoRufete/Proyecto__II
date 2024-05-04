using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
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
