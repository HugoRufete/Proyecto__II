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
    private float precioBaseMartilloGigante = 120;
    private float precioBaseSniper = 102;
    private float precioBaseShotgun = 90;
   
    private float precioBaseAlmaDa�oReducido = 102;
    private float precioBaseDa�oAumentado = 94;
    
    private float precioBaseVortice = 102;
    private float precioBaseEmpujon = 103;
    private float precioBaseBombaHumo = 150;
    private float precioBaseMolotov = 123;
    
    float interes_10_porciento = 0.90f;
    float interes_20_porciento = 0.80f;
    float interes_30_porciento = 0.70f;



    [Header("Precios Armas & Interfaz Negociaciones")]
    public TMP_Text precioAlabardaText;
    public TMP_Text precioMartilloText;
    public TMP_Text precioSniperText;
    public TMP_Text precioShotgunText;
    public TMP_Text precioDa�oAumentadoText;
    
    public TMP_Text PrecioNegociado;
    public TMP_Text PrecioNegociadoSniper;
    public TMP_Text PrecioNegociadoDa�oAumentado;
    public TMP_Text PrecioNegociadoMartilo;

    public SliderScript sliderNegocaci�nAlabarda;
    public SliderScript sliderNegocaci�nMartillo;
    public SliderScript sliderNegocaci�nSniper;
    public SliderScript sliderNegocaci�nShotgun;
    public SliderScript sliderNegocaci�nDa�oAumentado;

    public SliderScript sliderBolsaEsporasExtra;
    [SerializeField] private float extraSpores = 0;

    public static float interesDiosa;

    [Header("Esporas Extra")]
    public TMP_Text extraSporesText;

    private const string esporasExtra = "esporasExtra";

    public AumentarDa�oAEnemigos aumentarDa�o;
    
    public GameObject [] dialogosDiosaTienda1;

    

    void SaveExtraSpores()
    {
        // Save the value of the extra spores variable to player prefs
        PlayerPrefs.SetFloat(esporasExtra, extraSpores);
    }

    void LoadExtraSpores()
    {
        // Load the value of the extra spores variable from player prefs
        extraSpores = PlayerPrefs.GetFloat(esporasExtra, extraSpores);
    }

    public void UpdateExtraSporesText()
    {
        extraSporesText.text = extraSpores.ToString();

    }
    public void UpdateExtraSporesText2(float newExtraSpores)
    {
        extraSpores += newExtraSpores;
        extraSporesText.text = extraSpores.ToString();
    }
    private void Start()
    {
        interesDiosa = 1f;
        ActualizarPrecioAlabarda();
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        if(sliderBolsaEsporasExtra != null)
        {
            sliderBolsaEsporasExtra.slider.value = extraSpores;
        }
        if (sliderNegocaci�nAlabarda != null)
        {
            int valorSlider = (int)sliderNegocaci�nAlabarda.slider.value;
            PrecioNegociado.text = "Pagando: " + valorSlider + " esporas";
        }
        if (sliderNegocaci�nSniper != null)
        {
            int valorSliderSniper = (int)sliderNegocaci�nSniper.slider.value;
            PrecioNegociadoSniper.text = "Pagando : " + valorSliderSniper + " esporas";
        }
        if (sliderNegocaci�nMartillo != null)
        {
            int valorSliderMartillo = (int)sliderNegocaci�nMartillo.slider.value;
            PrecioNegociadoMartilo.text = "Pagando : " + valorSliderMartillo + " esporas";
        }
        if (sliderNegocaci�nDa�oAumentado != null)
        {
            int valorSliderMartillo = (int)sliderNegocaci�nDa�oAumentado.slider.value;
            PrecioNegociadoDa�oAumentado.text = "Pagando : " + valorSliderMartillo + " esporas";
        }


        if (Input.GetKeyDown(KeyCode.E))
        {
            popUpInicial.SetActive(false);

            // Si el men� est� activo, desact�valo
            if (menuActivo)
            {
                wheelMenu.SetActive(false);
                menuActivo = false;
            }
            // Si el men� no est� activo, act�valo
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

    public void ActivarNegocaci�nAlabardar()
    {
        animator.Play("Animaci�nNegocacionAlabarda");
        StartCoroutine(ActivateObjectWithDelay(dialogosDiosaTienda1[1], 2f));
        dialogosDiosaTienda1[0].SetActive(false);
    }
    public void ActivarNegocaci�nSniper()
    {
        animator.Play("Animaci�nNegocacionSniper");
        StartCoroutine(ActivateObjectWithDelay(dialogosDiosaTienda1[1], 2f));
        dialogosDiosaTienda1[0].SetActive(false);
    }

    public void ActivarNegociaci�nDa�oAumentado()
    {
        animator.Play("Animaci�nNegocacionAumentoDa�o");
        StartCoroutine(ActivateObjectWithDelay(dialogosDiosaTienda1[1], 2f));
        dialogosDiosaTienda1[0].SetActive(false);
    }

    public void ActivarNegocaci�nMartillo()
    {
        animator.Play("Animaci�nNegocaci�nMartillo");
    }
    public void ComprarAlabarda()
    {
        PlayerEsporas playerEsporas = FindObjectOfType<PlayerEsporas>();
        FreezeEnemies freezeEnemie = FindObjectOfType<FreezeEnemies>();

        if (playerEsporas != null)
        {
            int cantidadEsporas = playerEsporas.GetEsporas();

            if (cantidadEsporas >= precioBaseAlabarda)
            {
                freezeEnemie.ActivarComponentes();
                DesbloquearAlabarda();
                interesDiosa = 1;
                dialogosDiosaTienda1[4].SetActive(true);
                dialogosDiosaTienda1[0].SetActive(false);
                StartCoroutine(DestroyObjectCoroutine(Tienda_Diosa_5, 4f));
                StartCoroutine(DestroyObjectCoroutine(dialogosDiosaTienda1[4], 4f));


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
        BolsaEsporas bolsaEsporas = FindAnyObjectByType<BolsaEsporas>();
        Debug.Log("Regateando...");

        PlayerEsporas playerEsporas = FindObjectOfType<PlayerEsporas>();
        
        if (playerEsporas != null)
        {
            int valorSliderAlabarda = (int)sliderNegocaci�nAlabarda.slider.value;
            int cantidadEsporas = playerEsporas.GetEsporas();

            //Precio Alabarda = 77
            if (valorSliderAlabarda < precioBaseAlabarda * interes_10_porciento && cantidadEsporas >= precioBaseAlabarda * interes_10_porciento)
            {
                Debug.Log("Desbloqueando Alabarada 10");
                DesbloquearAlabarda();
                interesDiosa = interesDiosa * 1.1f;
                dialogosDiosaTienda1[2].SetActive(true);
                dialogosDiosaTienda1[1].SetActive(false);
                StartCoroutine(DestroyObjectCoroutine(Tienda_Diosa_5, 7f));
                playerEsporas.RestarEsporas(valorSliderAlabarda);

                ActualizarPrecioAlabarda();
            }
            else if (valorSliderAlabarda < precioBaseAlabarda * interes_20_porciento && cantidadEsporas >= precioBaseAlabarda * interes_20_porciento)
            {
                Debug.Log("Desbloqueando Alabarada 20%");
                DesbloquearAlabarda();
                interesDiosa = interesDiosa * 1.2f;
                playerEsporas.RestarEsporas(valorSliderAlabarda);
                dialogosDiosaTienda1[2].SetActive(true);
                dialogosDiosaTienda1[1].SetActive(false);
                StartCoroutine(DestroyObjectCoroutine(Tienda_Diosa_5, 7f));

                ActualizarPrecioAlabarda();
            }
            else if (valorSliderAlabarda < precioBaseAlabarda * interes_30_porciento && cantidadEsporas >= precioBaseAlabarda * interes_30_porciento)
            {
                Debug.Log("Desbloqueando Alabarada 30%");
                DesbloquearAlabarda();
                interesDiosa = interesDiosa * 1.3f;
                playerEsporas.RestarEsporas(valorSliderAlabarda);
                dialogosDiosaTienda1[2].SetActive(true);
                dialogosDiosaTienda1[1].SetActive(false);
                ActualizarPrecioAlabarda();
                StartCoroutine(DestroyObjectCoroutine(Tienda_Diosa_5, 7f));
                ActualizarPrecioAlabarda();
            }
            else if(valorSliderAlabarda >= precioBaseAlabarda && cantidadEsporas >= precioBaseAlabarda)
            {
                Debug.Log("Desbloqueando Alabarada");
                DesbloquearAlabarda();
                dialogosDiosaTienda1[3].SetActive(true);
                dialogosDiosaTienda1[1].SetActive(false);
                ActualizarPrecioAlabarda();
                StartCoroutine(DestroyObjectCoroutine(Tienda_Diosa_5, 7f));
                playerEsporas.RestarEsporas(valorSliderAlabarda);
                interesDiosa = 1;
                ActualizarPrecioAlabarda();

                extraSpores = valorSliderAlabarda - precioBaseAlabarda;

                StartCoroutine(bolsaEsporas.InstantiateEsporas(extraSpores));
                SaveExtraSpores();
                UpdateExtraSporesText();

                
            }
            else if (valorSliderAlabarda > precioBaseAlabarda && cantidadEsporas < precioBaseAlabarda)
            {
                esporasInsuficientes.SetActive(true);
                StartCoroutine(DestroyObjectCoroutine(esporasInsuficientes, 0.5f));
            }
            else if(valorSliderAlabarda < precioBaseAlabarda && cantidadEsporas < precioBaseAlabarda)
            {
                esporasInsuficientes.SetActive(true);
                StartCoroutine(DestroyObjectCoroutine(esporasInsuficientes, 0.5f));
            }
        }
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
                dialogosDiosaTienda1[4].SetActive(true);
                dialogosDiosaTienda1[0].SetActive(false);
                StartCoroutine(DestroyObjectCoroutine(Tienda_Diosa_5, 4f));
                StartCoroutine(DestroyObjectCoroutine(dialogosDiosaTienda1[4], 4f));

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
        BolsaEsporas bolsaEsporas = FindAnyObjectByType<BolsaEsporas>();
        Debug.Log("Regateando...");

        PlayerEsporas playerEsporas = FindObjectOfType<PlayerEsporas>();

        if (playerEsporas != null)
        {
            int valorSliderSniper = (int)sliderNegocaci�nSniper.slider.value;
            int cantidadEsporas = playerEsporas.GetEsporas();

            if (valorSliderSniper < precioBaseSniper * interes_10_porciento)
            {
                DesbloquearSniper();
                interesDiosa = interesDiosa * 1.1f;
                dialogosDiosaTienda1[2].SetActive(true);
                dialogosDiosaTienda1[1].SetActive(false);
                StartCoroutine(DestroyObjectCoroutine(Tienda_Diosa_5, 7f));
                playerEsporas.RestarEsporas(valorSliderSniper);

                ActualizarPrecioSniper();
            }
            else if (valorSliderSniper < precioBaseSniper * interes_20_porciento)
            {
                DesbloquearSniper();
                interesDiosa = interesDiosa * 1.2f;
                playerEsporas.RestarEsporas(valorSliderSniper);
                dialogosDiosaTienda1[2].SetActive(true);
                dialogosDiosaTienda1[1].SetActive(false);
                ActualizarPrecioAlabarda();
                StartCoroutine(DestroyObjectCoroutine(Tienda_Diosa_5, 7f));

            }
            else if (valorSliderSniper < precioBaseSniper * interes_30_porciento)
            {
                DesbloquearSniper();
                interesDiosa = interesDiosa * 1.3f;
                playerEsporas.RestarEsporas(valorSliderSniper);
                dialogosDiosaTienda1[2].SetActive(true);
                dialogosDiosaTienda1[1].SetActive(false);
                ActualizarPrecioSniper();
                StartCoroutine(DestroyObjectCoroutine(Tienda_Diosa_5, 7f));
                ActualizarPrecioAlabarda();
            }
            else if (valorSliderSniper >= precioBaseSniper)
            {
                DesbloquearSniper();
                dialogosDiosaTienda1[3].SetActive(true);
                dialogosDiosaTienda1[1].SetActive(false);
                StartCoroutine(DestroyObjectCoroutine(Tienda_Diosa_5, 7f));
                playerEsporas.RestarEsporas(valorSliderSniper);
                interesDiosa = 1;
                ActualizarPrecioSniper();

                extraSpores = valorSliderSniper - precioBaseSniper;

                StartCoroutine(bolsaEsporas.InstantiateEsporas(extraSpores));
                SaveExtraSpores();
                UpdateExtraSporesText();


            }
            else if (valorSliderSniper > precioBaseSniper && cantidadEsporas < precioBaseSniper)
            {
                esporasInsuficientes.SetActive(true);
                StartCoroutine(DestroyObjectCoroutine(esporasInsuficientes, 0.5f));
            }
        }
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

        BolsaEsporas bolsaEsporas = FindAnyObjectByType<BolsaEsporas>();

        PlayerEsporas playerEsporas = FindObjectOfType<PlayerEsporas>();

        int valorSliderMartillo = (int)sliderNegocaci�nMartillo.slider.value;
        
        if (playerEsporas != null)
        {
            int cantidadEsporas = playerEsporas.GetEsporas();

            if (cantidadEsporas < precioBaseMartilloGigante)
            {
                DesbloquearPistola();
                interesDiosa = 2f;
                Tienda_Diosa_15.SetActive(false);
            }
            else if (valorSliderMartillo >= precioBaseMartilloGigante)
            {
                Debug.Log("Desbloqueando Martillo");
                DesbloquearMartilloGigante();
                //dialogosDiosaTienda1[3].SetActive(true);
                //dialogosDiosaTienda1[1].SetActive(false);
                StartCoroutine(DestroyObjectCoroutine(Tienda_Diosa_15, 7f));
                playerEsporas.RestarEsporas(valorSliderMartillo);
                interesDiosa = 1;
                ActualizarPrecioMartillo();

                StartCoroutine(bolsaEsporas.InstantiateEsporas(extraSpores));
                SaveExtraSpores();

                float newExtraSpores = valorSliderMartillo - precioBaseMartilloGigante;
                UpdateExtraSporesText2(newExtraSpores);
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
        //menuNegoc�aci�nRevolver.SetActive(true);
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

    public void ComprarAumentoDa�o()
    {
        PlayerEsporas playerEsporas = FindObjectOfType<PlayerEsporas>();

        if (playerEsporas != null)
        {
            int cantidadEsporas = playerEsporas.GetEsporas();

            if (cantidadEsporas >= precioBaseAlmaDa�oReducido)
            {
                aumentarDa�o.ActivateAdditionalDamage();
                interesDiosa = 1;
                dialogosDiosaTienda1[4].SetActive(true);
                dialogosDiosaTienda1[0].SetActive(false);
                StartCoroutine(DestroyObjectCoroutine(Tienda_Diosa_5, 4f));
                StartCoroutine(DestroyObjectCoroutine(dialogosDiosaTienda1[4], 4f));
               
            }
            else
            {
                esporasInsuficientes.SetActive(true);
                StartCoroutine(DestroyObjectCoroutine(esporasInsuficientes, 0.5f));

            }
        }
        ActualizarPrecioAumentarda�o();
    }
    public void NegociarDa�oAumentado()
    {
        BolsaEsporas bolsaEsporas = FindAnyObjectByType<BolsaEsporas>();
        Debug.Log("Regateando...");

        PlayerEsporas playerEsporas = FindObjectOfType<PlayerEsporas>();

        if (playerEsporas != null)
        {
            int valorSliderDa�oAumentado = (int)sliderNegocaci�nDa�oAumentado.slider.value;
            int cantidadEsporas = playerEsporas.GetEsporas();


            if (valorSliderDa�oAumentado < precioBaseDa�oAumentado * interes_10_porciento && cantidadEsporas >= precioBaseDa�oAumentado * interes_10_porciento)
            {
                Debug.Log("Desbloqueando Alabarada 10");
                aumentarDa�o.ActivateAdditionalDamage();
                interesDiosa = interesDiosa * 1.1f;
                dialogosDiosaTienda1[2].SetActive(true);
                dialogosDiosaTienda1[1].SetActive(false);
                StartCoroutine(DestroyObjectCoroutine(Tienda_Diosa_5, 7f));
                playerEsporas.RestarEsporas(valorSliderDa�oAumentado);

                ActualizarPrecioAumentarda�o();
            }
            else if (valorSliderDa�oAumentado < precioBaseDa�oAumentado * interes_20_porciento && cantidadEsporas >= precioBaseDa�oAumentado * interes_20_porciento)
            {
                Debug.Log("Desbloqueando Alabarada 20%");
                aumentarDa�o.ActivateAdditionalDamage();
                interesDiosa = interesDiosa * 1.2f;
                playerEsporas.RestarEsporas(valorSliderDa�oAumentado);
                dialogosDiosaTienda1[2].SetActive(true);
                dialogosDiosaTienda1[1].SetActive(false);
                StartCoroutine(DestroyObjectCoroutine(Tienda_Diosa_5, 7f));

                ActualizarPrecioAumentarda�o();
            }
            else if (valorSliderDa�oAumentado < precioBaseDa�oAumentado * interes_30_porciento && cantidadEsporas >= precioBaseDa�oAumentado * interes_30_porciento)
            {
                Debug.Log("Desbloqueando Alabarada 30%");
                aumentarDa�o.ActivateAdditionalDamage();
                interesDiosa = interesDiosa * 1.3f;
                playerEsporas.RestarEsporas(valorSliderDa�oAumentado);
                dialogosDiosaTienda1[2].SetActive(true);
                dialogosDiosaTienda1[1].SetActive(false);

                ActualizarPrecioAlabarda();
                StartCoroutine(DestroyObjectCoroutine(Tienda_Diosa_5, 7f));
                ActualizarPrecioAumentarda�o();
            }
            else if (valorSliderDa�oAumentado >= precioBaseDa�oAumentado && cantidadEsporas >= precioBaseDa�oAumentado)
            {
                Debug.Log("Desbloqueando Alabarada");
                aumentarDa�o.ActivateAdditionalDamage();
                dialogosDiosaTienda1[3].SetActive(true);
                dialogosDiosaTienda1[1].SetActive(false);
                ActualizarPrecioAlabarda();
                StartCoroutine(DestroyObjectCoroutine(Tienda_Diosa_5, 7f));
                playerEsporas.RestarEsporas(valorSliderDa�oAumentado);
                interesDiosa = 1;
                ActualizarPrecioAumentarda�o();

                extraSpores = valorSliderDa�oAumentado - precioBaseDa�oAumentado;

                StartCoroutine(bolsaEsporas.InstantiateEsporas(extraSpores));
                SaveExtraSpores();
                UpdateExtraSporesText();


            }
            else if (valorSliderDa�oAumentado > precioBaseDa�oAumentado && cantidadEsporas < precioBaseDa�oAumentado)
            {
                esporasInsuficientes.SetActive(true);
                StartCoroutine(DestroyObjectCoroutine(esporasInsuficientes, 0.5f));
            }
            else if (valorSliderDa�oAumentado < precioBaseDa�oAumentado && cantidadEsporas < precioBaseDa�oAumentado)
            {
                esporasInsuficientes.SetActive(true);
                StartCoroutine(DestroyObjectCoroutine(esporasInsuficientes, 0.5f));
            }
        }
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
        Debug.Log("Alabarda Desbloqueada");
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


    public void BloquearRevolver()
    {
        revolverDesbloqueado = false;
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

    private void ActualizarPrecioAumentarda�o()
    {
        precioBaseDa�oAumentado = precioBaseDa�oAumentado * interesDiosa;

        if(precioDa�oAumentadoText != null)
        {
            precioDa�oAumentadoText.text = precioBaseDa�oAumentado.ToString();
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


