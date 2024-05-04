using UnityEngine;
using UnityEngine.SceneManagement;

public class CargarEscena : MonoBehaviour
{
    public void CargarEscenaPrincipal()
    {
        SceneManager.LoadScene("Escena Principal");
    }
    public void CargarMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void CargarControles()
    {
        SceneManager.LoadScene("Controlles");
    }
}
