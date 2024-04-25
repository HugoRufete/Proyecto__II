using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Weapon_Wheel_Manager : MonoBehaviour
{
    public GameObject popUpInicial;

    public GameObject wheelMenu;  

    [Header("Armas Desbloqueables : Revolver")]
    public GameObject revolver;
    public GameObject revolverImage;
    public GameObject revolver___;

    [Header("Armas Desbloqueables : Pistola")]
    public GameObject pistola;
    public GameObject pistolaImage;
    public GameObject pistola___;

    [Header("Armas Desbloqueables : Hacha")]
    public GameObject hacha;
    public GameObject hachaImage;
    public GameObject hacha___;

    [Header("Armas Desbloqueables : Alabarda")]
    public GameObject alabarda;
    public GameObject alabardaImage;
    public GameObject alabarda___;

    [Header("Armas Desbloqueables : MartilloGigante")]
    public GameObject martilloGigante_1;
    public GameObject martilloGigante_2;
    public GameObject martilloGiganteImage_1;
    public GameObject martilloGiganteImage_2;
    public GameObject martilloGigante___;
    public GameObject martilloGigante____;
   

    [Header("Armas Desbloqueables : Sniper")]
    public GameObject sniper;
    public GameObject sniperImage;
    public GameObject sniper___;

    [Header("Armas Desbloqueables : Shotgun")]
    public GameObject shotgun;
    public GameObject shotgunImage;
    public GameObject shotgun___;

    private bool menuActivo = false;

    private bool revolverDesbloqueado = false;
    private bool pistolaDesbloqueada = false;
    private bool hachaDesbloqueada = false;
    private bool alabardaDesbloqueda= false;
    private bool martilloGiganteDesbloqueado = false;
    private bool sniperDesbloqueado = false;
    private bool shotgunDesbloqueada = false;

    public GameObject Tienda_Diosa_5;
    public GameObject Tienda_Diosa_10;
    public GameObject Tienda_Diosa_15;
    public GameObject Tienda_Diosa_20;

    [Header("Comprar O Negociar")]
    public GameObject esporasInsuficientes;

    private float precioBaseRevolver = 250 ;
    private float precioBasePistola = 250 ;
    private float precioBaseHacha = 250 ;
    private float precioBaseAlabarda = 250 ;
    private float precioBaseMartilloGigante = 250 ;
    private float precioBaseSniper = 250 ;
    private float precioBaseShotgun = 250 ;

    public static float interesDiosa;

    [Header("Precios Armas & Interfaz Negociaciones")]
    public TMP_Text precioRevolverText;
    public TMP_Text precioPistolaText;
    public TMP_Text precioHachaText;
    public TMP_Text precioAlabardaText;
    public TMP_Text precioMartilloText;
    public TMP_Text precioSniperText;
    public TMP_Text precioShotgunText;
    public GameObject menuNegocíaciónRevolver;
    public GameObject menuNegocíaciónPistola;
    public GameObject menuNegocíaciónHacha;
    public GameObject menuNegocíaciónAlabarda;
    public GameObject menuNegocíaciónMartillo;
    public GameObject menuNegocíaciónSniper;
    public GameObject menuNegocíaciónShotgun;


    private void Start()
    {
        interesDiosa = 1f;
    }
    void Update()
    {
    

        // Verifica si se presiona la tecla E
        if (Input.GetKeyDown(KeyCode.E))
        {
            popUpInicial.SetActive(false);

            // Si el menú está activo, desactívalo
            if (menuActivo)
            {
                wheelMenu.SetActive(false);
                menuActivo = false;
            }
            // Si el menú no está activo, actívalo
            else
            {
                wheelMenu.SetActive(true);
                menuActivo = true;

                if(revolverDesbloqueado)
                {
                    revolver.SetActive(true);
                    revolverImage.SetActive(true);
                    revolver___.SetActive(false);
                }

                if(pistolaDesbloqueada)
                {
                    pistola.SetActive(true);
                    pistolaImage.SetActive(true);
                    pistola___.SetActive(false);
                }

                if(hachaDesbloqueada)
                {
                    hacha.SetActive(true);
                    hachaImage.SetActive(true);
                    hacha___.SetActive(false);
                }

                if(alabardaDesbloqueda)
                {
                    alabarda.SetActive(true);
                    alabardaImage.SetActive(true);
                    alabarda___.SetActive(false);
                }

                if(martilloGiganteDesbloqueado)
                {
                    martilloGigante_1.SetActive(true);
                    martilloGigante_2.SetActive(true);
                    martilloGiganteImage_1.SetActive(true);
                    martilloGiganteImage_2.SetActive(true);
                    martilloGigante___.SetActive(false);
                    martilloGigante____.SetActive(false);

                }
               
                if(sniperDesbloqueado)
                {
                    sniper.SetActive(true);
                    sniperImage.SetActive(true);
                    sniper___.SetActive(false);
                }

                if(shotgunDesbloqueada)
                {
                    shotgun.SetActive(true);
                    shotgunImage.SetActive(true);
                    shotgun___.SetActive(false);
                }
            }
        }

    }

   

    public void ComprarRevolver()
    {
        PlayerEsporas playerEsporas = FindObjectOfType<PlayerEsporas>();

        if (playerEsporas != null)
        {
            int cantidadEsporas = playerEsporas.GetEsporas();

            if (cantidadEsporas >= precioBaseRevolver)
            {
                DesbloquearRevolver();
                interesDiosa = 1;
                Tienda_Diosa_5.SetActive(false);
            }
            else
            {
                esporasInsuficientes.SetActive(true);
                StartCoroutine(DestroyObjectCoroutine(esporasInsuficientes, 0.5f));
            }
        }
        ActualizarPrecioRevolver();
    }

    public void NegociarRevolver()
    {
        //menuNegocíaciónRevolver.SetActive(true);
        PlayerEsporas playerEsporas = FindObjectOfType<PlayerEsporas>();

        if (playerEsporas != null)
        {
            int cantidadEsporas = playerEsporas.GetEsporas();

            if (cantidadEsporas < precioBaseRevolver)
            {
                DesbloquearRevolver();
                interesDiosa = 2f;
                Tienda_Diosa_5.SetActive(false);
            }
            
        }
        ActualizarPrecioRevolver();
    }

    public void ComprarPistola()
    {
        PlayerEsporas playerEsporas = FindObjectOfType<PlayerEsporas>();

        if (playerEsporas != null)
        {
            int cantidadEsporas = playerEsporas.GetEsporas();

            if (cantidadEsporas >= precioBasePistola)
            {
                DesbloquearPistola();
                interesDiosa = 1;
                Tienda_Diosa_5.SetActive(false);
            }
            else
            {
                esporasInsuficientes.SetActive(true);
                StartCoroutine(DestroyObjectCoroutine(esporasInsuficientes, 0.5f));
            }
        }
        ActualizarPrecioPistola();
    }

    public void NegociarPistola()
    {
        //menuNegocíaciónRevolver.SetActive(true);
        PlayerEsporas playerEsporas = FindObjectOfType<PlayerEsporas>();

        if (playerEsporas != null)
        {
            int cantidadEsporas = playerEsporas.GetEsporas();

            if (cantidadEsporas < precioBasePistola)
            {
                DesbloquearPistola();
                interesDiosa = 2f;
                Tienda_Diosa_5.SetActive(false);
            }

        }
        ActualizarPrecioPistola();
    }

    public void ComprarHacha()
    {
        PlayerEsporas playerEsporas = FindObjectOfType<PlayerEsporas>();

        if (playerEsporas != null)
        {
            int cantidadEsporas = playerEsporas.GetEsporas();

            if (cantidadEsporas >= precioBaseHacha)
            {
                DesbloquearHacha();
                interesDiosa = 1;
                Tienda_Diosa_10.SetActive(false);
            }
            else
            {
                esporasInsuficientes.SetActive(true);
                StartCoroutine(DestroyObjectCoroutine(esporasInsuficientes, 0.5f));
            }
        }
        ActualizarPrecioHacha();
    }

    public void NegociarHacha()
    {
        //menuNegocíaciónRevolver.SetActive(true);
        PlayerEsporas playerEsporas = FindObjectOfType<PlayerEsporas>();

        if (playerEsporas != null)
        {
            int cantidadEsporas = playerEsporas.GetEsporas();

            if (cantidadEsporas < precioBaseHacha)
            {
                DesbloquearHacha();
                interesDiosa = 2f;
                Tienda_Diosa_10.SetActive(false);
            }

        }
        ActualizarPrecioHacha();
    }

    public void ComprarAlabarda()
    {
        PlayerEsporas playerEsporas = FindObjectOfType<PlayerEsporas>();

        if (playerEsporas != null)
        {
            int cantidadEsporas = playerEsporas.GetEsporas();

            if (cantidadEsporas >= precioBaseAlabarda)
            {
                DesbloquearAlabarda();
                interesDiosa = 1;
                Tienda_Diosa_10.SetActive(false);
            }
            else
            {
                esporasInsuficientes.SetActive(true);
                StartCoroutine(DestroyObjectCoroutine(esporasInsuficientes, 0.5f));
            }
        }
        ActualizarPrecioAlabarda();
    }

    public void NegociarAlabarda()
    {
        //menuNegocíaciónRevolver.SetActive(true);
        PlayerEsporas playerEsporas = FindObjectOfType<PlayerEsporas>();

        if (playerEsporas != null)
        {
            int cantidadEsporas = playerEsporas.GetEsporas();

            if (cantidadEsporas < precioBaseAlabarda)
            {
                DesbloquearAlabarda();
                interesDiosa = 2f;
                Tienda_Diosa_10.SetActive(false);
            }

        }
        ActualizarPrecioAlabarda();
    }
    
    public void ComprarMartilloGigante()
    {
        PlayerEsporas playerEsporas = FindObjectOfType<PlayerEsporas>();

        if (playerEsporas != null)
        {
            int cantidadEsporas = playerEsporas.GetEsporas();

            if (cantidadEsporas >= precioBaseMartilloGigante)
            {
                DesbloquearPistola();
                interesDiosa = 1;
                Tienda_Diosa_15.SetActive(false);
            }
            else
            {
                esporasInsuficientes.SetActive(true);
                StartCoroutine(DestroyObjectCoroutine(esporasInsuficientes, 0.5f));
            }
        }
        ActualizarPrecioMartillo();
    }

    public void NegociarMartilloGigante()
    {
        //menuNegocíaciónRevolver.SetActive(true);
        PlayerEsporas playerEsporas = FindObjectOfType<PlayerEsporas>();

        if (playerEsporas != null)
        {
            int cantidadEsporas = playerEsporas.GetEsporas();

            if (cantidadEsporas < precioBaseMartilloGigante)
            {
                DesbloquearPistola();
                interesDiosa = 2f;
                Tienda_Diosa_15.SetActive(false);
            }

        }
        ActualizarPrecioMartillo();
    }

    public void ComprarShotgun()
    {
        PlayerEsporas playerEsporas = FindObjectOfType<PlayerEsporas>();

        if (playerEsporas != null)
        {
            int cantidadEsporas = playerEsporas.GetEsporas();

            if (cantidadEsporas >= precioBaseShotgun)
            {
                DesbloquearSniper();
                interesDiosa = 1;
                Tienda_Diosa_20.SetActive(false);
            }
            else
            {
                esporasInsuficientes.SetActive(true);
                StartCoroutine(DestroyObjectCoroutine(esporasInsuficientes, 0.5f));
            }
        }
        ActualizarPrecioShotgun();
    }
    
    public void NegociarShotgun()
    {
        //menuNegocíaciónRevolver.SetActive(true);
        PlayerEsporas playerEsporas = FindObjectOfType<PlayerEsporas>();

        if (playerEsporas != null)
        {
            int cantidadEsporas = playerEsporas.GetEsporas();

            if (cantidadEsporas < precioBaseShotgun)
            {
                DesbloquearSniper();
                interesDiosa = 2f;
                Tienda_Diosa_20.SetActive(false);
            }

        }
        ActualizarPrecioShotgun();
    }
    
    public void ComprarSniper()
    {
        PlayerEsporas playerEsporas = FindObjectOfType<PlayerEsporas>();

        if (playerEsporas != null)
        {
            int cantidadEsporas = playerEsporas.GetEsporas();

            if (cantidadEsporas >= precioBaseSniper)
            {
                DesbloquearSniper();
                interesDiosa = 1;
                Tienda_Diosa_20.SetActive(false);
            }
            else
            {
                esporasInsuficientes.SetActive(true);
                StartCoroutine(DestroyObjectCoroutine(esporasInsuficientes, 0.5f));
            }
        }
        ActualizarPrecioSniper();
    }
    
    public void NegociarSniper()
    {
        //menuNegocíaciónRevolver.SetActive(true);
        PlayerEsporas playerEsporas = FindObjectOfType<PlayerEsporas>();

        if (playerEsporas != null)
        {
            int cantidadEsporas = playerEsporas.GetEsporas();

            if (cantidadEsporas < precioBaseSniper)
            {
                DesbloquearSniper();
                interesDiosa = 2f;
                Tienda_Diosa_20.SetActive(false);
            }

        }
        ActualizarPrecioSniper();
    }




    public void DesbloquearRevolver()
    {
        revolverDesbloqueado = true;
        Time.timeScale = 1.0f;
    }

    public void DesbloquearPistola()
    {
        pistolaDesbloqueada = true;
        Time.timeScale = 1.0f;
    }

    public void DesbloquearHacha()
    {
        hachaDesbloqueada = true;
        Time.timeScale = 1.0f;
    }

    public void DesbloquearAlabarda()
    {
        alabardaDesbloqueda = true;
        Time.timeScale = 1.0f;
    }

    public void DesbloquearMartilloGigante()
    {
        martilloGiganteDesbloqueado = true;
        Time.timeScale = 1.0f;
    }

    public void DesbloquearSniper()
    {
        sniperDesbloqueado = true;
        Time.timeScale = 1.0f;
    }

    public void DesbloquearShotgun()
    {
        shotgunDesbloqueada = true;
        Time.timeScale = 1.0f;
    }



    public void BloquearPistola()
    {
        pistolaDesbloqueada = false;
        Time.timeScale = 1.0f;
    }

    public void BloqueaHacha()
    {
        hachaDesbloqueada = false;
        Time.timeScale = 1.0f;
    }

    public void BloqueaAlabarda()
    {
        alabardaDesbloqueda = false;
        Time.timeScale = 1.0f;
    }

    public void BloqueaMartilloGigante()
    {
        martilloGiganteDesbloqueado = false;
        Time.timeScale = 1.0f;
    }

    public void BloqueaSniper()
    {
        sniperDesbloqueado = false;
        Time.timeScale = 1.0f;
    }

    public void BloqueaShotgun()
    {
        shotgunDesbloqueada = false;
        Time.timeScale = 1.0f;
    }



    private IEnumerator DestroyObjectCoroutine(GameObject objetoADestruir, float delayInSeconds)
    {
        yield return new WaitForSeconds(delayInSeconds);

        if (objetoADestruir != null)
        {
            Destroy(objetoADestruir);
        }
    }

    
    private void ActualizarPrecioRevolver()
    {
        precioBaseRevolver = precioBaseRevolver * interesDiosa;
        
        if (precioRevolverText != null)
        {
            precioRevolverText.text = precioBaseRevolver.ToString();
        }
    }
    
    private void ActualizarPrecioPistola()
    {
        precioBasePistola = precioBasePistola * interesDiosa;

        if (precioPistolaText != null)
        {
            precioPistolaText.text = precioBaseRevolver.ToString();
        }
    }
    
    private void ActualizarPrecioHacha()
    {
        precioBaseHacha = precioBaseHacha * interesDiosa;

        if (precioHachaText != null)
        {
            precioHachaText.text = precioBaseRevolver.ToString();
        }
    }
    private void ActualizarPrecioAlabarda()
    {
        precioBaseAlabarda = precioBaseAlabarda * interesDiosa;

        if (precioAlabardaText != null)
        {
            precioAlabardaText.text = precioBaseRevolver.ToString();
        }
    }

    private void ActualizarPrecioMartillo()
    {
        precioBaseMartilloGigante = precioBaseMartilloGigante * interesDiosa;

        if (precioMartilloText != null)
        {
            precioMartilloText.text = precioBaseRevolver.ToString();
        }
    }

    private void ActualizarPrecioSniper()
    {
        precioBaseSniper = precioBaseSniper * interesDiosa;

        if (precioSniperText != null)
        {
            precioSniperText.text = precioBaseRevolver.ToString();
        }
    }

    private void ActualizarPrecioShotgun()
    {
        precioBaseShotgun = precioBaseShotgun * interesDiosa;

        if (precioShotgunText != null)
        {
            precioShotgunText.text = precioBaseRevolver.ToString();
        }
    }

}
