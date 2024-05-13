using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

public class Weapon_Wheel_Manager : MonoBehaviour
{
    public GameObject minimapa; 

    private float precioBasePistola = 250;
    private float precioBaseAlabarda = 77;
    private float precioBaseMartilloGigante = 120;
    private float precioBaseSniper = 100;
    private float precioBaseShotgun = 120;

    private float precioBaseAlmaDa�oReducido = 102;
    private float precioBaseDa�oAumentado = 94;
    private float precioBaseAtaqueArea = 102;

    private float precioBaseEmpujon = 103;
    private float precioBaseBombaHumo = 150;
    private float precioBaseMolotov = 123;

    float interes_10_porciento = 0.90f;
    float interes_20_porciento = 0.80f;
    float interes_30_porciento = 0.70f;

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

    [Header("Precios Armas & Interfaz Negociaciones")]
    public TMP_Text precioAlabardaText;
    public TMP_Text precioMartilloText;
    public TMP_Text precioSniperText;
    public TMP_Text precioSniper2Text;
    public TMP_Text precioShotgunText;
    public TMP_Text precioShotgun2Text;
    public TMP_Text precioDa�oAumentadoText;
    public TMP_Text precioDa�oReducidoText;
    public TMP_Text precioAtaqueAreaText;

    [Header("Precio Negocaci�n Text")]
    public TMP_Text PrecioNegociado;
    public TMP_Text PrecioNegociadoSniper;
    public TMP_Text PrecioNegociadoSniper2;
    public TMP_Text PrecioNegociadoDa�oAumentado;
    public TMP_Text PrecioNegociadoDa�oReducido;
    public TMP_Text PrecioNegociadoAtaqueEnArea;
    public TMP_Text PrecioNegociadoMartilo;
    public TMP_Text PrecioNegociadoShotgun;
    public TMP_Text PrecioNegociadoShotgun2;

    [Header("Sliders Negociaci�n")]
    public SliderScript sliderNegocaci�nAlabarda;
    public SliderScript sliderNegocaci�nMartillo;
    public SliderScript sliderNegocaci�nSniper;
    public SliderScript sliderNegocaci�nSnipe2;
    public SliderScript sliderNegocaci�nShotgun;
    public SliderScript sliderNegocaci�nShotgun2;
    public SliderScript sliderNegocaci�nDa�oAumentado;
    public SliderScript sliderNegocaci�nDa�oReducido;
    public SliderScript sliderNegocaci�nAtaqueArea;

    public SliderScript sliderBolsaEsporasExtra;
    [SerializeField] private float extraSpores = 0;

    public static float interesDiosa = 1;

    [Header("Esporas Extra")]
    public TMP_Text extraSporesText;

    private const string esporasExtra = "esporasExtra";

    private bool negocaci�nRealizada = false;
    private bool compraRealizada = false;

    SpawnerManager gameManager;

    public AumentarDa�oAEnemigos aumentarDa�o;

    public PlayerAreaDamage areaAttack;

    public VidaPlayer reducirDa�o;

    public GameObject inventory;

    public Animator equippedPopUps;

    public GameObject extraEsporasPopUp;
    
    public GameObject [] dialogosDiosaTienda1;

    public GameObject [] dialogosDiosaTienda2;

    public GameObject[] dialogosDiosaTienda3;

    FreezeEnemies[] freezeEnemies;

    public GameObject bolsaExporasExtra;

    public Button [] botonesADesactivar;

    private EnemyHealth enemyhealth;
    

    void SaveExtraSpores()
    {
        PlayerPrefs.SetFloat(esporasExtra, extraSpores);
    }

    void LoadExtraSpores()
    {
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
        ActualizarPrecioSniper();
        ActualizarPrecioSniper2();
        ActualizarPrecioAumentarda�o();
        ActualizarPrecioMartillo();
        ActualizarPrecioShotgun();
        ActualizarPrecioShotgun2();
        ActualizarPrecioDa�oReducido();
        ActualizarPrecioAtaqueArea();
        animator = GetComponent<Animator>();

        freezeEnemies = FindObjectsOfType<FreezeEnemies>();
        gameManager = FindObjectOfType<SpawnerManager>();
        enemyhealth= FindAnyObjectByType<EnemyHealth>();
    }
    void Update()
    {

        if(extraSpores >= 40)
        {
            StartCoroutine(ActivateObjectWithDelay(extraEsporasPopUp, 7f));
            StartCoroutine(ActivateObjectAnimationwithDelay("PopUpRecomensaExtraAnimation", 7f));
        }
        if(sliderBolsaEsporasExtra != null)
        {
            sliderBolsaEsporasExtra.slider.value = extraSpores;
        }
        if (sliderNegocaci�nAlabarda != null)
        {
            int valorSlider = (int)sliderNegocaci�nAlabarda.slider.value;
            PrecioNegociado.text = "" + valorSlider ;
        }
        if (sliderNegocaci�nSniper != null)
        {
            int valorSliderSniper = (int)sliderNegocaci�nSniper.slider.value;
            PrecioNegociadoSniper.text = "" + valorSliderSniper;
        }
        if (sliderNegocaci�nSnipe2 != null)
        {
            int valorSliderSniper2 = (int)sliderNegocaci�nSnipe2.slider.value;
            PrecioNegociadoSniper2.text = "" + valorSliderSniper2;
        }
        if (sliderNegocaci�nMartillo != null)
        {
            int valorSliderMartillo = (int)sliderNegocaci�nMartillo.slider.value;
            PrecioNegociadoMartilo.text = "" + valorSliderMartillo;
        }
        if (sliderNegocaci�nDa�oAumentado != null)
        {
            int valorSliderMartillo = (int)sliderNegocaci�nDa�oAumentado.slider.value;
            PrecioNegociadoDa�oAumentado.text = "" + valorSliderMartillo;
        }

        if (sliderNegocaci�nShotgun != null)
        {
            int valorSliderShotgun = (int)sliderNegocaci�nShotgun.slider.value;
            PrecioNegociadoShotgun.text = "" + valorSliderShotgun;
        }
        if (sliderNegocaci�nShotgun2 != null)
        {
            int valorSliderShotgun = (int)sliderNegocaci�nShotgun2.slider.value;
            PrecioNegociadoShotgun2.text = "" + valorSliderShotgun;
        }
        if (sliderNegocaci�nDa�oReducido != null)
        {
            int valorSliderDa�oReducidon = (int)sliderNegocaci�nDa�oReducido.slider.value;
            PrecioNegociadoDa�oReducido.text = "" + valorSliderDa�oReducidon;
        }
        if (sliderNegocaci�nAtaqueArea != null)
        {
            int valorSliderDa�oReducidon = (int)sliderNegocaci�nAtaqueArea.slider.value;
            PrecioNegociadoAtaqueEnArea.text = "" + valorSliderDa�oReducidon;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            popUpInicial.SetActive(false);

            // Si el men� est� activo, se desactiva
            if (menuActivo)
            {
                wheelMenu.SetActive(false);
                menuActivo = false;
            }
            // Si el men� no est� activo, se activa
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
        animator.enabled = true;
        animator.Play("Animaci�nNegocacionAlabarda");
        StartCoroutine(ActivateObjectWithDelay(dialogosDiosaTienda1[1], 2f));
        dialogosDiosaTienda1[0].SetActive(false);
    }
    public void ActivarNegocaci�nSniper()
    {
        animator.enabled = true;
        animator.Play("Animaci�nNegocacionSniper");
        StartCoroutine(ActivateObjectWithDelay(dialogosDiosaTienda1[1], 2f));
        dialogosDiosaTienda1[0].SetActive(false);
    }
    public void ActivarNegocaci�nSnipe2()
    {
        animator.enabled = true;
        animator.Play("Animaci�nNegocacionSniper2");
        StartCoroutine(ActivateObjectWithDelay(dialogosDiosaTienda3[1], 2f));
        dialogosDiosaTienda3[0].SetActive(false);
    }
    public void ActivarNegociaci�nDa�oAumentado()
    {
        animator.enabled = true;
        animator.Play("Animaci�nNegocacionAumentoDa�o");
        StartCoroutine(ActivateObjectWithDelay(dialogosDiosaTienda1[1], 2f));
        dialogosDiosaTienda1[0].SetActive(false);
    }
    public void ActivarNegocaci�nMartillo()
    {
        animator.enabled = true;
        StartCoroutine(ActivateObjectWithDelay(dialogosDiosaTienda2[1], 2f));
        dialogosDiosaTienda2[0].SetActive(false);
        animator.Play("Animaci�nNegocacionMartillo");
    }
    public void ActivarNegocaci�nShotgun()
    {
        animator.enabled = true;
        StartCoroutine(ActivateObjectWithDelay(dialogosDiosaTienda2[1], 2f));
        dialogosDiosaTienda2[0].SetActive(false);
        animator.Play("Animaci�nNegocacionShotgun");
    }
    public void ActivarNegocaci�nShotgun2()
    {
        animator.enabled = true;
        StartCoroutine(ActivateObjectWithDelay(dialogosDiosaTienda3[1], 2f));
        dialogosDiosaTienda3[0].SetActive(false);
        animator.Play("Animaci�nNegocacionShotgun2");
    }
    public void ActivarNegocaci�nDa�oReducido()
    {
        animator.enabled = true;
        StartCoroutine(ActivateObjectWithDelay(dialogosDiosaTienda2[1], 2f));
        dialogosDiosaTienda2[0].SetActive(false);
        animator.Play("Animaci�nNegocacionDa�oReducido");
    }
    public void ActivarNegocaci�nAtaqueArea()
    {
        animator.enabled = true;
        StartCoroutine(ActivateObjectWithDelay(dialogosDiosaTienda3[1], 2f));
        dialogosDiosaTienda3[0].SetActive(false);
        animator.Play("Animaci�nNegocacionAtaqueArea");
    }
    public void ComprarAlabarda()
    {
        
        PlayerEsporas playerEsporas = FindObjectOfType<PlayerEsporas>();


        if (playerEsporas != null)
        {
            float cantidadEsporas = playerEsporas.GetEsporas();

            if (cantidadEsporas >= precioBaseAlabarda)
            {
                for (int i = 0; i < botonesADesactivar.Length; i++)
                {
                    botonesADesactivar[i].enabled = false;
                }
                for (int i = 0; i < freezeEnemies.Length; i++)
                {
                    if (freezeEnemies[i] != null)
                    {
                        StartCoroutine(ActivarComponentesEnemigosConRetraso(i, 4f));
                    }
                }
                DesbloquearAlabarda();
                interesDiosa = 1;
                dialogosDiosaTienda1[4].SetActive(true);
                dialogosDiosaTienda1[0].SetActive(false);
                StartCoroutine(DestroyObjectCoroutine(Tienda_Diosa_5, 4f));
                StartCoroutine(DestroyObjectCoroutine(dialogosDiosaTienda1[4], 4f));
                StartCoroutine(EnableObjectWithDelay(gameManager, 4f));
                StartCoroutine(DestroyObjectCoroutine(bolsaExporasExtra, 4f));
                playerEsporas.RestarEsporas(precioBaseAlabarda);
                ActualizarPrecioAlabarda();

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
        animator.enabled = false;
        BolsaEsporas bolsaEsporas = FindAnyObjectByType<BolsaEsporas>();
        Debug.Log("Regateando...");

        PlayerEsporas playerEsporas = FindObjectOfType<PlayerEsporas>();
            

        if (playerEsporas != null)
        {
            int valorSliderAlabarda = (int)sliderNegocaci�nAlabarda.slider.value;
            float cantidadEsporas = playerEsporas.GetEsporas();

            //Precio Alabarda = 77
            if (valorSliderAlabarda == precioBaseAlabarda && cantidadEsporas >= precioBaseAlabarda)
            {
                Debug.Log("Pagando precio base");
                for (int i = 0; i < freezeEnemies.Length; i++)
                {
                    if (freezeEnemies[i] != null)
                    {
                        StartCoroutine(ActivarComponentesEnemigosConRetraso(i, 7f));
                    }
                }

                DesbloquearSniper();
                dialogosDiosaTienda1[4].SetActive(true);
                dialogosDiosaTienda1[1].SetActive(false);
                StartCoroutine(DestroyObjectCoroutine(Tienda_Diosa_5, 7f));
                StartCoroutine(EnableObjectWithDelay(gameManager, 7f));
                playerEsporas.RestarEsporas(valorSliderAlabarda);
                interesDiosa = 1;
                ActualizarPrecioSniper();
                StartCoroutine(DestroyObjectCoroutine(bolsaExporasExtra, 7f));
                

                extraSpores = valorSliderAlabarda - precioBaseAlabarda;

                StartCoroutine(bolsaEsporas.InstantiateEsporas(extraSpores));
                SaveExtraSpores();
                UpdateExtraSporesText();
            }
            else if (valorSliderAlabarda >= precioBaseAlabarda * interes_10_porciento && valorSliderAlabarda < precioBaseAlabarda && cantidadEsporas >= precioBaseAlabarda * interes_10_porciento)
            {
                Debug.Log("Desbloqueando Alabarada 10");
                for (int i = 0; i < freezeEnemies.Length; i++)
                {
                    if (freezeEnemies[i] != null)
                    {
                        StartCoroutine(ActivarComponentesEnemigosConRetraso(i, 7f));
                    }
                }
                bolsaExporasExtra.SetActive(false);
                StartCoroutine(DestroyObjectCoroutine(bolsaExporasExtra, 7f));
                StartCoroutine(EnableObjectWithDelay(gameManager, 7f));
                DesbloquearAlabarda();
                interesDiosa = interesDiosa * 1.1f;
                dialogosDiosaTienda1[2].SetActive(true);
                dialogosDiosaTienda1[1].SetActive(false);
                StartCoroutine(DestroyObjectCoroutine(Tienda_Diosa_5, 7f));
                playerEsporas.RestarEsporas(valorSliderAlabarda);
                
                ActualizarPrecioAlabarda();
            }
            else if (valorSliderAlabarda >= precioBaseAlabarda* interes_20_porciento && valorSliderAlabarda < precioBaseAlabarda && cantidadEsporas >= precioBaseAlabarda * interes_20_porciento)
            {
                Debug.Log("Desbloqueando Alabarada 20%");
                for (int i = 0; i < freezeEnemies.Length; i++)
                {
                    if (freezeEnemies[i] != null)
                    {
                        StartCoroutine(ActivarComponentesEnemigosConRetraso(i, 7f));
                    }
                }
                DesbloquearAlabarda();
                interesDiosa = interesDiosa * 1.2f;
                playerEsporas.RestarEsporas(valorSliderAlabarda);
                dialogosDiosaTienda1[2].SetActive(true);
                dialogosDiosaTienda1[1].SetActive(false);
                StartCoroutine(EnableObjectWithDelay(gameManager, 7f));
                StartCoroutine(DestroyObjectCoroutine(Tienda_Diosa_5, 7f));
                StartCoroutine(DestroyObjectCoroutine(bolsaExporasExtra, 7f));
                ActualizarPrecioAlabarda();
            }
            else if (valorSliderAlabarda >= precioBaseAlabarda * interes_30_porciento && valorSliderAlabarda < precioBaseAlabarda && cantidadEsporas >= precioBaseAlabarda * interes_30_porciento)
            {
                Debug.Log("Desbloqueando Alabarada 30%");
                for (int i = 0; i < freezeEnemies.Length; i++)
                {
                    if (freezeEnemies[i] != null)
                    {
                        StartCoroutine(ActivarComponentesEnemigosConRetraso(i, 7f));
                    }
                }
                DesbloquearAlabarda();
                interesDiosa = interesDiosa * 1.3f;
                playerEsporas.RestarEsporas(valorSliderAlabarda);
                dialogosDiosaTienda1[2].SetActive(true);
                dialogosDiosaTienda1[1].SetActive(false);
                ActualizarPrecioAlabarda();
                StartCoroutine(EnableObjectWithDelay(gameManager, 7f));
                StartCoroutine(DestroyObjectCoroutine(Tienda_Diosa_5, 7f));
                StartCoroutine(DestroyObjectCoroutine(bolsaExporasExtra, 7f));
                ActualizarPrecioAlabarda();
            }
            else if(valorSliderAlabarda > precioBaseAlabarda && cantidadEsporas >= precioBaseAlabarda)
            {
                Debug.Log("Desbloqueando Alabarada");
                for (int i = 0; i < freezeEnemies.Length; i++)
                {
                    if (freezeEnemies[i] != null)
                    {
                        StartCoroutine(ActivarComponentesEnemigosConRetraso(i, 7f));
                    }
                }
                DesbloquearAlabarda();
                dialogosDiosaTienda1[3].SetActive(true);
                dialogosDiosaTienda1[1].SetActive(false);
                ActualizarPrecioAlabarda();
                StartCoroutine(DestroyObjectCoroutine(Tienda_Diosa_5, 7f));
                StartCoroutine(EnableObjectWithDelay(gameManager, 7f));
                playerEsporas.RestarEsporas(valorSliderAlabarda);
                interesDiosa = 1;
                ActualizarPrecioAlabarda();
                StartCoroutine(DestroyObjectCoroutine(bolsaExporasExtra, 7f));
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
            float cantidadEsporas = playerEsporas.GetEsporas();

            if (cantidadEsporas >= precioBaseSniper)
            {
                for (int i = 0; i < botonesADesactivar.Length; i++)
                {
                    botonesADesactivar[i].enabled = false;
                }
                for (int i = 0; i < freezeEnemies.Length; i++)
                {
                    if (freezeEnemies[i] != null)
                    {
                        StartCoroutine(ActivarComponentesEnemigosConRetraso(i, 4f));
                    }
                }
                DesbloquearSniper();
                interesDiosa = 1;
                dialogosDiosaTienda1[4].SetActive(true);
                dialogosDiosaTienda1[0].SetActive(false);
                StartCoroutine(EnableObjectWithDelay(gameManager, 4f));
                StartCoroutine(DestroyObjectCoroutine(Tienda_Diosa_5, 4f));
                StartCoroutine(DestroyObjectCoroutine(dialogosDiosaTienda1[4], 4f));
                StartCoroutine(DestroyObjectCoroutine(bolsaExporasExtra, 4f));
                playerEsporas.RestarEsporas(precioBaseSniper);
                ActualizarPrecioSniper();

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
        animator.enabled = false;
        BolsaEsporas bolsaEsporas = FindAnyObjectByType<BolsaEsporas>();
        Debug.Log("Regateando...");

        PlayerEsporas playerEsporas = FindObjectOfType<PlayerEsporas>();

        if (playerEsporas != null)
        {
            int valorSliderSniper = (int)sliderNegocaci�nSniper.slider.value;
            float cantidadEsporas = playerEsporas.GetEsporas();

            if (valorSliderSniper == precioBaseSniper && cantidadEsporas >= precioBaseSniper)
            {
                Debug.Log("Pagando precio base");
                for (int i = 0; i < freezeEnemies.Length; i++)
                {
                    if (freezeEnemies[i] != null)
                    {
                        StartCoroutine(ActivarComponentesEnemigosConRetraso(i, 7f));
                    }
                }
                DesbloquearSniper();
                dialogosDiosaTienda1[4].SetActive(true);
                dialogosDiosaTienda1[1].SetActive(false);
                StartCoroutine(DestroyObjectCoroutine(Tienda_Diosa_5, 7f));
                StartCoroutine(EnableObjectWithDelay(gameManager, 7f));
                playerEsporas.RestarEsporas(valorSliderSniper);
                interesDiosa = 1;
                ActualizarPrecioSniper();
                StartCoroutine(DestroyObjectCoroutine(bolsaExporasExtra, 7f));

                extraSpores = valorSliderSniper - precioBaseSniper;

                StartCoroutine(bolsaEsporas.InstantiateEsporas(extraSpores));
                SaveExtraSpores();
                UpdateExtraSporesText();
            }
            else if (valorSliderSniper >= precioBaseSniper * interes_10_porciento && valorSliderSniper < precioBaseSniper && cantidadEsporas >= precioBaseSniper * interes_10_porciento)
            {
                Debug.Log("Pagando 10% Menos");
                for (int i = 0; i < freezeEnemies.Length; i++)
                {
                    if (freezeEnemies[i] != null)
                    {
                        StartCoroutine(ActivarComponentesEnemigosConRetraso(i, 7f));
                    }
                }
                DesbloquearSniper();
                interesDiosa = interesDiosa * 1.1f;
                dialogosDiosaTienda1[2].SetActive(true);
                dialogosDiosaTienda1[1].SetActive(false);
                StartCoroutine(DestroyObjectCoroutine(Tienda_Diosa_5, 7f));
                StartCoroutine(EnableObjectWithDelay(gameManager, 7f));
                playerEsporas.RestarEsporas(valorSliderSniper);
                StartCoroutine(DestroyObjectCoroutine(bolsaExporasExtra, 7f));
                ActualizarPrecioSniper();
            }
            else if (valorSliderSniper >= precioBaseSniper * interes_20_porciento && valorSliderSniper < precioBaseSniper && cantidadEsporas >= precioBaseSniper * interes_20_porciento)
            {
                Debug.Log("Pagando 20% Menos");
                for (int i = 0; i < freezeEnemies.Length; i++)
                {
                    if (freezeEnemies[i] != null)
                    {
                        StartCoroutine(ActivarComponentesEnemigosConRetraso(i, 7f));
                    }
                }
                DesbloquearSniper();
                interesDiosa = interesDiosa * 1.2f;
                playerEsporas.RestarEsporas(valorSliderSniper);
                dialogosDiosaTienda1[2].SetActive(true);
                dialogosDiosaTienda1[1].SetActive(false);
                ActualizarPrecioSniper();
                StartCoroutine(DestroyObjectCoroutine(bolsaExporasExtra, 7f));
                StartCoroutine(DestroyObjectCoroutine(Tienda_Diosa_5, 7f));
                StartCoroutine(EnableObjectWithDelay(gameManager, 7f));

            }
            else if (valorSliderSniper >= precioBaseSniper * interes_30_porciento && valorSliderSniper < precioBaseSniper && cantidadEsporas >= precioBaseSniper * interes_30_porciento)
            {
                Debug.Log("Pagando 30% Menos");
                for (int i = 0; i < freezeEnemies.Length; i++)
                {
                    if (freezeEnemies[i] != null)
                    {
                        StartCoroutine(ActivarComponentesEnemigosConRetraso(i, 7f));
                    }
                }
                DesbloquearSniper();
                interesDiosa = interesDiosa * 1.3f;
                playerEsporas.RestarEsporas(valorSliderSniper);
                dialogosDiosaTienda1[2].SetActive(true);
                dialogosDiosaTienda1[1].SetActive(false);
                ActualizarPrecioSniper();
                StartCoroutine(DestroyObjectCoroutine(Tienda_Diosa_5, 7f));
                StartCoroutine(DestroyObjectCoroutine(bolsaExporasExtra, 7f));
                StartCoroutine(EnableObjectWithDelay(gameManager, 7f));
            }
            else if (valorSliderSniper > precioBaseSniper && cantidadEsporas > precioBaseSniper)
            {
                Debug.Log("Pagando extra");
                for (int i = 0; i < freezeEnemies.Length; i++)
                {
                    if (freezeEnemies[i] != null)
                    {
                        StartCoroutine(ActivarComponentesEnemigosConRetraso(i, 7f));
                    }
                }
                DesbloquearSniper();
                dialogosDiosaTienda1[3].SetActive(true);
                dialogosDiosaTienda1[1].SetActive(false);
                StartCoroutine(DestroyObjectCoroutine(Tienda_Diosa_5, 7f));
                playerEsporas.RestarEsporas(valorSliderSniper);
                interesDiosa = 1;
                ActualizarPrecioSniper();
                StartCoroutine(DestroyObjectCoroutine(bolsaExporasExtra, 7f));
                StartCoroutine(EnableObjectWithDelay(gameManager, 7f));

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
    public void ComprarSniper2()
    {
        
        PlayerEsporas playerEsporas = FindObjectOfType<PlayerEsporas>();

        if (playerEsporas != null)
        {

            float cantidadEsporas = playerEsporas.GetEsporas();
           
            if (cantidadEsporas >= precioBaseSniper)
            {
                int valorSliderSniper = (int)sliderNegocaci�nSniper.slider.value;
                for (int i = 0; i < botonesADesactivar.Length; i++)
                {
                    botonesADesactivar[i].enabled = false;
                }

                for (int i = 0; i < freezeEnemies.Length; i++)
                {
                    if (freezeEnemies[i] != null)
                    {
                        StartCoroutine(ActivarComponentesEnemigosConRetraso(i, 4f));
                    }
                }
                DesbloquearSniper();
                interesDiosa = 1;
                dialogosDiosaTienda3[4].SetActive(true);
                dialogosDiosaTienda3[0].SetActive(false);
                StartCoroutine(DestroyObjectCoroutine(Tienda_Diosa_15, 4f));
                StartCoroutine(DestroyObjectCoroutine(dialogosDiosaTienda3[4], 4f));
                StartCoroutine(DestroyObjectCoroutine(bolsaExporasExtra, 4f));
                StartCoroutine(EnableObjectWithDelay(gameManager, 4f));
                playerEsporas.RestarEsporas(precioBaseSniper);
                ActualizarPrecioSniper2();

            }
            else
            {
                esporasInsuficientes.SetActive(true);
                StartCoroutine(DestroyObjectCoroutine(esporasInsuficientes, 0.5f));

            }
        }
        ActualizarPrecioSniper();
    }
   
    public void NegociarSniper2()
    {
        animator.enabled = false;
        BolsaEsporas bolsaEsporas = FindAnyObjectByType<BolsaEsporas>();
        Debug.Log("Regateando...");

        PlayerEsporas playerEsporas = FindObjectOfType<PlayerEsporas>();

        if (playerEsporas != null)
        {
            int valorSliderSniper = (int)sliderNegocaci�nSnipe2.slider.value;
            float cantidadEsporas = playerEsporas.GetEsporas();

            if (valorSliderSniper == precioBaseSniper && cantidadEsporas >= precioBaseSniper)
            {
                Debug.Log("Pagando precio base");
                for (int i = 0; i < freezeEnemies.Length; i++)
                {
                    if (freezeEnemies[i] != null)
                    {
                        StartCoroutine(ActivarComponentesEnemigosConRetraso(i, 7f));
                    }
                }
                DesbloquearSniper();
                dialogosDiosaTienda3[4].SetActive(true);
                dialogosDiosaTienda3[1].SetActive(false);
                StartCoroutine(DestroyObjectCoroutine(Tienda_Diosa_15, 7f));
                StartCoroutine(EnableObjectWithDelay(gameManager, 7f));
                playerEsporas.RestarEsporas(valorSliderSniper);
                interesDiosa = 1;
                ActualizarPrecioSniper2();
                StartCoroutine(DestroyObjectCoroutine(bolsaExporasExtra, 7f));

                extraSpores = valorSliderSniper - precioBaseSniper;

                StartCoroutine(bolsaEsporas.InstantiateEsporas(extraSpores));
                SaveExtraSpores();
                UpdateExtraSporesText();
            }
            else if (valorSliderSniper >= precioBaseSniper * interes_10_porciento && valorSliderSniper < precioBaseSniper && cantidadEsporas >= precioBaseSniper * interes_10_porciento)
            {
                Debug.Log("Pagando 10% Menos");
                for (int i = 0; i < freezeEnemies.Length; i++)
                {
                    if (freezeEnemies[i] != null)
                    {
                        StartCoroutine(ActivarComponentesEnemigosConRetraso(i, 7f));
                    }
                }
                DesbloquearSniper();
                interesDiosa = interesDiosa * 1.1f;
                dialogosDiosaTienda3[2].SetActive(true);
                dialogosDiosaTienda3[1].SetActive(false);
                StartCoroutine(DestroyObjectCoroutine(Tienda_Diosa_15, 7f));
                StartCoroutine(EnableObjectWithDelay(gameManager, 7f));
                playerEsporas.RestarEsporas(valorSliderSniper);
                StartCoroutine(DestroyObjectCoroutine(bolsaExporasExtra, 7f));
                ActualizarPrecioSniper2();
            }
            else if (valorSliderSniper >= precioBaseSniper * interes_20_porciento && valorSliderSniper < precioBaseSniper && cantidadEsporas >= precioBaseSniper * interes_20_porciento)
            {
                Debug.Log("Pagando 20% Menos");
                for (int i = 0; i < freezeEnemies.Length; i++)
                {
                    if (freezeEnemies[i] != null)
                    {
                        StartCoroutine(ActivarComponentesEnemigosConRetraso(i, 7f));
                    }
                }
                DesbloquearSniper();
                interesDiosa = interesDiosa * 1.2f;
                playerEsporas.RestarEsporas(valorSliderSniper);
                dialogosDiosaTienda3[2].SetActive(true);
                dialogosDiosaTienda3[1].SetActive(false);
                ActualizarPrecioSniper2();
                StartCoroutine(DestroyObjectCoroutine(bolsaExporasExtra, 7f));
                StartCoroutine(EnableObjectWithDelay(gameManager, 7f));
                StartCoroutine(DestroyObjectCoroutine(Tienda_Diosa_15, 7f));

            }
            else if (valorSliderSniper >= precioBaseSniper * interes_30_porciento && valorSliderSniper < precioBaseSniper && cantidadEsporas >= precioBaseSniper * interes_30_porciento)
            {
                Debug.Log("Pagando 30% Menos");
                for (int i = 0; i < freezeEnemies.Length; i++)
                {
                    if (freezeEnemies[i] != null)
                    {
                        StartCoroutine(ActivarComponentesEnemigosConRetraso(i, 7f));
                    }
                }
                DesbloquearSniper();
                interesDiosa = interesDiosa * 1.3f;
                playerEsporas.RestarEsporas(valorSliderSniper);
                dialogosDiosaTienda3[2].SetActive(true);
                dialogosDiosaTienda3[1].SetActive(false);
                ActualizarPrecioSniper2();
                StartCoroutine(DestroyObjectCoroutine(Tienda_Diosa_15, 7f));
                StartCoroutine(EnableObjectWithDelay(gameManager, 7f));
                ActualizarPrecioAlabarda();
                StartCoroutine(DestroyObjectCoroutine(bolsaExporasExtra, 7f));
            }
            else if (valorSliderSniper > precioBaseSniper)
            {
                for (int i = 0; i < freezeEnemies.Length; i++)
                {
                    if (freezeEnemies[i] != null)
                    {
                        StartCoroutine(ActivarComponentesEnemigosConRetraso(i, 7f));
                    }
                }
                DesbloquearSniper();
                dialogosDiosaTienda3[3].SetActive(true);
                dialogosDiosaTienda3[1].SetActive(false);
                StartCoroutine(DestroyObjectCoroutine(Tienda_Diosa_15, 7f));
                playerEsporas.RestarEsporas(valorSliderSniper);
                interesDiosa = 1;
                ActualizarPrecioSniper2();
                StartCoroutine(DestroyObjectCoroutine(bolsaExporasExtra, 7f));
                StartCoroutine(EnableObjectWithDelay(gameManager, 7f));

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
            float cantidadEsporas = playerEsporas.GetEsporas();
            
            if (cantidadEsporas >= precioBaseMartilloGigante)
            {
                for (int i = 0; i < botonesADesactivar.Length; i++)
                {
                    botonesADesactivar[i].enabled = false;
                }
                for (int i = 0; i < freezeEnemies.Length; i++)
                {
                    if (freezeEnemies[i] != null)
                    {
                        StartCoroutine(ActivarComponentesEnemigosConRetraso(i, 4f));
                    }
                }
                DesbloquearMartilloGigante();
                interesDiosa = 1;
                dialogosDiosaTienda2[4].SetActive(true);
                dialogosDiosaTienda2[0].SetActive(false);
                StartCoroutine(DestroyObjectCoroutine(Tienda_Diosa_10, 4f));
                StartCoroutine(DestroyObjectCoroutine(dialogosDiosaTienda2[4], 4f));
                StartCoroutine(DestroyObjectCoroutine(bolsaExporasExtra, 4f));
                StartCoroutine(EnableObjectWithDelay(gameManager, 4f));
                playerEsporas.RestarEsporas(precioBaseMartilloGigante);

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
        animator.enabled = false;
        BolsaEsporas bolsaEsporas = FindAnyObjectByType<BolsaEsporas>();
        Debug.Log("Regateando...");

        PlayerEsporas playerEsporas = FindObjectOfType<PlayerEsporas>();

        if (playerEsporas != null)
        {
            int valorSliderMartillo = (int)sliderNegocaci�nMartillo.slider.value;
            float cantidadEsporas = playerEsporas.GetEsporas();
           
            if(valorSliderMartillo == precioBaseMartilloGigante && cantidadEsporas >= precioBaseMartilloGigante)
            {
                Debug.Log("Pagando precio base");
                for (int i = 0; i < freezeEnemies.Length; i++)
                {
                    if (freezeEnemies[i] != null)
                    {
                        StartCoroutine(ActivarComponentesEnemigosConRetraso(i, 7f));
                    }
                }

                DesbloquearMartilloGigante();
                dialogosDiosaTienda2[4].SetActive(true);
                dialogosDiosaTienda2[1].SetActive(false);
                StartCoroutine(DestroyObjectCoroutine(Tienda_Diosa_5, 7f));
                StartCoroutine(EnableObjectWithDelay(gameManager, 7f));
                playerEsporas.RestarEsporas(valorSliderMartillo);
                interesDiosa = 1;
                ActualizarPrecioMartillo();
                

                extraSpores = valorSliderMartillo - precioBaseMartilloGigante;

                StartCoroutine(bolsaEsporas.InstantiateEsporas(extraSpores));
                SaveExtraSpores();
                UpdateExtraSporesText();
            }
            else if (valorSliderMartillo >= precioBaseMartilloGigante * interes_10_porciento && valorSliderMartillo < precioBaseMartilloGigante && cantidadEsporas >= precioBaseMartilloGigante * interes_10_porciento)
            {
                for (int i = 0; i < freezeEnemies.Length; i++)
                {
                    if (freezeEnemies[i] != null)
                    {
                        StartCoroutine(ActivarComponentesEnemigosConRetraso(i, 7f));
                    }
                }
                DesbloquearMartilloGigante();
                interesDiosa = interesDiosa * 1.1f;
                dialogosDiosaTienda2[2].SetActive(true);
                dialogosDiosaTienda2[1].SetActive(false);
                StartCoroutine(DestroyObjectCoroutine(Tienda_Diosa_10, 7f));
                StartCoroutine(EnableObjectWithDelay(gameManager, 7f));
                playerEsporas.RestarEsporas(valorSliderMartillo);
                StartCoroutine(DestroyObjectCoroutine(bolsaExporasExtra, 7f));
                ActualizarPrecioMartillo();
            }
            else if (valorSliderMartillo >= precioBaseMartilloGigante * interes_20_porciento && valorSliderMartillo < precioBaseMartilloGigante && cantidadEsporas >= precioBaseMartilloGigante * interes_20_porciento)
            {
                for (int i = 0; i < freezeEnemies.Length; i++)
                {
                    if (freezeEnemies[i] != null)
                    {
                        StartCoroutine(ActivarComponentesEnemigosConRetraso(i, 7f));
                    }
                }
                DesbloquearMartilloGigante();
                interesDiosa = interesDiosa * 1.2f;
                playerEsporas.RestarEsporas(valorSliderMartillo);
                dialogosDiosaTienda2[2].SetActive(true);
                dialogosDiosaTienda2[1].SetActive(false);
                ActualizarPrecioMartillo();
                StartCoroutine(DestroyObjectCoroutine(bolsaExporasExtra, 7f));
                StartCoroutine(EnableObjectWithDelay(gameManager, 7f));
                StartCoroutine(DestroyObjectCoroutine(Tienda_Diosa_10, 7f));

            }
            else if (valorSliderMartillo >= precioBaseMartilloGigante * interes_30_porciento && valorSliderMartillo < precioBaseMartilloGigante && cantidadEsporas >= precioBaseMartilloGigante * interes_30_porciento)
            {
                for (int i = 0; i < freezeEnemies.Length; i++)
                {
                    if (freezeEnemies[i] != null)
                    {
                        StartCoroutine(ActivarComponentesEnemigosConRetraso(i, 7f));
                    }
                }
                DesbloquearMartilloGigante();
                interesDiosa = interesDiosa * 1.3f;
                playerEsporas.RestarEsporas(valorSliderMartillo);
                dialogosDiosaTienda2[2].SetActive(true);
                dialogosDiosaTienda2[1].SetActive(false);
                ActualizarPrecioMartillo();
                StartCoroutine(DestroyObjectCoroutine(Tienda_Diosa_10, 7f));
                StartCoroutine(DestroyObjectCoroutine(bolsaExporasExtra, 7f));
                StartCoroutine(EnableObjectWithDelay(gameManager, 7f));
            }
            else if (valorSliderMartillo > precioBaseMartilloGigante)
            {
                for (int i = 0; i < freezeEnemies.Length; i++)
                {
                    if (freezeEnemies[i] != null)
                    {
                        StartCoroutine(ActivarComponentesEnemigosConRetraso(i, 7f));
                    }
                }
                DesbloquearMartilloGigante();
                dialogosDiosaTienda2[3].SetActive(true);
                dialogosDiosaTienda2[1].SetActive(false);
                StartCoroutine(DestroyObjectCoroutine(Tienda_Diosa_10, 7f));
                playerEsporas.RestarEsporas(valorSliderMartillo);
                interesDiosa = 1;
                ActualizarPrecioMartillo();
                StartCoroutine(DestroyObjectCoroutine(bolsaExporasExtra, 7f));
                StartCoroutine(EnableObjectWithDelay(gameManager, 7f));


                float newExtraSpores = valorSliderMartillo - precioBaseMartilloGigante;
                UpdateExtraSporesText2(newExtraSpores);
                StartCoroutine(DestroyObjectCoroutine(bolsaExporasExtra, 7f));
                StartCoroutine(bolsaEsporas.InstantiateEsporas(newExtraSpores));
                SaveExtraSpores();




            }
            else if (valorSliderMartillo > precioBaseMartilloGigante && cantidadEsporas < precioBaseMartilloGigante)
            {
                esporasInsuficientes.SetActive(true);
                StartCoroutine(DestroyObjectCoroutine(esporasInsuficientes, 0.5f));
            }
            else if (valorSliderMartillo < precioBaseMartilloGigante && cantidadEsporas < precioBaseMartilloGigante)
            {
                esporasInsuficientes.SetActive(true);
                StartCoroutine(DestroyObjectCoroutine(esporasInsuficientes, 0.5f));
            }
        }
        ActualizarPrecioMartillo();
    }
    public void ComprarShotgun()
    {
       
        PlayerEsporas playerEsporas = FindObjectOfType<PlayerEsporas>();

        if (playerEsporas != null)
        {
            float cantidadEsporas = playerEsporas.GetEsporas();
            
            if (cantidadEsporas >= precioBaseShotgun)
            {
                for (int i = 0; i < botonesADesactivar.Length; i++)
                {
                    botonesADesactivar[i].enabled = false;
                }

                for (int i = 0; i < freezeEnemies.Length; i++)
                {
                    StartCoroutine(ActivarComponentesEnemigosConRetraso(i, 4f));
                }
                StartCoroutine(ActivateObjectWithDelay(minimapa, 4f));
                DesbloquearShotgun();
                interesDiosa = 1;
                dialogosDiosaTienda2[4].SetActive(true);
                dialogosDiosaTienda2[0].SetActive(false);
                StartCoroutine(DestroyObjectCoroutine(Tienda_Diosa_10, 4f));
                StartCoroutine(DestroyObjectCoroutine(dialogosDiosaTienda2[4], 4f));
                StartCoroutine(DestroyObjectCoroutine(bolsaExporasExtra, 4f));
                StartCoroutine(EnableObjectWithDelay(gameManager, 4f));
                playerEsporas.RestarEsporas(precioBaseShotgun);
                ActualizarPrecioShotgun();

            }
            else
            {
                esporasInsuficientes.SetActive(true);
                StartCoroutine(DestroyObjectCoroutine(esporasInsuficientes, 0.5f));

            }
        }
        ActualizarPrecioShotgun();
    }
    public void ComprarShotgun2()
    {
        for (int i = 0; i < botonesADesactivar.Length; i++)
        {
            botonesADesactivar[i].enabled = false;
        }
        PlayerEsporas playerEsporas = FindObjectOfType<PlayerEsporas>();

        if (playerEsporas != null)
        {
            float cantidadEsporas = playerEsporas.GetEsporas();

            if (cantidadEsporas >= precioBaseShotgun)
            {
                for (int i = 0; i < freezeEnemies.Length; i++)
                {
                    freezeEnemies[i].ActivarComponentes();
                }
                for (int i = 0; i < freezeEnemies.Length; i++)
                {
                    StartCoroutine(ActivarComponentesEnemigosConRetraso(i, 4f));
                }
                StartCoroutine(ActivateObjectWithDelay(minimapa, 4f));
                DesbloquearShotgun();
                interesDiosa = 1;
                dialogosDiosaTienda3[4].SetActive(true);
                dialogosDiosaTienda3[0].SetActive(false);
                StartCoroutine(DestroyObjectCoroutine(Tienda_Diosa_15, 4f));
                StartCoroutine(DestroyObjectCoroutine(dialogosDiosaTienda3[4], 4f));
                StartCoroutine(DestroyObjectCoroutine(bolsaExporasExtra, 4f));
                StartCoroutine(EnableObjectWithDelay(gameManager, 4f));
                playerEsporas.RestarEsporas(precioBaseShotgun);
                ActualizarPrecioShotgun2();

            }
            else
            {
                esporasInsuficientes.SetActive(true);
                StartCoroutine(DestroyObjectCoroutine(esporasInsuficientes, 0.5f));

            }
        }
        ActualizarPrecioShotgun2();
    }

    public void NegociarShotgun()
    {
        animator.enabled = false;
        BolsaEsporas bolsaEsporas = FindAnyObjectByType<BolsaEsporas>();
        Debug.Log("Regateando...");

        PlayerEsporas playerEsporas = FindObjectOfType<PlayerEsporas>();

        if (playerEsporas != null)
        {
            int valorSliderShotgun = (int)sliderNegocaci�nShotgun.slider.value;
            float cantidadEsporas = playerEsporas.GetEsporas();
            if (valorSliderShotgun == precioBaseShotgun && cantidadEsporas >= precioBaseShotgun)
            {
                Debug.Log("Pagando precio base");
                for (int i = 0; i < freezeEnemies.Length; i++)
                {
                    StartCoroutine(ActivarComponentesEnemigosConRetraso(i, 7f));
                }
                StartCoroutine(ActivateObjectWithDelay(minimapa, 7f));
                DesbloquearShotgun();
                dialogosDiosaTienda2[4].SetActive(true);
                dialogosDiosaTienda2[1].SetActive(false);
                StartCoroutine(DestroyObjectCoroutine(Tienda_Diosa_10, 7f));
                StartCoroutine(EnableObjectWithDelay(gameManager, 7f));
                playerEsporas.RestarEsporas(valorSliderShotgun);
                interesDiosa = 1;
                ActualizarPrecioShotgun();
                

                extraSpores = valorSliderShotgun - precioBaseShotgun;

                StartCoroutine(bolsaEsporas.InstantiateEsporas(extraSpores));
                SaveExtraSpores();
                UpdateExtraSporesText();
            }

            else if (valorSliderShotgun >= precioBaseShotgun * interes_10_porciento && valorSliderShotgun < precioBaseShotgun && cantidadEsporas >= precioBaseShotgun * interes_10_porciento)
            {
                for (int i = 0; i < freezeEnemies.Length; i++)
                {
                    StartCoroutine(ActivarComponentesEnemigosConRetraso(i, 7f));
                }
                StartCoroutine(ActivateObjectWithDelay(minimapa, 7f));
                DesbloquearShotgun();
                interesDiosa = interesDiosa * 1.1f;
                dialogosDiosaTienda2[2].SetActive(true);
                dialogosDiosaTienda2[1].SetActive(false);
                StartCoroutine(DestroyObjectCoroutine(Tienda_Diosa_10, 7f));
                playerEsporas.RestarEsporas(valorSliderShotgun);
                StartCoroutine(DestroyObjectCoroutine(bolsaExporasExtra, 7f));
                ActualizarPrecioShotgun();
            }
            else if (valorSliderShotgun >= precioBaseShotgun * interes_20_porciento && valorSliderShotgun < precioBaseShotgun && cantidadEsporas >= precioBaseShotgun * interes_20_porciento)
            {
                for (int i = 0; i < freezeEnemies.Length; i++)
                {
                    StartCoroutine(ActivarComponentesEnemigosConRetraso(i, 7f));
                }
                StartCoroutine(ActivateObjectWithDelay(minimapa, 7f));
                DesbloquearShotgun(); 
                interesDiosa = interesDiosa * 1.2f;
                playerEsporas.RestarEsporas(valorSliderShotgun);
                dialogosDiosaTienda2[2].SetActive(true);
                dialogosDiosaTienda2[1].SetActive(false);
                ActualizarPrecioShotgun(); 
                StartCoroutine(DestroyObjectCoroutine(bolsaExporasExtra, 7f));
                StartCoroutine(DestroyObjectCoroutine(Tienda_Diosa_10, 7f));

            }
            else if (valorSliderShotgun >= precioBaseShotgun * interes_30_porciento && valorSliderShotgun < precioBaseShotgun && cantidadEsporas >= precioBaseShotgun * interes_30_porciento)
            {
                for (int i = 0; i < freezeEnemies.Length; i++)
                {
                    StartCoroutine(ActivarComponentesEnemigosConRetraso(i, 7f));
                }
                DesbloquearShotgun();
                interesDiosa = interesDiosa * 1.3f;
                playerEsporas.RestarEsporas(valorSliderShotgun);
                dialogosDiosaTienda2[2].SetActive(true);
                dialogosDiosaTienda2[1].SetActive(false);
                ActualizarPrecioShotgun();
                StartCoroutine(DestroyObjectCoroutine(Tienda_Diosa_10, 7f));
                StartCoroutine(DestroyObjectCoroutine(bolsaExporasExtra, 7f));
            }
            else if (valorSliderShotgun > precioBaseShotgun)
            {
                Debug.Log("Pagando Extra");
                for (int i = 0; i < freezeEnemies.Length; i++)
                {
                    StartCoroutine(ActivarComponentesEnemigosConRetraso(i, 7f));
                }
                StartCoroutine(ActivateObjectWithDelay(minimapa, 7f));
                DesbloquearShotgun();
                dialogosDiosaTienda2[3].SetActive(true);
                dialogosDiosaTienda2[1].SetActive(false);
                StartCoroutine(DestroyObjectCoroutine(Tienda_Diosa_10, 7f));
                playerEsporas.RestarEsporas(valorSliderShotgun);
                interesDiosa = 1;
                ActualizarPrecioShotgun();
                StartCoroutine(DestroyObjectCoroutine(bolsaExporasExtra, 7f));

                
                float newExtraSpores = valorSliderShotgun - precioBaseShotgun;
                UpdateExtraSporesText2(newExtraSpores);
                StartCoroutine(DestroyObjectCoroutine(bolsaExporasExtra, 7f));
                StartCoroutine(bolsaEsporas.InstantiateEsporas(newExtraSpores));
                SaveExtraSpores();

               


            }
            else if (valorSliderShotgun > precioBaseShotgun && cantidadEsporas < precioBaseShotgun)
            {
                esporasInsuficientes.SetActive(true);
                StartCoroutine(DestroyObjectCoroutine(esporasInsuficientes, 0.5f));
            }
            else if (valorSliderShotgun < precioBaseShotgun && cantidadEsporas < precioBaseShotgun)
            {
                esporasInsuficientes.SetActive(true);
                StartCoroutine(DestroyObjectCoroutine(esporasInsuficientes, 0.5f));
            }
        }
    }
    public void NegociarShotgun2()
    {
        animator.enabled = false;
        BolsaEsporas bolsaEsporas = FindAnyObjectByType<BolsaEsporas>();
        Debug.Log("Regateando...");

        PlayerEsporas playerEsporas = FindObjectOfType<PlayerEsporas>();

        if (playerEsporas != null)
        {
            int valorSliderShotgun = (int)sliderNegocaci�nShotgun2.slider.value;
            float cantidadEsporas = playerEsporas.GetEsporas();
            if (valorSliderShotgun == precioBaseShotgun && cantidadEsporas >= precioBaseShotgun)
            {
                Debug.Log("Pagando precio base");
                for (int i = 0; i < freezeEnemies.Length; i++)
                {
                    StartCoroutine(ActivarComponentesEnemigosConRetraso(i, 7f));
                }
                StartCoroutine(ActivateObjectWithDelay(minimapa, 7f));
                DesbloquearShotgun();
                dialogosDiosaTienda3[4].SetActive(true);
                dialogosDiosaTienda3[1].SetActive(false);
                StartCoroutine(DestroyObjectCoroutine(Tienda_Diosa_15, 7f));
                StartCoroutine(EnableObjectWithDelay(gameManager, 7f));
                playerEsporas.RestarEsporas(valorSliderShotgun);
                interesDiosa = 1;
                ActualizarPrecioShotgun2();


                extraSpores = valorSliderShotgun - precioBaseShotgun;

                StartCoroutine(bolsaEsporas.InstantiateEsporas(extraSpores));
                SaveExtraSpores();
                UpdateExtraSporesText();
            }

            else if (valorSliderShotgun >= precioBaseShotgun * interes_10_porciento && valorSliderShotgun < precioBaseShotgun && cantidadEsporas >= precioBaseShotgun * interes_10_porciento)
            {
                for (int i = 0; i < freezeEnemies.Length; i++)
                {
                    StartCoroutine(ActivarComponentesEnemigosConRetraso(i, 7f));
                }
                StartCoroutine(ActivateObjectWithDelay(minimapa, 7f));
                DesbloquearShotgun();
                interesDiosa = interesDiosa * 1.1f;
                dialogosDiosaTienda3[2].SetActive(true);
                dialogosDiosaTienda3[1].SetActive(false);
                StartCoroutine(DestroyObjectCoroutine(Tienda_Diosa_15, 7f));
                playerEsporas.RestarEsporas(valorSliderShotgun);
                StartCoroutine(DestroyObjectCoroutine(bolsaExporasExtra, 7f));
                ActualizarPrecioShotgun2();
            }
            else if (valorSliderShotgun >= precioBaseShotgun * interes_20_porciento && valorSliderShotgun < precioBaseShotgun && cantidadEsporas >= precioBaseShotgun * interes_20_porciento)
            {
                for (int i = 0; i < freezeEnemies.Length; i++)
                {
                    StartCoroutine(ActivarComponentesEnemigosConRetraso(i, 7f));
                }
                StartCoroutine(ActivateObjectWithDelay(minimapa, 7f));
                DesbloquearShotgun();
                interesDiosa = interesDiosa * 1.2f;
                playerEsporas.RestarEsporas(valorSliderShotgun);
                dialogosDiosaTienda3[2].SetActive(true);
                dialogosDiosaTienda3[1].SetActive(false);
                ActualizarPrecioShotgun2();
                StartCoroutine(DestroyObjectCoroutine(bolsaExporasExtra, 7f));
                StartCoroutine(DestroyObjectCoroutine(Tienda_Diosa_10, 7f));

            }
            else if (valorSliderShotgun >= precioBaseShotgun * interes_30_porciento && valorSliderShotgun < precioBaseShotgun && cantidadEsporas >= precioBaseShotgun * interes_30_porciento)
            {
                for (int i = 0; i < freezeEnemies.Length; i++)
                {
                    StartCoroutine(ActivarComponentesEnemigosConRetraso(i, 7f));
                }
                StartCoroutine(ActivateObjectWithDelay(minimapa, 7f));
                DesbloquearShotgun();
                interesDiosa = interesDiosa * 1.3f;
                playerEsporas.RestarEsporas(valorSliderShotgun);
                dialogosDiosaTienda3[2].SetActive(true);
                dialogosDiosaTienda3[1].SetActive(false);
                ActualizarPrecioShotgun2();
                StartCoroutine(DestroyObjectCoroutine(Tienda_Diosa_10, 7f));
                StartCoroutine(DestroyObjectCoroutine(bolsaExporasExtra, 7f));
            }
            else if (valorSliderShotgun >= precioBaseShotgun)
            {
                for (int i = 0; i < freezeEnemies.Length; i++)
                {
                    StartCoroutine(ActivarComponentesEnemigosConRetraso(i, 7f));
                }
                StartCoroutine(ActivateObjectWithDelay(minimapa, 7f));
                DesbloquearShotgun();
                dialogosDiosaTienda3[3].SetActive(true);
                dialogosDiosaTienda3[1].SetActive(false);
                StartCoroutine(DestroyObjectCoroutine(Tienda_Diosa_10, 7f));
                playerEsporas.RestarEsporas(valorSliderShotgun);
                interesDiosa = 1;
                ActualizarPrecioShotgun2();
                StartCoroutine(DestroyObjectCoroutine(bolsaExporasExtra, 7f));


                float newExtraSpores = valorSliderShotgun - precioBaseShotgun;
                UpdateExtraSporesText2(newExtraSpores);
                StartCoroutine(DestroyObjectCoroutine(bolsaExporasExtra, 7f));
                StartCoroutine(bolsaEsporas.InstantiateEsporas(newExtraSpores));
                SaveExtraSpores();




            }
            else if (valorSliderShotgun > precioBaseShotgun && cantidadEsporas < precioBaseShotgun)
            {
                esporasInsuficientes.SetActive(true);
                StartCoroutine(DestroyObjectCoroutine(esporasInsuficientes, 0.5f));
            }
        }
    }

    public void ComprarAumentoDa�o()
    {
        
        PlayerEsporas playerEsporas = FindObjectOfType<PlayerEsporas>();

        if (playerEsporas != null)
        {
            float cantidadEsporas = playerEsporas.GetEsporas();

            if (cantidadEsporas >= precioBaseDa�oAumentado)
            {
                for (int i = 0; i < freezeEnemies.Length; i++)
                {
                    if (freezeEnemies[i] != null)
                    {
                        StartCoroutine(ActivarComponentesEnemigosConRetraso(i, 4f));
                    }
                }
                for (int i = 0; i < botonesADesactivar.Length; i++)
                {
                    botonesADesactivar[i].enabled = false;
                }
                StartCoroutine(ActivateObjectWithDelay(minimapa, 4f));
                aumentarDa�o.ActivateAdditionalDamage();
                interesDiosa = 1;
                dialogosDiosaTienda1[4].SetActive(true);
                dialogosDiosaTienda1[0].SetActive(false);
                StartCoroutine(DestroyObjectCoroutine(Tienda_Diosa_5, 4f));
                StartCoroutine(DestroyObjectCoroutine(dialogosDiosaTienda1[4], 4f));
                StartCoroutine(DestroyObjectCoroutine(bolsaExporasExtra, 4f));
                StartCoroutine(EnableObjectWithDelay(gameManager, 4f));
                playerEsporas.RestarEsporas(precioBaseDa�oAumentado);
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
        animator.enabled = false;
        BolsaEsporas bolsaEsporas = FindAnyObjectByType<BolsaEsporas>();
        Debug.Log("Regateando...");

        PlayerEsporas playerEsporas = FindObjectOfType<PlayerEsporas>();

        if (playerEsporas != null)
        {
            int valorSliderDa�oAumentado = (int)sliderNegocaci�nDa�oAumentado.slider.value;
            float cantidadEsporas = playerEsporas.GetEsporas();


            if (valorSliderDa�oAumentado >= precioBaseDa�oAumentado * interes_10_porciento && valorSliderDa�oAumentado < precioBaseDa�oAumentado && cantidadEsporas >= precioBaseDa�oAumentado * interes_10_porciento)
            {
                Debug.Log("Desbloqueando Da�o Aumentado 10");
                for (int i = 0; i < freezeEnemies.Length; i++)
                {
                    if (freezeEnemies[i] != null)
                    {
                        StartCoroutine(ActivarComponentesEnemigosConRetraso(i, 7f));
                    }
                }
                StartCoroutine(ActivateObjectWithDelay(minimapa, 7f));
                aumentarDa�o.ActivateAdditionalDamage();
                interesDiosa = interesDiosa * 1.1f;
                dialogosDiosaTienda1[2].SetActive(true);
                dialogosDiosaTienda1[1].SetActive(false);
                StartCoroutine(DestroyObjectCoroutine(Tienda_Diosa_5, 7f));
                playerEsporas.RestarEsporas(valorSliderDa�oAumentado);
                StartCoroutine(DestroyObjectCoroutine(bolsaExporasExtra, 7f));
                StartCoroutine(EnableObjectWithDelay(gameManager, 7f));
                ActualizarPrecioAumentarda�o();
            }
            else if (valorSliderDa�oAumentado >= precioBaseDa�oAumentado * interes_20_porciento && valorSliderDa�oAumentado < precioBaseDa�oAumentado && cantidadEsporas >= precioBaseDa�oAumentado * interes_20_porciento)
            {
                Debug.Log("Desbloqueando Da�o Aumentado 20%");
                for (int i = 0; i < freezeEnemies.Length; i++)
                {
                    if (freezeEnemies[i] != null)
                    {
                        StartCoroutine(ActivarComponentesEnemigosConRetraso(i, 7f));
                    }
                }
                StartCoroutine(ActivateObjectWithDelay(minimapa, 7f));
                aumentarDa�o.ActivateAdditionalDamage();
                interesDiosa = interesDiosa * 1.2f;
                playerEsporas.RestarEsporas(valorSliderDa�oAumentado);
                dialogosDiosaTienda1[2].SetActive(true);
                dialogosDiosaTienda1[1].SetActive(false);
                StartCoroutine(DestroyObjectCoroutine(Tienda_Diosa_5, 7f));
                StartCoroutine(DestroyObjectCoroutine(bolsaExporasExtra, 7f));
                StartCoroutine(EnableObjectWithDelay(gameManager, 7f));
                ActualizarPrecioAumentarda�o();
            }
            else if (valorSliderDa�oAumentado >= precioBaseDa�oAumentado * interes_30_porciento && valorSliderDa�oAumentado < precioBaseDa�oAumentado && cantidadEsporas >= precioBaseDa�oAumentado * interes_30_porciento)
            {
                Debug.Log("Desbloqueando Da�o Aumentado 30%");
                for (int i = 0; i < freezeEnemies.Length; i++)
                {
                    if (freezeEnemies[i] != null)
                    {
                        StartCoroutine(ActivarComponentesEnemigosConRetraso(i, 7f));
                    }
                }
                StartCoroutine(ActivateObjectWithDelay(minimapa, 7f));
                aumentarDa�o.ActivateAdditionalDamage();
                interesDiosa = interesDiosa * 1.3f;
                playerEsporas.RestarEsporas(valorSliderDa�oAumentado);
                dialogosDiosaTienda1[2].SetActive(true);
                dialogosDiosaTienda1[1].SetActive(false);
                StartCoroutine(DestroyObjectCoroutine(bolsaExporasExtra, 7f));
                ActualizarPrecioAlabarda();
                StartCoroutine(DestroyObjectCoroutine(Tienda_Diosa_5, 7f));
                StartCoroutine(EnableObjectWithDelay(gameManager, 7f));
                ActualizarPrecioAumentarda�o();
            }
            else if (valorSliderDa�oAumentado >= precioBaseDa�oAumentado && cantidadEsporas >= precioBaseDa�oAumentado)
            {
                Debug.Log("Desbloqueando Da�o Aumentado");
                for (int i = 0; i < freezeEnemies.Length; i++)
                {
                    if (freezeEnemies[i] != null)
                    {
                        StartCoroutine(ActivarComponentesEnemigosConRetraso(i, 7f));
                    }
                }
                StartCoroutine(ActivateObjectWithDelay(minimapa, 7f));
                aumentarDa�o.ActivateAdditionalDamage();
                dialogosDiosaTienda1[3].SetActive(true);
                dialogosDiosaTienda1[1].SetActive(false);
                ActualizarPrecioAlabarda();
                StartCoroutine(DestroyObjectCoroutine(Tienda_Diosa_5, 7f));
                playerEsporas.RestarEsporas(valorSliderDa�oAumentado);
                interesDiosa = 1;
                ActualizarPrecioAumentarda�o();
                StartCoroutine(DestroyObjectCoroutine(bolsaExporasExtra, 7f));
                StartCoroutine(EnableObjectWithDelay(gameManager, 7f));
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
   
    public void ComprarDa�oReducido()
    {
        
        PlayerEsporas playerEsporas = FindObjectOfType<PlayerEsporas>();

        if (playerEsporas != null)
        {
            float cantidadEsporas = playerEsporas.GetEsporas();
            
            if (cantidadEsporas >= precioBaseAlmaDa�oReducido)
            {
                for (int i = 0; i < freezeEnemies.Length; i++)
                {
                    freezeEnemies[i].ActivarComponentes();
                }
                for (int i = 0; i < freezeEnemies.Length; i++)
                {
                    if (freezeEnemies[i] != null)
                    {
                        StartCoroutine(ActivarComponentesEnemigosConRetraso(i, 4f));
                    }
                }
                StartCoroutine(ActivateObjectWithDelay(minimapa, 4f));
                reducirDa�o.ActivateReducedDamage();
                interesDiosa = 1;
                dialogosDiosaTienda2[4].SetActive(true);
                dialogosDiosaTienda2[0].SetActive(false);
                StartCoroutine(DestroyObjectCoroutine(Tienda_Diosa_10, 4f));
                StartCoroutine(DestroyObjectCoroutine(dialogosDiosaTienda2[4], 4f));
                StartCoroutine(DestroyObjectCoroutine(bolsaExporasExtra, 4f));
                StartCoroutine(EnableObjectWithDelay(gameManager, 4f));
                playerEsporas.RestarEsporas(precioBaseAlmaDa�oReducido);
            }
            else
            {
                esporasInsuficientes.SetActive(true);
                StartCoroutine(DestroyObjectCoroutine(esporasInsuficientes, 0.5f));

            }
        }
        ActualizarPrecioDa�oReducido();
    }

    public void NegociarDa�oReducido()
    {
        animator.enabled = false;
        BolsaEsporas bolsaEsporas = FindAnyObjectByType<BolsaEsporas>();
        Debug.Log("Regateando...");

        PlayerEsporas playerEsporas = FindObjectOfType<PlayerEsporas>();

        if (playerEsporas != null)
        {
            int valorSliderDa�oReducido = (int)sliderNegocaci�nDa�oReducido.slider.value;
            float cantidadEsporas = playerEsporas.GetEsporas();


            if (valorSliderDa�oReducido >= precioBaseAlmaDa�oReducido * interes_10_porciento && valorSliderDa�oReducido < precioBaseAlmaDa�oReducido && cantidadEsporas >= precioBaseAlmaDa�oReducido * interes_10_porciento)
            {
                Debug.Log("Desbloqueando Da�o Reducido 10");
                for (int i = 0; i < freezeEnemies.Length; i++)
                {
                    if (freezeEnemies[i] != null)
                    {
                        StartCoroutine(ActivarComponentesEnemigosConRetraso(i, 7f));
                    }
                }
                StartCoroutine(ActivateObjectWithDelay(minimapa, 7f));
                reducirDa�o.ActivateReducedDamage();
                interesDiosa = interesDiosa * 1.1f;
                dialogosDiosaTienda2[2].SetActive(true);
                dialogosDiosaTienda2[1].SetActive(false);
                StartCoroutine(DestroyObjectCoroutine(Tienda_Diosa_10, 7f));
                playerEsporas.RestarEsporas(valorSliderDa�oReducido);
                StartCoroutine(DestroyObjectCoroutine(bolsaExporasExtra, 7f));
                StartCoroutine(EnableObjectWithDelay(gameManager, 7f));
                ActualizarPrecioAumentarda�o();
            }
            else if (valorSliderDa�oReducido >= precioBaseAlmaDa�oReducido * interes_20_porciento && valorSliderDa�oReducido < precioBaseAlmaDa�oReducido && cantidadEsporas >= precioBaseAlmaDa�oReducido * interes_20_porciento)
            {
                Debug.Log("Desbloqueando Da�o Reducido 20%");
                for (int i = 0; i < freezeEnemies.Length; i++)
                {
                    if (freezeEnemies[i] != null)
                    {
                        StartCoroutine(ActivarComponentesEnemigosConRetraso(i, 7f));
                    }
                }
                StartCoroutine(ActivateObjectWithDelay(minimapa, 7f));
                reducirDa�o.ActivateReducedDamage(); ;
                interesDiosa = interesDiosa * 1.2f;
                playerEsporas.RestarEsporas(valorSliderDa�oReducido);
                dialogosDiosaTienda2[2].SetActive(true);
                dialogosDiosaTienda2[1].SetActive(false);
                StartCoroutine(DestroyObjectCoroutine(Tienda_Diosa_10, 7f));
                StartCoroutine(DestroyObjectCoroutine(bolsaExporasExtra, 7f));
                StartCoroutine(EnableObjectWithDelay(gameManager, 7f));
                ActualizarPrecioAumentarda�o();
            }
            else if (valorSliderDa�oReducido >= precioBaseAlmaDa�oReducido * interes_30_porciento && valorSliderDa�oReducido < precioBaseAlmaDa�oReducido && cantidadEsporas >= precioBaseAlmaDa�oReducido * interes_30_porciento)
            {
                Debug.Log("Desbloqueando Da�o Reducido 30%");
                for (int i = 0; i < freezeEnemies.Length; i++)
                {
                    if (freezeEnemies[i] != null)
                    {
                        StartCoroutine(ActivarComponentesEnemigosConRetraso(i, 7f));
                    }
                }
                StartCoroutine(ActivateObjectWithDelay(minimapa, 7f));
                reducirDa�o.ActivateReducedDamage();
                interesDiosa = interesDiosa * 1.3f;
                playerEsporas.RestarEsporas(valorSliderDa�oReducido);
                dialogosDiosaTienda2[2].SetActive(true);
                dialogosDiosaTienda2[1].SetActive(false);
                StartCoroutine(DestroyObjectCoroutine(bolsaExporasExtra, 7f));
                ActualizarPrecioAlabarda();
                StartCoroutine(EnableObjectWithDelay(gameManager, 7f));
                StartCoroutine(DestroyObjectCoroutine(Tienda_Diosa_10, 7f));
                ActualizarPrecioAumentarda�o();
            }
            else if (valorSliderDa�oReducido >= precioBaseAlmaDa�oReducido && cantidadEsporas >= precioBaseAlmaDa�oReducido)
            {
                Debug.Log("Desbloqueando Da�o Reducido");
                for (int i = 0; i < freezeEnemies.Length; i++)
                {
                    if (freezeEnemies[i] != null)
                    {
                        StartCoroutine(ActivarComponentesEnemigosConRetraso(i, 7f));
                    }
                }
                StartCoroutine(ActivateObjectWithDelay(minimapa, 7f));
                reducirDa�o.ActivateReducedDamage();
                dialogosDiosaTienda2[3].SetActive(true);
                dialogosDiosaTienda2[1].SetActive(false);
                ActualizarPrecioAlabarda();
                StartCoroutine(DestroyObjectCoroutine(Tienda_Diosa_10, 7f));
                playerEsporas.RestarEsporas(valorSliderDa�oReducido);
                interesDiosa = 1;
                ActualizarPrecioAumentarda�o();
                StartCoroutine(DestroyObjectCoroutine(bolsaExporasExtra, 7f));
                StartCoroutine(EnableObjectWithDelay(gameManager, 7f));
                extraSpores = valorSliderDa�oReducido - precioBaseAlmaDa�oReducido;

                StartCoroutine(bolsaEsporas.InstantiateEsporas(extraSpores));
                SaveExtraSpores();
                UpdateExtraSporesText();


            }
            else if (valorSliderDa�oReducido > precioBaseAlmaDa�oReducido && cantidadEsporas < precioBaseAlmaDa�oReducido)
            {
                esporasInsuficientes.SetActive(true);
                StartCoroutine(DestroyObjectCoroutine(esporasInsuficientes, 0.5f));
            }
            else if (valorSliderDa�oReducido < precioBaseAlmaDa�oReducido && cantidadEsporas < precioBaseAlmaDa�oReducido)
            {
                esporasInsuficientes.SetActive(true);
                StartCoroutine(DestroyObjectCoroutine(esporasInsuficientes, 0.5f));
            }
        }
    }
    public void ComprarAtaqueArea()
    {
        for (int i = 0; i < botonesADesactivar.Length; i++)
        {
            botonesADesactivar[i].enabled = false;
        }
        PlayerEsporas playerEsporas = FindObjectOfType<PlayerEsporas>();

        if (playerEsporas != null)
        {
            float cantidadEsporas = playerEsporas.GetEsporas();

            if (cantidadEsporas >= precioBaseAtaqueArea)
            {
                for (int i = 0; i < freezeEnemies.Length; i++)
                {
                    if (freezeEnemies[i] != null)
                    {
                        StartCoroutine(ActivarComponentesEnemigosConRetraso(i, 4f));
                    }
                }
                for (int i = 0; i < botonesADesactivar.Length; i++)
                {
                    botonesADesactivar[i].enabled = false;
                }
                StartCoroutine(ActivateObjectWithDelay(minimapa, 4f));
                areaAttack.DesbloquearAtaqueArea();
                interesDiosa = 1;
                dialogosDiosaTienda3[4].SetActive(true);
                dialogosDiosaTienda3[0].SetActive(false);
                StartCoroutine(DestroyObjectCoroutine(Tienda_Diosa_15, 4f));
                StartCoroutine(DestroyObjectCoroutine(dialogosDiosaTienda3[4], 4f));
                StartCoroutine(DestroyObjectCoroutine(bolsaExporasExtra, 4f));
                StartCoroutine(EnableObjectWithDelay(gameManager, 4f));
                playerEsporas.RestarEsporas(precioBaseAtaqueArea);
            }
            else
            {
                esporasInsuficientes.SetActive(true);
                StartCoroutine(DestroyObjectCoroutine(esporasInsuficientes, 0.5f));

            }
        }
        ActualizarPrecioDa�oReducido();
    }
    public void NegociarAtaqueArea()
    {
        animator.enabled = false;
        BolsaEsporas bolsaEsporas = FindAnyObjectByType<BolsaEsporas>();
        Debug.Log("Regateando...");

        PlayerEsporas playerEsporas = FindObjectOfType<PlayerEsporas>();

        if (playerEsporas != null)
        {
            int valorSliderAtaqueArea = (int)sliderNegocaci�nAtaqueArea.slider.value;
            float cantidadEsporas = playerEsporas.GetEsporas();


            if (valorSliderAtaqueArea >= precioBaseAtaqueArea * interes_10_porciento && valorSliderAtaqueArea < precioBaseAtaqueArea && cantidadEsporas >= precioBaseAtaqueArea * interes_10_porciento)
            {
                Debug.Log("Desbloqueando Da�o Reducido 10");
                for (int i = 0; i < freezeEnemies.Length; i++)
                {
                    if (freezeEnemies[i] != null)
                    {
                        StartCoroutine(ActivarComponentesEnemigosConRetraso(i, 7f));
                    }
                }
                StartCoroutine(ActivateObjectWithDelay(minimapa, 7f));
                areaAttack.DesbloquearAtaqueArea();
                interesDiosa = interesDiosa * 1.1f;
                dialogosDiosaTienda3[2].SetActive(true);
                dialogosDiosaTienda3[1].SetActive(false);
                StartCoroutine(DestroyObjectCoroutine(Tienda_Diosa_15, 7f));
                playerEsporas.RestarEsporas(valorSliderAtaqueArea);
                StartCoroutine(DestroyObjectCoroutine(bolsaExporasExtra, 7f));
                StartCoroutine(EnableObjectWithDelay(gameManager, 7f));
                ActualizarPrecioAtaqueArea();
            }
            else if (valorSliderAtaqueArea >= precioBaseAtaqueArea * interes_20_porciento && valorSliderAtaqueArea < precioBaseAtaqueArea && cantidadEsporas >= precioBaseAtaqueArea * interes_20_porciento)
            {
                Debug.Log("Desbloqueando Da�o Reducido 20%");
                for (int i = 0; i < freezeEnemies.Length; i++)
                {
                    if (freezeEnemies[i] != null)
                    {
                        StartCoroutine(ActivarComponentesEnemigosConRetraso(i, 7f));
                    }
                }
                StartCoroutine(ActivateObjectWithDelay(minimapa, 7f));
                areaAttack.DesbloquearAtaqueArea();
                interesDiosa = interesDiosa * 1.2f;
                playerEsporas.RestarEsporas(valorSliderAtaqueArea);
                dialogosDiosaTienda3[2].SetActive(true);
                dialogosDiosaTienda3[1].SetActive(false);
                StartCoroutine(DestroyObjectCoroutine(Tienda_Diosa_15, 7f));
                StartCoroutine(DestroyObjectCoroutine(bolsaExporasExtra, 7f));
                StartCoroutine(EnableObjectWithDelay(gameManager, 7f));
                ActualizarPrecioAtaqueArea();
            }
            else if (valorSliderAtaqueArea >= precioBaseAtaqueArea * interes_30_porciento && valorSliderAtaqueArea < precioBaseAtaqueArea && cantidadEsporas >= precioBaseAtaqueArea * interes_30_porciento)
            {
                Debug.Log("Desbloqueando Da�o Reducido 30%");
                for (int i = 0; i < freezeEnemies.Length; i++)
                {
                    if (freezeEnemies[i] != null)
                    {
                        StartCoroutine(ActivarComponentesEnemigosConRetraso(i, 7f));
                    }
                }
                StartCoroutine(ActivateObjectWithDelay(minimapa, 7f));
                areaAttack.DesbloquearAtaqueArea();
                interesDiosa = interesDiosa * 1.3f;
                playerEsporas.RestarEsporas(valorSliderAtaqueArea);
                dialogosDiosaTienda3[2].SetActive(true);
                dialogosDiosaTienda3[1].SetActive(false);
                StartCoroutine(DestroyObjectCoroutine(bolsaExporasExtra, 7f));
                ActualizarPrecioAlabarda();
                StartCoroutine(DestroyObjectCoroutine(Tienda_Diosa_15, 7f));
                StartCoroutine(EnableObjectWithDelay(gameManager, 7f));
                ActualizarPrecioAtaqueArea();
            }
            else if (valorSliderAtaqueArea >= precioBaseAtaqueArea && cantidadEsporas >= precioBaseAtaqueArea)
            {
                Debug.Log("Desbloqueando Da�o Reducido");
                for (int i = 0; i < freezeEnemies.Length; i++)
                {
                    if (freezeEnemies[i] != null)
                    {
                        StartCoroutine(ActivarComponentesEnemigosConRetraso(i, 7f));
                    }
                }
                StartCoroutine(ActivateObjectWithDelay(minimapa, 7f));
                areaAttack.DesbloquearAtaqueArea();
                dialogosDiosaTienda3[3].SetActive(true);
                dialogosDiosaTienda3[1].SetActive(false);
                StartCoroutine(DestroyObjectCoroutine(Tienda_Diosa_15, 7f));
                playerEsporas.RestarEsporas(valorSliderAtaqueArea);
                interesDiosa = 1;
                ActualizarPrecioAtaqueArea();
                StartCoroutine(DestroyObjectCoroutine(bolsaExporasExtra, 7f));
                StartCoroutine(EnableObjectWithDelay(gameManager, 7f));
                extraSpores = valorSliderAtaqueArea - precioBaseAtaqueArea;

                StartCoroutine(bolsaEsporas.InstantiateEsporas(extraSpores));
                SaveExtraSpores();
                UpdateExtraSporesText();


            }
            else if (valorSliderAtaqueArea > precioBaseAtaqueArea && cantidadEsporas < precioBaseAtaqueArea)
            {
                esporasInsuficientes.SetActive(true);
                StartCoroutine(DestroyObjectCoroutine(esporasInsuficientes, 0.5f));
            }
            else if (valorSliderAtaqueArea < precioBaseAtaqueArea && cantidadEsporas < precioBaseAtaqueArea)
            {
                esporasInsuficientes.SetActive(true);
                StartCoroutine(DestroyObjectCoroutine(esporasInsuficientes, 0.5f));
            }
        }
    }

    public void DesbloquearRevolver()
    {
        equippedPopUps.Play("RevolverEquippedAnimation");
        StartCoroutine(ActivateObjectAnimationwithDelay("HalberdEquippedAnimation", 7f));
        inventory.SetActive(false);
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
        StartCoroutine(ActivateObjectAnimationwithDelay("AxeEquippedAnimation", 7f));
        equippedPopUps.Play("AxeEquippedAnimation");
        inventory.SetActive(false);
        hachaDesbloqueada = true;
        Time.timeScale = 1.0f;
    }
    public void DesbloquearAlabarda()
    {
        StartCoroutine(ActivateObjectWithDelay(minimapa, 7f));
        StartCoroutine(ActivateObjectAnimationwithDelay("HalberdEquippedAnimation", 7f));
        Debug.Log("Alabarda Desbloqueada");
        alabardaDesbloqueda = true;
        Time.timeScale = 1.0f;
    }
    public void DesbloquearMartilloGigante()
    {
        StartCoroutine(ActivateObjectAnimationwithDelay("HammerEquippedAnimation", 7f));
        StartCoroutine(ActivateObjectWithDelay(minimapa, 7f));
        Debug.Log("Desbloqueando Martillo");
        martilloGiganteDesbloqueado = true;
        Time.timeScale = 1.0f;
    }
    public void DesbloquearSniper()
    {
        StartCoroutine(ActivateObjectWithDelay(minimapa, 7f));
        StartCoroutine(ActivateObjectAnimationwithDelay("SniperEquippedAnimation", 7f));
        sniperDesbloqueado = true;
        Time.timeScale = 1.0f;
    }
    public void DesbloquearShotgun()
    {
        StartCoroutine(ActivateObjectWithDelay(minimapa, 7f));
        StartCoroutine(ActivateObjectAnimationwithDelay("ShotgunEquippedAnimation", 7f));
        Debug.Log("Shotgun Desbloqueada");
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
    private IEnumerator ActivateObjectWithDelay(GameObject objetoAActivar, float delayInSeconds)
    {
        yield return new WaitForSeconds(delayInSeconds);

        if (objetoAActivar != null)
        {
            objetoAActivar.SetActive(true);
        }
    }

    private IEnumerator EnableObjectWithDelay(SpawnerManager objetoADestruir, float delayInSeconds)
    {
        yield return new WaitForSeconds(delayInSeconds);

        if (objetoADestruir != null)
        {
            objetoADestruir.enabled = true;
        }
    }

    private IEnumerator ActivateObjectAnimationwithDelay(string animaci�n, float delayInSeconds)
    {
        yield return new WaitForSeconds(delayInSeconds);


        equippedPopUps.Play(animaci�n);
    }
    private IEnumerator ActivarComponentesEnemigosConRetraso(int i, float delay)
    {
        yield return new WaitForSeconds(delay);
        freezeEnemies[i].ActivarComponentes();
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
    private void ActualizarPrecioSniper2()
    {
        precioBaseSniper = precioBaseSniper * interesDiosa;

        if (precioSniper2Text != null)
        {
            precioSniper2Text.text = precioBaseSniper.ToString();
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
    private void ActualizarPrecioShotgun2()
    {
        precioBaseShotgun = precioBaseShotgun * interesDiosa;

        if (precioShotgun2Text != null)
        {
            precioShotgun2Text.text = precioBaseShotgun.ToString();
        }
    }
    private void ActualizarPrecioDa�oReducido()
    {
        precioBaseAlmaDa�oReducido = precioBaseAlmaDa�oReducido * interesDiosa;

        if (precioShotgunText != null)
        {
            precioDa�oReducidoText.text = precioBaseAlmaDa�oReducido.ToString();
        }
    }
    private void ActualizarPrecioAtaqueArea()
    {
        precioBaseAtaqueArea = precioBaseAtaqueArea * interesDiosa;

        if (precioShotgunText != null)
        {
            precioAtaqueAreaText.text = precioBaseAtaqueArea.ToString();
        }
    }

   

}


