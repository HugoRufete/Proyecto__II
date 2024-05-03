using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Weapon_Wheel_Manager : MonoBehaviour
{
    private float precioBasePistola = 250;
    private float precioBaseAlabarda = 77;
    private float precioBaseMartilloGigante = 120;
    private float precioBaseSniper = 102;
    private float precioBaseShotgun = 90;

    private float precioBaseAlmaDañoReducido = 102;
    private float precioBaseDañoAumentado = 94;

    private float precioBaseVortice = 102;
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
    public TMP_Text precioShotgunText;
    public TMP_Text precioDañoAumentadoText;
    public TMP_Text precioDañoReducidoText;

    [Header("Precio Negocación Text")]
    public TMP_Text PrecioNegociado;
    public TMP_Text PrecioNegociadoSniper;
    public TMP_Text PrecioNegociadoDañoAumentado;
    public TMP_Text PrecioNegociadoDañoReducido;
    public TMP_Text PrecioNegociadoMartilo;
    public TMP_Text PrecioNegociadoShotgun;

    [Header("Sliders Negociación")]
    public SliderScript sliderNegocaciónAlabarda;
    public SliderScript sliderNegocaciónMartillo;
    public SliderScript sliderNegocaciónSniper;
    public SliderScript sliderNegocaciónShotgun;
    public SliderScript sliderNegocaciónDañoAumentado;
    public SliderScript sliderNegocaciónDañoReducido;

    public SliderScript sliderBolsaEsporasExtra;
    [SerializeField] private float extraSpores = 0;

    public static float interesDiosa = 1;

    [Header("Esporas Extra")]
    public TMP_Text extraSporesText;

    private const string esporasExtra = "esporasExtra";

    public AumentarDañoAEnemigos aumentarDaño;

    public VidaPlayer reducirDaño;
    
    public GameObject [] dialogosDiosaTienda1;

    public GameObject [] dialogosDiosaTienda2;

    public GameObject bolsaExporasExtra;

    FreezeEnemies[] freezeEnemies;

    public Button [] botonesADesactivar; 
    

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
        ActualizarPrecioAumentardaño();
        ActualizarPrecioMartillo();
        ActualizarPrecioShotgun();
        ActualizarPrecioDañoReducido();
        animator = GetComponent<Animator>();

        freezeEnemies = FindObjectsOfType<FreezeEnemies>();
    }
    void Update()
    {
        if(sliderBolsaEsporasExtra != null)
        {
            sliderBolsaEsporasExtra.slider.value = extraSpores;
        }
        if (sliderNegocaciónAlabarda != null)
        {
            int valorSlider = (int)sliderNegocaciónAlabarda.slider.value;
            PrecioNegociado.text = "Pagando: " + valorSlider + " esporas";
        }
        if (sliderNegocaciónSniper != null)
        {
            int valorSliderSniper = (int)sliderNegocaciónSniper.slider.value;
            PrecioNegociadoSniper.text = "Pagando : " + valorSliderSniper + " esporas";
        }
        if (sliderNegocaciónMartillo != null)
        {
            int valorSliderMartillo = (int)sliderNegocaciónMartillo.slider.value;
            PrecioNegociadoMartilo.text = "Pagando : " + valorSliderMartillo + " esporas";
        }
        if (sliderNegocaciónDañoAumentado != null)
        {
            int valorSliderMartillo = (int)sliderNegocaciónDañoAumentado.slider.value;
            PrecioNegociadoDañoAumentado.text = "Pagando : " + valorSliderMartillo + " esporas";
        }

        if (sliderNegocaciónShotgun != null)
        {
            int valorSliderShotgun = (int)sliderNegocaciónShotgun.slider.value;
            PrecioNegociadoShotgun.text = "Pagando : " + valorSliderShotgun + " esporas";
        }
        if (sliderNegocaciónDañoReducido != null)
        {
            int valorSliderDañoReducidon = (int)sliderNegocaciónDañoReducido.slider.value;
            PrecioNegociadoDañoReducido.text = "Pagando : " + valorSliderDañoReducidon + " esporas";
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

    public void ActivarNegocaciónAlabardar()
    {
        animator.Play("AnimaciónNegocacionAlabarda");
        StartCoroutine(ActivateObjectWithDelay(dialogosDiosaTienda1[1], 2f));
        dialogosDiosaTienda1[0].SetActive(false);
    }
    public void ActivarNegocaciónSniper()
    {
        animator.Play("AnimaciónNegocacionSniper");
        StartCoroutine(ActivateObjectWithDelay(dialogosDiosaTienda1[1], 2f));
        dialogosDiosaTienda1[0].SetActive(false);
    }
    public void ActivarNegociaciónDañoAumentado()
    {
        animator.Play("AnimaciónNegocacionAumentoDaño");
        StartCoroutine(ActivateObjectWithDelay(dialogosDiosaTienda1[1], 2f));
        dialogosDiosaTienda1[0].SetActive(false);
    }
    public void ActivarNegocaciónMartillo()
    {
        StartCoroutine(ActivateObjectWithDelay(dialogosDiosaTienda2[1], 2f));
        dialogosDiosaTienda2[0].SetActive(false);
        animator.Play("AnimaciónNegocacionMartillo");
    }
    public void ActivarNegocaciónShotgun()
    {
        StartCoroutine(ActivateObjectWithDelay(dialogosDiosaTienda2[1], 2f));
        dialogosDiosaTienda2[0].SetActive(false);
        animator.Play("AnimaciónNegocacionShotgun");
    }
    public void ActivarNegocaciónDañoReducido()
    {
        StartCoroutine(ActivateObjectWithDelay(dialogosDiosaTienda2[1], 2f));
        dialogosDiosaTienda2[0].SetActive(false);
        animator.Play("AnimaciónNegocacionDañoReducido");
    }

    public void ComprarAlabarda()
    {
        for (int i = 0; i < botonesADesactivar.Length; i++)
        {
            botonesADesactivar[i].enabled = false;
        }
        PlayerEsporas playerEsporas = FindObjectOfType<PlayerEsporas>();


        if (playerEsporas != null)
        {
            float cantidadEsporas = playerEsporas.GetEsporas();

            if (cantidadEsporas >= precioBaseAlabarda)
            {
                for (int i = 0; i < freezeEnemies.Length; i++)
                {
                    freezeEnemies[i].ActivarComponentes();
                }
                DesbloquearAlabarda();
                interesDiosa = 1;
                dialogosDiosaTienda1[4].SetActive(true);
                dialogosDiosaTienda1[0].SetActive(false);
                StartCoroutine(DestroyObjectCoroutine(Tienda_Diosa_5, 4f));
                StartCoroutine(DestroyObjectCoroutine(dialogosDiosaTienda1[4], 4f));
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
        BolsaEsporas bolsaEsporas = FindAnyObjectByType<BolsaEsporas>();
        Debug.Log("Regateando...");

        PlayerEsporas playerEsporas = FindObjectOfType<PlayerEsporas>();
        StartCoroutine(DestroyObjectCoroutine(bolsaExporasExtra, 7f));
        bolsaExporasExtra.SetActive(false); 
        

        if (playerEsporas != null)
        {
            int valorSliderAlabarda = (int)sliderNegocaciónAlabarda.slider.value;
            float cantidadEsporas = playerEsporas.GetEsporas();

            //Precio Alabarda = 77
            if (valorSliderAlabarda == precioBaseAlabarda && cantidadEsporas >= precioBaseAlabarda)
            {
                Debug.Log("Pagando precio base");
                for (int i = 0; i < freezeEnemies.Length; i++)
                {
                    StartCoroutine(ActivarComponentesEnemigosConRetraso(i));
                }
               
                DesbloquearSniper();
                dialogosDiosaTienda1[4].SetActive(true);
                dialogosDiosaTienda1[1].SetActive(false);
                StartCoroutine(DestroyObjectCoroutine(Tienda_Diosa_5, 7f));
                playerEsporas.RestarEsporas(valorSliderAlabarda);
                interesDiosa = 1;
                ActualizarPrecioSniper();
                ;

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
                    StartCoroutine(ActivarComponentesEnemigosConRetraso(i));
                }
                bolsaExporasExtra.SetActive(false);
                StartCoroutine(DestroyObjectCoroutine(bolsaExporasExtra, 7f));
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
                    StartCoroutine(ActivarComponentesEnemigosConRetraso(i));
                }
                DesbloquearAlabarda();
                interesDiosa = interesDiosa * 1.2f;
                playerEsporas.RestarEsporas(valorSliderAlabarda);
                dialogosDiosaTienda1[2].SetActive(true);
                dialogosDiosaTienda1[1].SetActive(false);
                StartCoroutine(DestroyObjectCoroutine(Tienda_Diosa_5, 7f));
                StartCoroutine(DestroyObjectCoroutine(bolsaExporasExtra, 7f));
                ActualizarPrecioAlabarda();
            }
            else if (valorSliderAlabarda >= precioBaseAlabarda * interes_30_porciento && valorSliderAlabarda < precioBaseAlabarda && cantidadEsporas >= precioBaseAlabarda * interes_30_porciento)
            {
                Debug.Log("Desbloqueando Alabarada 30%");
                for (int i = 0; i < freezeEnemies.Length; i++)
                {
                    StartCoroutine(ActivarComponentesEnemigosConRetraso(i));
                }
                DesbloquearAlabarda();
                interesDiosa = interesDiosa * 1.3f;
                playerEsporas.RestarEsporas(valorSliderAlabarda);
                dialogosDiosaTienda1[2].SetActive(true);
                dialogosDiosaTienda1[1].SetActive(false);
                ActualizarPrecioAlabarda();
                StartCoroutine(DestroyObjectCoroutine(Tienda_Diosa_5, 7f));
                StartCoroutine(DestroyObjectCoroutine(bolsaExporasExtra, 7f));
                ActualizarPrecioAlabarda();
            }
            else if(valorSliderAlabarda > precioBaseAlabarda && cantidadEsporas >= precioBaseAlabarda)
            {
                Debug.Log("Desbloqueando Alabarada");
                for (int i = 0; i < freezeEnemies.Length; i++)
                {
                    StartCoroutine(ActivarComponentesEnemigosConRetraso(i));
                }
                DesbloquearAlabarda();
                dialogosDiosaTienda1[3].SetActive(true);
                dialogosDiosaTienda1[1].SetActive(false);
                ActualizarPrecioAlabarda();
                StartCoroutine(DestroyObjectCoroutine(Tienda_Diosa_5, 7f));
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
        for (int i = 0; i < botonesADesactivar.Length; i++)
        {
            botonesADesactivar[i].enabled = false;
        }
        PlayerEsporas playerEsporas = FindObjectOfType<PlayerEsporas>();

        if (playerEsporas != null)
        {
            float cantidadEsporas = playerEsporas.GetEsporas();

            if (cantidadEsporas >= precioBaseSniper)
            {
                int valorSliderSniper = (int)sliderNegocaciónSniper.slider.value;

                for (int i = 0; i < freezeEnemies.Length; i++)
                {
                    freezeEnemies[i].ActivarComponentes();
                }
                DesbloquearSniper();
                interesDiosa = 1;
                dialogosDiosaTienda1[4].SetActive(true);
                dialogosDiosaTienda1[0].SetActive(false);
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
        BolsaEsporas bolsaEsporas = FindAnyObjectByType<BolsaEsporas>();
        Debug.Log("Regateando...");

        PlayerEsporas playerEsporas = FindObjectOfType<PlayerEsporas>();

        if (playerEsporas != null)
        {
            int valorSliderSniper = (int)sliderNegocaciónSniper.slider.value;
            float cantidadEsporas = playerEsporas.GetEsporas();

            if (valorSliderSniper == precioBaseSniper && cantidadEsporas >= precioBaseSniper)
            {
                Debug.Log("Pagando precio base");
                for (int i = 0; i < freezeEnemies.Length; i++)
                {
                    StartCoroutine(ActivarComponentesEnemigosConRetraso(i));
                }
                DesbloquearSniper();
                dialogosDiosaTienda1[4].SetActive(true);
                dialogosDiosaTienda1[1].SetActive(false);
                StartCoroutine(DestroyObjectCoroutine(Tienda_Diosa_5, 7f));
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
                    StartCoroutine(ActivarComponentesEnemigosConRetraso(i));
                }
                DesbloquearSniper();
                interesDiosa = interesDiosa * 1.1f;
                dialogosDiosaTienda1[2].SetActive(true);
                dialogosDiosaTienda1[1].SetActive(false);
                StartCoroutine(DestroyObjectCoroutine(Tienda_Diosa_5, 7f));
                playerEsporas.RestarEsporas(valorSliderSniper);
                StartCoroutine(DestroyObjectCoroutine(bolsaExporasExtra, 7f));
                ActualizarPrecioSniper();
            }
            else if (valorSliderSniper >= precioBaseSniper * interes_20_porciento && valorSliderSniper < precioBaseSniper && cantidadEsporas >= precioBaseSniper * interes_20_porciento)
            {
                Debug.Log("Pagando 20% Menos");
                for (int i = 0; i < freezeEnemies.Length; i++)
                {
                    StartCoroutine(ActivarComponentesEnemigosConRetraso(i));
                }
                DesbloquearSniper();
                interesDiosa = interesDiosa * 1.2f;
                playerEsporas.RestarEsporas(valorSliderSniper);
                dialogosDiosaTienda1[2].SetActive(true);
                dialogosDiosaTienda1[1].SetActive(false);
                ActualizarPrecioAlabarda();
                StartCoroutine(DestroyObjectCoroutine(bolsaExporasExtra, 7f));
                StartCoroutine(DestroyObjectCoroutine(Tienda_Diosa_5, 7f));

            }
            else if (valorSliderSniper >= precioBaseSniper * interes_30_porciento && valorSliderSniper < precioBaseSniper && cantidadEsporas >= precioBaseSniper * interes_30_porciento)
            {
                Debug.Log("Pagando 30% Menos");
                for (int i = 0; i < freezeEnemies.Length; i++)
                {
                    StartCoroutine(ActivarComponentesEnemigosConRetraso(i));
                }
                DesbloquearSniper();
                interesDiosa = interesDiosa * 1.3f;
                playerEsporas.RestarEsporas(valorSliderSniper);
                dialogosDiosaTienda1[2].SetActive(true);
                dialogosDiosaTienda1[1].SetActive(false);
                ActualizarPrecioSniper();
                StartCoroutine(DestroyObjectCoroutine(Tienda_Diosa_5, 7f));
                ActualizarPrecioAlabarda();
                StartCoroutine(DestroyObjectCoroutine(bolsaExporasExtra, 7f));
            }
            else if (valorSliderSniper > precioBaseSniper)
            {
                for (int i = 0; i < freezeEnemies.Length; i++)
                {
                    StartCoroutine(ActivarComponentesEnemigosConRetraso(i));
                }
                DesbloquearSniper();
                dialogosDiosaTienda1[3].SetActive(true);
                dialogosDiosaTienda1[1].SetActive(false);
                StartCoroutine(DestroyObjectCoroutine(Tienda_Diosa_5, 7f));
                playerEsporas.RestarEsporas(valorSliderSniper);
                interesDiosa = 1;
                ActualizarPrecioSniper();
                StartCoroutine(DestroyObjectCoroutine(bolsaExporasExtra, 7f));

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
        for (int i = 0; i < botonesADesactivar.Length; i++)
        {
            botonesADesactivar[i].enabled = false;
        }
        PlayerEsporas playerEsporas = FindObjectOfType<PlayerEsporas>();

        if (playerEsporas != null)
        {
            float cantidadEsporas = playerEsporas.GetEsporas();

            if (cantidadEsporas >= precioBaseMartilloGigante)
            {
                for (int i = 0; i < freezeEnemies.Length; i++)
                {
                    freezeEnemies[i].ActivarComponentes();
                }
                DesbloquearMartilloGigante();
                interesDiosa = 1;
                dialogosDiosaTienda2[4].SetActive(true);
                dialogosDiosaTienda2[0].SetActive(false);
                StartCoroutine(DestroyObjectCoroutine(Tienda_Diosa_10, 4f));
                StartCoroutine(DestroyObjectCoroutine(dialogosDiosaTienda2[4], 4f));
                StartCoroutine(DestroyObjectCoroutine(bolsaExporasExtra, 4f));
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

        BolsaEsporas bolsaEsporas = FindAnyObjectByType<BolsaEsporas>();
        Debug.Log("Regateando...");

        PlayerEsporas playerEsporas = FindObjectOfType<PlayerEsporas>();

        if (playerEsporas != null)
        {
            int valorSliderMartillo = (int)sliderNegocaciónMartillo.slider.value;
            float cantidadEsporas = playerEsporas.GetEsporas();
           
            if(valorSliderMartillo == precioBaseMartilloGigante && cantidadEsporas >= precioBaseMartilloGigante)
            {
                Debug.Log("Pagando precio base");
                for (int i = 0; i < freezeEnemies.Length; i++)
                {
                    StartCoroutine(ActivarComponentesEnemigosConRetraso(i));
                }

                DesbloquearMartilloGigante();
                dialogosDiosaTienda2[4].SetActive(true);
                dialogosDiosaTienda2[1].SetActive(false);
                StartCoroutine(DestroyObjectCoroutine(Tienda_Diosa_5, 7f));
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
                    StartCoroutine(ActivarComponentesEnemigosConRetraso(i));
                }
                DesbloquearMartilloGigante();
                interesDiosa = interesDiosa * 1.1f;
                dialogosDiosaTienda2[2].SetActive(true);
                dialogosDiosaTienda2[1].SetActive(false);
                StartCoroutine(DestroyObjectCoroutine(Tienda_Diosa_10, 7f));
                playerEsporas.RestarEsporas(valorSliderMartillo);
                StartCoroutine(DestroyObjectCoroutine(bolsaExporasExtra, 7f));
                ActualizarPrecioMartillo();
            }
            else if (valorSliderMartillo >= precioBaseMartilloGigante * interes_20_porciento && valorSliderMartillo < precioBaseMartilloGigante && cantidadEsporas >= precioBaseMartilloGigante * interes_20_porciento)
            {
                for (int i = 0; i < freezeEnemies.Length; i++)
                {
                    StartCoroutine(ActivarComponentesEnemigosConRetraso(i));
                }
                DesbloquearMartilloGigante();
                interesDiosa = interesDiosa * 1.2f;
                playerEsporas.RestarEsporas(valorSliderMartillo);
                dialogosDiosaTienda2[2].SetActive(true);
                dialogosDiosaTienda2[1].SetActive(false);
                ActualizarPrecioMartillo();
                StartCoroutine(DestroyObjectCoroutine(bolsaExporasExtra, 7f));
                StartCoroutine(DestroyObjectCoroutine(Tienda_Diosa_10, 7f));

            }
            else if (valorSliderMartillo >= precioBaseMartilloGigante * interes_30_porciento && valorSliderMartillo < precioBaseMartilloGigante && cantidadEsporas >= precioBaseMartilloGigante * interes_30_porciento)
            {
                for (int i = 0; i < freezeEnemies.Length; i++)
                {
                    StartCoroutine(ActivarComponentesEnemigosConRetraso(i));
                }
                DesbloquearMartilloGigante();
                interesDiosa = interesDiosa * 1.3f;
                playerEsporas.RestarEsporas(valorSliderMartillo);
                dialogosDiosaTienda2[2].SetActive(true);
                dialogosDiosaTienda2[1].SetActive(false);
                ActualizarPrecioMartillo();
                StartCoroutine(DestroyObjectCoroutine(Tienda_Diosa_10, 7f));
                StartCoroutine(DestroyObjectCoroutine(bolsaExporasExtra, 7f));
            }
            else if (valorSliderMartillo > precioBaseMartilloGigante)
            {
                for (int i = 0; i < freezeEnemies.Length; i++)
                {
                    StartCoroutine(ActivarComponentesEnemigosConRetraso(i));
                }
                DesbloquearMartilloGigante();
                dialogosDiosaTienda2[3].SetActive(true);
                dialogosDiosaTienda2[1].SetActive(false);
                StartCoroutine(DestroyObjectCoroutine(Tienda_Diosa_10, 7f));
                playerEsporas.RestarEsporas(valorSliderMartillo);
                interesDiosa = 1;
                ActualizarPrecioMartillo();
                StartCoroutine(DestroyObjectCoroutine(bolsaExporasExtra, 7f));


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
        }
        ActualizarPrecioMartillo();
    }
    public void ComprarShotgun()
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
                DesbloquearShotgun();
                interesDiosa = 1;
                dialogosDiosaTienda2[4].SetActive(true);
                dialogosDiosaTienda2[0].SetActive(false);
                StartCoroutine(DestroyObjectCoroutine(Tienda_Diosa_10, 4f));
                StartCoroutine(DestroyObjectCoroutine(dialogosDiosaTienda2[4], 4f));
                StartCoroutine(DestroyObjectCoroutine(bolsaExporasExtra, 4f));
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
    public void NegociarShotgun()
    {
        BolsaEsporas bolsaEsporas = FindAnyObjectByType<BolsaEsporas>();
        Debug.Log("Regateando...");

        PlayerEsporas playerEsporas = FindObjectOfType<PlayerEsporas>();

        if (playerEsporas != null)
        {
            int valorSliderShotgun = (int)sliderNegocaciónShotgun.slider.value;
            float cantidadEsporas = playerEsporas.GetEsporas();
            if (valorSliderShotgun == precioBaseShotgun && cantidadEsporas >= precioBaseShotgun)
            {
                Debug.Log("Pagando precio base");
                for (int i = 0; i < freezeEnemies.Length; i++)
                {
                    StartCoroutine(ActivarComponentesEnemigosConRetraso(i));
                }

                DesbloquearShotgun();
                dialogosDiosaTienda2[4].SetActive(true);
                dialogosDiosaTienda2[1].SetActive(false);
                StartCoroutine(DestroyObjectCoroutine(Tienda_Diosa_5, 7f));
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
                    StartCoroutine(ActivarComponentesEnemigosConRetraso(i));
                }
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
                    StartCoroutine(ActivarComponentesEnemigosConRetraso(i));
                }
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
                    StartCoroutine(ActivarComponentesEnemigosConRetraso(i));
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
            else if (valorSliderShotgun >= precioBaseShotgun)
            {
                for (int i = 0; i < freezeEnemies.Length; i++)
                {
                    StartCoroutine(ActivarComponentesEnemigosConRetraso(i));
                }
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
        }
    }

    public void ComprarAumentoDaño()
    {
        for (int i = 0; i < botonesADesactivar.Length; i++)
        {
            botonesADesactivar[i].enabled = false;
        }
        PlayerEsporas playerEsporas = FindObjectOfType<PlayerEsporas>();

        if (playerEsporas != null)
        {
            float cantidadEsporas = playerEsporas.GetEsporas();

            if (cantidadEsporas >= precioBaseDañoAumentado)
            {
                aumentarDaño.ActivateAdditionalDamage();
                interesDiosa = 1;
                dialogosDiosaTienda1[4].SetActive(true);
                dialogosDiosaTienda1[0].SetActive(false);
                StartCoroutine(DestroyObjectCoroutine(Tienda_Diosa_5, 4f));
                StartCoroutine(DestroyObjectCoroutine(dialogosDiosaTienda1[4], 4f));
                StartCoroutine(DestroyObjectCoroutine(bolsaExporasExtra, 4f));
                playerEsporas.RestarEsporas(precioBaseDañoAumentado);
            }
            else
            {
                esporasInsuficientes.SetActive(true);
                StartCoroutine(DestroyObjectCoroutine(esporasInsuficientes, 0.5f));

            }
        }
        ActualizarPrecioAumentardaño();
    }
    public void NegociarDañoAumentado()
    {
        BolsaEsporas bolsaEsporas = FindAnyObjectByType<BolsaEsporas>();
        Debug.Log("Regateando...");

        PlayerEsporas playerEsporas = FindObjectOfType<PlayerEsporas>();

        if (playerEsporas != null)
        {
            int valorSliderDañoAumentado = (int)sliderNegocaciónDañoAumentado.slider.value;
            float cantidadEsporas = playerEsporas.GetEsporas();


            if (valorSliderDañoAumentado >= precioBaseDañoAumentado * interes_10_porciento && valorSliderDañoAumentado < precioBaseDañoAumentado && cantidadEsporas >= precioBaseDañoAumentado * interes_10_porciento)
            {
                Debug.Log("Desbloqueando Daño Aumentado 10");
                for (int i = 0; i < freezeEnemies.Length; i++)
                {
                    StartCoroutine(ActivarComponentesEnemigosConRetraso(i));
                }
                aumentarDaño.ActivateAdditionalDamage();
                interesDiosa = interesDiosa * 1.1f;
                dialogosDiosaTienda1[2].SetActive(true);
                dialogosDiosaTienda1[1].SetActive(false);
                StartCoroutine(DestroyObjectCoroutine(Tienda_Diosa_5, 7f));
                playerEsporas.RestarEsporas(valorSliderDañoAumentado);
                StartCoroutine(DestroyObjectCoroutine(bolsaExporasExtra, 7f));
                ActualizarPrecioAumentardaño();
            }
            else if (valorSliderDañoAumentado >= precioBaseDañoAumentado * interes_20_porciento && valorSliderDañoAumentado < precioBaseDañoAumentado && cantidadEsporas >= precioBaseDañoAumentado * interes_20_porciento)
            {
                Debug.Log("Desbloqueando Daño Aumentado 20%");
                for (int i = 0; i < freezeEnemies.Length; i++)
                {
                    StartCoroutine(ActivarComponentesEnemigosConRetraso(i));
                }
                aumentarDaño.ActivateAdditionalDamage();
                interesDiosa = interesDiosa * 1.2f;
                playerEsporas.RestarEsporas(valorSliderDañoAumentado);
                dialogosDiosaTienda1[2].SetActive(true);
                dialogosDiosaTienda1[1].SetActive(false);
                StartCoroutine(DestroyObjectCoroutine(Tienda_Diosa_5, 7f));
                StartCoroutine(DestroyObjectCoroutine(bolsaExporasExtra, 7f));
                ActualizarPrecioAumentardaño();
            }
            else if (valorSliderDañoAumentado >= precioBaseDañoAumentado * interes_30_porciento && valorSliderDañoAumentado < precioBaseDañoAumentado && cantidadEsporas >= precioBaseDañoAumentado * interes_30_porciento)
            {
                Debug.Log("Desbloqueando Daño Aumentado 30%");
                for (int i = 0; i < freezeEnemies.Length; i++)
                {
                    StartCoroutine(ActivarComponentesEnemigosConRetraso(i));
                }
                aumentarDaño.ActivateAdditionalDamage();
                interesDiosa = interesDiosa * 1.3f;
                playerEsporas.RestarEsporas(valorSliderDañoAumentado);
                dialogosDiosaTienda1[2].SetActive(true);
                dialogosDiosaTienda1[1].SetActive(false);
                StartCoroutine(DestroyObjectCoroutine(bolsaExporasExtra, 7f));
                ActualizarPrecioAlabarda();
                StartCoroutine(DestroyObjectCoroutine(Tienda_Diosa_5, 7f));
                ActualizarPrecioAumentardaño();
            }
            else if (valorSliderDañoAumentado >= precioBaseDañoAumentado && cantidadEsporas >= precioBaseDañoAumentado)
            {
                Debug.Log("Desbloqueando Daño Aumentado");
                for (int i = 0; i < freezeEnemies.Length; i++)
                {
                    StartCoroutine(ActivarComponentesEnemigosConRetraso(i));
                }
                aumentarDaño.ActivateAdditionalDamage();
                dialogosDiosaTienda1[3].SetActive(true);
                dialogosDiosaTienda1[1].SetActive(false);
                ActualizarPrecioAlabarda();
                StartCoroutine(DestroyObjectCoroutine(Tienda_Diosa_5, 7f));
                playerEsporas.RestarEsporas(valorSliderDañoAumentado);
                interesDiosa = 1;
                ActualizarPrecioAumentardaño();
                StartCoroutine(DestroyObjectCoroutine(bolsaExporasExtra, 7f));
                extraSpores = valorSliderDañoAumentado - precioBaseDañoAumentado;

                StartCoroutine(bolsaEsporas.InstantiateEsporas(extraSpores));
                SaveExtraSpores();
                UpdateExtraSporesText();


            }
            else if (valorSliderDañoAumentado > precioBaseDañoAumentado && cantidadEsporas < precioBaseDañoAumentado)
            {
                esporasInsuficientes.SetActive(true);
                StartCoroutine(DestroyObjectCoroutine(esporasInsuficientes, 0.5f));
            }
            else if (valorSliderDañoAumentado < precioBaseDañoAumentado && cantidadEsporas < precioBaseDañoAumentado)
            {
                esporasInsuficientes.SetActive(true);
                StartCoroutine(DestroyObjectCoroutine(esporasInsuficientes, 0.5f));
            }
        }
    }
   
    public void ComprarDañoReducido()
    {
        for (int i = 0; i < botonesADesactivar.Length; i++)
        {
            botonesADesactivar[i].enabled = false;
        }
        PlayerEsporas playerEsporas = FindObjectOfType<PlayerEsporas>();

        if (playerEsporas != null)
        {
            float cantidadEsporas = playerEsporas.GetEsporas();

            if (cantidadEsporas >= precioBaseAlmaDañoReducido)
            {
                reducirDaño.ActivateReducedDamage();
                interesDiosa = 1;
                dialogosDiosaTienda2[4].SetActive(true);
                dialogosDiosaTienda2[0].SetActive(false);
                StartCoroutine(DestroyObjectCoroutine(Tienda_Diosa_10, 4f));
                StartCoroutine(DestroyObjectCoroutine(dialogosDiosaTienda2[4], 4f));
                StartCoroutine(DestroyObjectCoroutine(bolsaExporasExtra, 4f));
                playerEsporas.RestarEsporas(precioBaseAlmaDañoReducido);
            }
            else
            {
                esporasInsuficientes.SetActive(true);
                StartCoroutine(DestroyObjectCoroutine(esporasInsuficientes, 0.5f));

            }
        }
        ActualizarPrecioDañoReducido();
    }

    public void NegociarDañoReducido()
    {
        BolsaEsporas bolsaEsporas = FindAnyObjectByType<BolsaEsporas>();
        Debug.Log("Regateando...");

        PlayerEsporas playerEsporas = FindObjectOfType<PlayerEsporas>();

        if (playerEsporas != null)
        {
            int valorSliderDañoReducido = (int)sliderNegocaciónDañoReducido.slider.value;
            float cantidadEsporas = playerEsporas.GetEsporas();


            if (valorSliderDañoReducido >= precioBaseAlmaDañoReducido * interes_10_porciento && valorSliderDañoReducido < precioBaseAlmaDañoReducido && cantidadEsporas >= precioBaseAlmaDañoReducido * interes_10_porciento)
            {
                Debug.Log("Desbloqueando Daño Reducido 10");
                for (int i = 0; i < freezeEnemies.Length; i++)
                {
                    StartCoroutine(ActivarComponentesEnemigosConRetraso(i));
                }
                reducirDaño.ActivateReducedDamage();
                interesDiosa = interesDiosa * 1.1f;
                dialogosDiosaTienda2[2].SetActive(true);
                dialogosDiosaTienda2[1].SetActive(false);
                StartCoroutine(DestroyObjectCoroutine(Tienda_Diosa_10, 7f));
                playerEsporas.RestarEsporas(valorSliderDañoReducido);
                StartCoroutine(DestroyObjectCoroutine(bolsaExporasExtra, 7f));
                ActualizarPrecioAumentardaño();
            }
            else if (valorSliderDañoReducido >= precioBaseAlmaDañoReducido * interes_20_porciento && valorSliderDañoReducido < precioBaseAlmaDañoReducido && cantidadEsporas >= precioBaseAlmaDañoReducido * interes_20_porciento)
            {
                Debug.Log("Desbloqueando Daño Reducido 20%");
                for (int i = 0; i < freezeEnemies.Length; i++)
                {
                    StartCoroutine(ActivarComponentesEnemigosConRetraso(i));
                }
                reducirDaño.ActivateReducedDamage(); ;
                interesDiosa = interesDiosa * 1.2f;
                playerEsporas.RestarEsporas(valorSliderDañoReducido);
                dialogosDiosaTienda2[2].SetActive(true);
                dialogosDiosaTienda2[1].SetActive(false);
                StartCoroutine(DestroyObjectCoroutine(Tienda_Diosa_10, 7f));
                StartCoroutine(DestroyObjectCoroutine(bolsaExporasExtra, 7f));
                ActualizarPrecioAumentardaño();
            }
            else if (valorSliderDañoReducido >= precioBaseAlmaDañoReducido * interes_30_porciento && valorSliderDañoReducido < precioBaseAlmaDañoReducido && cantidadEsporas >= precioBaseAlmaDañoReducido * interes_30_porciento)
            {
                Debug.Log("Desbloqueando Daño Reducido 30%");
                for (int i = 0; i < freezeEnemies.Length; i++)
                {
                    StartCoroutine(ActivarComponentesEnemigosConRetraso(i));
                }
                reducirDaño.ActivateReducedDamage();
                interesDiosa = interesDiosa * 1.3f;
                playerEsporas.RestarEsporas(valorSliderDañoReducido);
                dialogosDiosaTienda2[2].SetActive(true);
                dialogosDiosaTienda2[1].SetActive(false);
                StartCoroutine(DestroyObjectCoroutine(bolsaExporasExtra, 7f));
                ActualizarPrecioAlabarda();
                StartCoroutine(DestroyObjectCoroutine(Tienda_Diosa_10, 7f));
                ActualizarPrecioAumentardaño();
            }
            else if (valorSliderDañoReducido >= precioBaseAlmaDañoReducido && cantidadEsporas >= precioBaseAlmaDañoReducido)
            {
                Debug.Log("Desbloqueando Daño Reducido");
                for (int i = 0; i < freezeEnemies.Length; i++)
                {
                    StartCoroutine(ActivarComponentesEnemigosConRetraso(i));
                }
                reducirDaño.ActivateReducedDamage();
                dialogosDiosaTienda2[3].SetActive(true);
                dialogosDiosaTienda2[1].SetActive(false);
                ActualizarPrecioAlabarda();
                StartCoroutine(DestroyObjectCoroutine(Tienda_Diosa_10, 7f));
                playerEsporas.RestarEsporas(valorSliderDañoReducido);
                interesDiosa = 1;
                ActualizarPrecioAumentardaño();
                StartCoroutine(DestroyObjectCoroutine(bolsaExporasExtra, 7f));
                extraSpores = valorSliderDañoReducido - precioBaseAlmaDañoReducido;

                StartCoroutine(bolsaEsporas.InstantiateEsporas(extraSpores));
                SaveExtraSpores();
                UpdateExtraSporesText();


            }
            else if (valorSliderDañoReducido > precioBaseAlmaDañoReducido && cantidadEsporas < precioBaseAlmaDañoReducido)
            {
                esporasInsuficientes.SetActive(true);
                StartCoroutine(DestroyObjectCoroutine(esporasInsuficientes, 0.5f));
            }
            else if (valorSliderDañoReducido < precioBaseAlmaDañoReducido && cantidadEsporas < precioBaseAlmaDañoReducido)
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
        Debug.Log("Desbloqueando Martillo");
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
    private void ActualizarPrecioAumentardaño()
    {
        precioBaseDañoAumentado = precioBaseDañoAumentado * interesDiosa;

        if(precioDañoAumentadoText != null)
        {
            precioDañoAumentadoText.text = precioBaseDañoAumentado.ToString();
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

    private void ActualizarPrecioDañoReducido()
    {
        precioBaseAlmaDañoReducido = precioBaseAlmaDañoReducido * interesDiosa;

        if (precioShotgunText != null)
        {
            precioDañoReducidoText.text = precioBaseAlmaDañoReducido.ToString();
        }
    }

    IEnumerator ActivarComponentesEnemigosConRetraso(int i)
    {
        yield return new WaitForSeconds(7f); 
        freezeEnemies[i].ActivarComponentes();
    }

}


