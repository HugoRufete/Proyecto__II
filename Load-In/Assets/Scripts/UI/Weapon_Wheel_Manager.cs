using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Weapon_Wheel_Manager : MonoBehaviour
{
    private Animator animator;

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
    private bool pistolaDesbloqueada = true;
    private bool hachaDesbloqueada = false;
    private bool alabardaDesbloqueda = false;
    private bool martilloGiganteDesbloqueado = false;
    private bool sniperDesbloqueado = false;
    private bool shotgunDesbloqueada = false;

    [Header("Tiendas Diosa")]
    public GameObject Tienda_Diosa_5;
    public GameObject Tienda_Diosa_10;
    public GameObject Tienda_Diosa_15;
    public GameObject Tienda_Diosa_20;

    [Header("Comprar O Negociar")]
    public GameObject esporasInsuficientes;


    private float precioBasePistola = 250;
    private float precioBaseAlabarda = 77;
    private float precioBaseMartilloGigante = 133;
    private float precioBaseSniper = 102;
    private float precioBaseShotgun = 90;

    public static float interesDiosa;

    [Header("Interfaces Iniciales")]

    public GameObject interfazInicialAlabarda;
    public GameObject interfazInicialMartillo;
    public GameObject interfazInicialSniper;
    public GameObject interfazInicialShotgun;


    [Header("Precios Armas & Interfaz Negociaciones")]
    public TMP_Text precioRevolverText;
    public TMP_Text precioPistolaText;
    public TMP_Text precioHachaText;
    public TMP_Text precioAlabardaText;
    public TMP_Text precioMartilloText;
    public TMP_Text precioSniperText;
    public TMP_Text precioShotgunText;
    
    public GameObject menuNegocíaciónAlabarda;
    public GameObject menuNegocíaciónMartillo;
    public GameObject menuNegocíaciónSniper;
    public GameObject menuNegocíaciónShotgun;

    public TMP_Text PrecioNegociado;
    public SliderScript sliderNegocaciónAlabarda;
    public SliderScript sliderNegocaciónPistola;
    public SliderScript sliderNegocaciónMartillo;
    public SliderScript sliderNegocaciónSniper;
    public SliderScript sliderNegocaciónShotgun;

    public GameObject [] dialogosDiosaTienda1;  


    private void Start()
    {
        interesDiosa = 1f;
        ActualizarPrecioAlabarda();
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        if (sliderNegocaciónAlabarda != null)
        {
            int valorSlider = (int)sliderNegocaciónAlabarda.slider.value;
            PrecioNegociado.text = "Pagando: " + valorSlider + " esporas";
        }


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

                if (revolverDesbloqueado)
                {
                    revolver.SetActive(true);
                    revolverImage.SetActive(true);
                    revolver___.SetActive(false);
                }

                if (pistolaDesbloqueada)
                {
                    pistola.SetActive(true);
                    pistolaImage.SetActive(true);
                    pistola___.SetActive(false);
                }

                if (hachaDesbloqueada)
                {
                    hacha.SetActive(true);
                    hachaImage.SetActive(true);
                    hacha___.SetActive(false);
                }

                if (alabardaDesbloqueda)
                {
                    alabarda.SetActive(true);
                    alabardaImage.SetActive(true);
                    alabarda___.SetActive(false);
                }

                if (martilloGiganteDesbloqueado)
                {
                    martilloGigante_1.SetActive(true);
                    martilloGigante_2.SetActive(true);
                    martilloGiganteImage_1.SetActive(true);
                    martilloGiganteImage_2.SetActive(true);
                    martilloGigante___.SetActive(false);
                    martilloGigante____.SetActive(false);

                }

                if (sniperDesbloqueado)
                {
                    sniper.SetActive(true);
                    sniperImage.SetActive(true);
                    sniper___.SetActive(false);
                }

                if (shotgunDesbloqueada)
                {
                    shotgun.SetActive(true);
                    shotgunImage.SetActive(true);
                    shotgun___.SetActive(false);
                }
            }
        }

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
        Debug.Log("Regateando...");

        float interes_10_porciento = 0.90f;
        float interes_20_porciento = 0.80f;
        float interes_30_porciento = 0.70f;

        PlayerEsporas playerEsporas = FindObjectOfType<PlayerEsporas>();

        if (playerEsporas != null)
        {
            int valorSlider = (int)sliderNegocaciónAlabarda.slider.value;
            int cantidadEsporas = playerEsporas.GetEsporas();
            //Precio Alabarda = 77
            if (valorSlider < precioBaseAlabarda * interes_10_porciento)
            {
                DesbloquearAlabarda();
                interesDiosa = interesDiosa * 1.1f;
                dialogosDiosaTienda1[2].SetActive(true);
                dialogosDiosaTienda1[1].SetActive(false);
                ActualizarPrecioAlabarda();
                StartCoroutine(DestroyObjectCoroutine(Tienda_Diosa_5, 7f));
                playerEsporas.RestarEsporas(valorSlider);

                ActualizarPrecioAlabarda();
            }
            else if (valorSlider < precioBaseAlabarda * interes_20_porciento)
            {
                DesbloquearAlabarda();
                interesDiosa = interesDiosa * 1.2f;
                playerEsporas.RestarEsporas(valorSlider);
                dialogosDiosaTienda1[2].SetActive(true);
                dialogosDiosaTienda1[1].SetActive(false);
                ActualizarPrecioAlabarda();
                StartCoroutine(DestroyObjectCoroutine(Tienda_Diosa_5, 7f));

            }
            else if (valorSlider < precioBaseAlabarda * interes_30_porciento)
            {
                DesbloquearAlabarda();
                interesDiosa = interesDiosa * 1.3f;
                playerEsporas.RestarEsporas(valorSlider);
                dialogosDiosaTienda1[2].SetActive(true);
                dialogosDiosaTienda1[1].SetActive(false);
                ActualizarPrecioAlabarda();
                StartCoroutine(DestroyObjectCoroutine(Tienda_Diosa_5, 7f));
                ActualizarPrecioAlabarda();
            }
            else if(valorSlider >= precioBaseAlabarda)
            {
                DesbloquearAlabarda();
                dialogosDiosaTienda1[2].SetActive(true);
                dialogosDiosaTienda1[1].SetActive(false);
                ActualizarPrecioAlabarda();
                StartCoroutine(DestroyObjectCoroutine(Tienda_Diosa_5, 7f));
                playerEsporas.RestarEsporas(valorSlider);
                interesDiosa = 1;
                ActualizarPrecioAlabarda();
            }
            else if (valorSlider > precioBaseAlabarda && cantidadEsporas < precioBaseAlabarda)
            {
                esporasInsuficientes.SetActive(true);
                StartCoroutine(DestroyObjectCoroutine(esporasInsuficientes, 0.5f));
            }
        }
    }


    public void ActivarNegocaciónAlabardar()
    {
        animator.Play("AnimaciónNegocacionAlabarda");
        StartCoroutine(ActivateObjectWithDelay(dialogosDiosaTienda1[1], 2f));
        dialogosDiosaTienda1[0].SetActive(false);


    }

    public void ActivarNegocaciónMartillo()
    {
        animator.Play("AnimaciónNegocaciónMartillo");
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


    public void DesbloquearPistola()
    {
        pistolaDesbloqueada = true;
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
            objetoADestruir.SetActive(false);
        }
    }
    private IEnumerator ActivateObjectWithDelay(GameObject objetoADestruir, float delayInSeconds)
    {
        yield return new WaitForSeconds(delayInSeconds);

        if (objetoADestruir != null)
        {
            objetoADestruir.SetActive(true);
        }
    }

    private void ActualizarPrecioAlabarda()
    {
        precioBaseAlabarda = precioBaseAlabarda * interesDiosa;

        if (precioAlabardaText != null)
        {
            precioAlabardaText.text = precioBaseAlabarda.ToString();
        }
    }

    private void ActualizarPrecioPistola()
    {
        precioBasePistola = precioBasePistola * interesDiosa;

        if (precioPistolaText != null)
        {
            precioPistolaText.text = precioBasePistola.ToString();
        }
    }


    private void ActualizarPrecioMartillo()
    {
        precioBaseMartilloGigante = precioBaseMartilloGigante * interesDiosa;

        if (precioMartilloText != null)
        {
            precioMartilloText.text = precioBaseMartilloGigante.ToString();
        }
    }

    private void ActualizarPrecioSniper()
    {
        precioBaseSniper = precioBaseSniper * interesDiosa;

        if (precioSniperText != null)
        {
            precioSniperText.text = precioBaseSniper.ToString();
        }
    }

    private void ActualizarPrecioShotgun()
    {
        precioBaseShotgun = precioBaseShotgun * interesDiosa;

        if (precioShotgunText != null)
        {
            precioShotgunText.text = precioBaseShotgun.ToString();
        }
    }

}


