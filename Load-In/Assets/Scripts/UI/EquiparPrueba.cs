using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EquiparPrueba : MonoBehaviour
{
    [Header("Range Weapons")]
    public GameObject pistol;
    public GameObject revolver;
    public GameObject shotgun;
    public GameObject sniper;

    [Header("Melee Weapons")]
    public GameObject alabarda;
    public GameObject hacha;
    public GameObject martilloGigante;

    [Header("Punto De Equipamiento")]
    public Transform puntoDeEquipamiento;

    public GameObject wheelMenu;
    public GameObject centroMenu; // Objeto para mostrar el arma seleccionada en el centro del menú
    public Image imagenArmaCentro; // Imagen del arma seleccionada en el centro del menú

    private GameObject armaActualmenteEquipada;

    public void EquiparShotGun()
    {
        DesequiparArmaActual();
        InstanciarArma(shotgun);
        wheelMenu.SetActive(false);
    }

    public void EquiparPistola()
    {
        DesequiparArmaActual();
        InstanciarArma(pistol);
        wheelMenu.SetActive(false);
    }

    public void EquiparRevolver()
    {
        DesequiparArmaActual();
        InstanciarArma(revolver);
        wheelMenu.SetActive(false);
    }

    public void EquiparSniper()
    {
        DesequiparArmaActual();
        InstanciarArma(sniper);
        wheelMenu.SetActive(false);
    }

    public void EquiparAlabarda()
    {
        DesequiparArmaActual();
        InstanciarArma(alabarda);
        wheelMenu.SetActive(false);
    }

    public void EquiparHacha()
    {
        DesequiparArmaActual();
        InstanciarArma(hacha);
        wheelMenu.SetActive(false);
    }

    public void EquiparMartilloGigante()
    {
        DesequiparArmaActual();
        InstanciarArma(martilloGigante);
        wheelMenu.SetActive(false);
    }

    private void DesequiparArmaActual()
    {
        if (armaActualmenteEquipada != null)
        {
            Destroy(armaActualmenteEquipada);
        }
    }

    private void InstanciarArma(GameObject arma)
    {
        if (arma != null)
        {
            // Encuentra el objeto del jugador (asume que solo hay un objeto de jugador en la escena)
            GameObject jugador = GameObject.FindGameObjectWithTag("Player");

            if (jugador != null)
            {
                // Instancia el prefab del arma con una rotación de -45 grados en Z y lo hace hijo del jugador
                armaActualmenteEquipada = Instantiate(arma, puntoDeEquipamiento.position, Quaternion.Euler(0f, 0f, -45f), jugador.transform);
                wheelMenu.SetActive(true);
                Debug.Log("Arma equipada");
            }
        }
    }

    public void MostrarImagenArma(string nombreArma)
    {
        Debug.Log("MostrarImagenArma llamado con el arma: " + nombreArma);

        SpriteRenderer spriteRenderer = null;

        switch (nombreArma)
        {
            case "Pistola":
                spriteRenderer = pistol.GetComponentInChildren<SpriteRenderer>();
                break;
            case "Revolver":
                spriteRenderer = revolver.GetComponentInChildren<SpriteRenderer>();
                break;
            case "Shotgun":
                spriteRenderer = shotgun.GetComponentInChildren<SpriteRenderer>();
                break;
            case "Sniper":
                spriteRenderer = sniper.GetComponentInChildren<SpriteRenderer>();
                break;
            case "Alabarda":
                spriteRenderer = alabarda.GetComponentInChildren<SpriteRenderer>();
                break;
            case "Hacha":
                spriteRenderer = hacha.GetComponentInChildren<SpriteRenderer>();
                break;
            case "MartilloGigante":
                spriteRenderer = martilloGigante.GetComponentInChildren<SpriteRenderer>();
                break;
        }

        if (spriteRenderer != null)
        {
            Debug.Log("SpriteRenderer encontrado para el arma: " + nombreArma);
            imagenArmaCentro.sprite = spriteRenderer.sprite;
        }
        else
        {
            Debug.LogWarning("No se encontró SpriteRenderer para el arma: " + nombreArma);
            // Si el nombre del arma no coincide o no se encuentra el SpriteRenderer, desactiva la imagen del centro
            imagenArmaCentro.sprite = null;
        }
    }


}
