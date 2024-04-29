using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class EquiparPrueba : MonoBehaviour
{
    public GameObject machete;
     
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
    public GameObject centroMenu;
    public Image imagenArmaCentro; 

    private GameObject armaActualmenteEquipada;
    private SpriteRenderer spriteRendererActualmenteSeleccionado;

    public Image imagenArmaSeleccionada;

    public TMP_Text textoMunicionActual; 
    public TMP_Text textoMunicionMaxima;

    private Revolver revolverScript;
    public GameObject revolverParent;

    private Hacha hachaScript;
    private LookAtMouse hachaParent;

    private void Start()
    {
        imagenArmaSeleccionada.gameObject.SetActive(false);
        textoMunicionActual.gameObject.SetActive(false);
        textoMunicionMaxima.gameObject.SetActive(false);


        hachaScript = GetComponent<Hacha>();
        hachaParent = GetComponent<LookAtMouse>();

        revolverScript = GetComponent<Revolver>();

        Shotgun.shotgunDisparada += ActualizarInterfazMunicionShotgun;
        Sniper.sniperDisparado += ActualizarInterfazMunicionSniper;
        Revolver.revolverDisparado += ActualizarInterfazMunicionRevolver;
        Pistol.pistolaDisparada += ActualizarInterfazMunicionPistola;
    }

    private void OnDestroy()
    {
        Shotgun.shotgunDisparada -= ActualizarInterfazMunicionShotgun;
        Sniper.sniperDisparado -= ActualizarInterfazMunicionSniper;
        Pistol.pistolaDisparada -= ActualizarInterfazMunicionPistola;
        Revolver.revolverDisparado -= ActualizarInterfazMunicionRevolver;

        // Guardar las balas restantes de la escopeta al destruir el objeto
        LimpiarDatosGuardados();
    }

    public void EquiparShotGun()
    {
        DesequiparArmaActual();
        InstanciarArma(shotgun);
        wheelMenu.SetActive(false);
        MostrarImagenArmaSeleccionada("Shotgun");
    }

    public void EquiparMachete()
    {
        DesequiparArmaActual();
        InstanciarArma(machete);
        wheelMenu.SetActive(false);
        MostrarImagenArmaSeleccionada("Machete");
    }

    public void EquiparPistola()
    {
        DesequiparArmaActual();
        InstanciarArma(pistol);
        wheelMenu.SetActive(false);
        MostrarImagenArmaSeleccionada("Pistola");
        textoMunicionActual.gameObject.SetActive(true);
        textoMunicionMaxima.gameObject.SetActive(true);
        ActualizarInterfazMunicionPistola();
    }

    public void EquiparRevolver()
    {
        DesequiparArmaActual();
        InstanciarArma(revolver);
        wheelMenu.SetActive(false);
        MostrarImagenArmaSeleccionada("Revolver");
        textoMunicionActual.gameObject.SetActive(true);
        textoMunicionMaxima.gameObject.SetActive(true);

        ActualizarInterfazMunicionRevolver();
    }

    public void EquiparSniper()
    {
        DesequiparArmaActual();
        InstanciarArma(sniper);
        wheelMenu.SetActive(false);
        MostrarImagenArmaSeleccionada("Sniper");
        textoMunicionActual.gameObject.SetActive(true);
        textoMunicionMaxima.gameObject.SetActive(true);
    }

    public void EquiparAlabarda()
    {
        DesequiparArmaActual();
        InstanciarArma(alabarda);
        wheelMenu.SetActive(false);
        MostrarImagenArmaSeleccionada("Alabarda");
    }

    public void EquiparHacha()
    {
        hachaScript.enabled = true;
        hachaParent.enabled = true;
        DesequiparArmaActual();
        InstanciarArma(hacha);
        wheelMenu.SetActive(false);
        MostrarImagenArmaSeleccionada("Hacha");
    }

    public void EquiparMartilloGigante()
    {
        DesequiparArmaActual();
        InstanciarArma(martilloGigante);
        wheelMenu.SetActive(false);
        MostrarImagenArmaSeleccionada("MartilloGigante");
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
            GameObject jugador = GameObject.FindGameObjectWithTag("Player");

            if (jugador != null)
            {
                armaActualmenteEquipada = Instantiate(arma, puntoDeEquipamiento.position, Quaternion.Euler(0f, 0f, -45f), jugador.transform);
                wheelMenu.SetActive(true);
                Debug.Log("Arma equipada");

                // Actualizar la interfaz de municiones al equipar el arma
                ActualizarInterfazMunicion();
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
            case "Machete":
                spriteRenderer = machete.GetComponentInChildren<SpriteRenderer>();
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
            imagenArmaCentro.sprite = null;
        }
    }

    public void MostrarImagenArmaSeleccionada(string nombreArma)
    {
        SpriteRenderer nuevoSpriteRenderer = null;

        switch (nombreArma)
        {
            case "Pistola":
                nuevoSpriteRenderer = pistol.GetComponentInChildren<SpriteRenderer>();
                break;
            case "Revolver":
                nuevoSpriteRenderer = revolver.GetComponentInChildren<SpriteRenderer>();
                break;
            case "Shotgun":
                nuevoSpriteRenderer = shotgun.GetComponentInChildren<SpriteRenderer>();
                break;
            case "Sniper":
                nuevoSpriteRenderer = sniper.GetComponentInChildren<SpriteRenderer>();
                break;
            case "Alabarda":
                nuevoSpriteRenderer = alabarda.GetComponentInChildren<SpriteRenderer>();
                break;
            case "Hacha":
                nuevoSpriteRenderer = hacha.GetComponentInChildren<SpriteRenderer>();
                break;
            case "MartilloGigante":
                nuevoSpriteRenderer = martilloGigante.GetComponentInChildren<SpriteRenderer>();
                break;
        }

        if (nuevoSpriteRenderer != null)
        {
            imagenArmaSeleccionada.sprite = nuevoSpriteRenderer.sprite;
            imagenArmaSeleccionada.gameObject.SetActive(true);
            spriteRendererActualmenteSeleccionado = nuevoSpriteRenderer;
        }
        else
        {
            imagenArmaSeleccionada.sprite = null;
        }
    }

    public void ActualizarInterfazMunicion()
    {
        if (armaActualmenteEquipada != null)
        {
            Shotgun shotgunScript = armaActualmenteEquipada.GetComponentInChildren<Shotgun>();
            if (shotgunScript != null)
            {
                ActualizarInterfazMunicionShotgun();
            }
            else
            {
                Sniper sniperScript = armaActualmenteEquipada.GetComponentInChildren<Sniper>();
                if (sniperScript != null)
                {
                    ActualizarInterfazMunicionSniper();
                }
                else
                {
                    Revolver revolverScript = armaActualmenteEquipada.GetComponentInChildren<Revolver>();
                    if (revolverScript != null)
                    {
                        ActualizarInterfazMunicionRevolver();
                    }
                    else
                    {
                        // Si el arma no es una de las anteriores, no necesitamos actualizar la interfaz de municiones
                    }
                }
            }
        }
    }

    public void ActualizarInterfazMunicionShotgun()
    {
        Shotgun scriptArma = armaActualmenteEquipada.GetComponentInChildren<Shotgun>();

        if (scriptArma != null)
        {
            int municionActual = scriptArma.ObtenerMunicionActual();
            int municionMaxima = scriptArma.ObtenerMunicionMaxima();

            textoMunicionActual.text = municionActual.ToString();
            textoMunicionMaxima.text = municionMaxima.ToString();
        }
        else
        {
            textoMunicionActual.text = "Munición Actual: N/A";
            textoMunicionMaxima.text = "Munición Máxima: N/A";
        }
    }

    public void ActualizarInterfazMunicionSniper()
    {
        Sniper scriptArma = armaActualmenteEquipada.GetComponentInChildren<Sniper>();

        if (scriptArma != null)
        {
            int municionActual = scriptArma.ObtenerMunicionActual();
            int municionMaxima = scriptArma.ObtenerMunicionMaxima();

            textoMunicionActual.text = municionActual.ToString();
            textoMunicionMaxima.text = municionMaxima.ToString();
        }
        else
        {
            textoMunicionActual.text = "Munición Actual: N/A";
            textoMunicionMaxima.text = "Munición Máxima: N/A";
        }
    }

    public void ActualizarInterfazMunicionPistola()
    {
        int municionActual = PlayerPrefs.GetInt("BalasPistola", Pistol.ObtenerMaxBalas());
        int municionMaxima = Pistol.ObtenerMaxBalas();

        textoMunicionActual.text = municionActual.ToString();
        textoMunicionMaxima.text = municionMaxima.ToString();
    }

    public void ActualizarInterfazMunicionRevolver()
    {
        Revolver scriptArma = armaActualmenteEquipada.GetComponentInChildren<Revolver>();

        if (scriptArma != null)
        {
            int municionActual = scriptArma.ObtenerMunicionActual();
            int municionMaxima = scriptArma.ObtenerMunicionMaxima();

            textoMunicionActual.text = municionActual.ToString();
            textoMunicionMaxima.text = municionMaxima.ToString();
        }
        else
        {
            textoMunicionActual.text = "Munición Actual: N/A";
            textoMunicionMaxima.text = "Munición Máxima: N/A";
        }
    }
    private void LimpiarDatosGuardados()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }
}
