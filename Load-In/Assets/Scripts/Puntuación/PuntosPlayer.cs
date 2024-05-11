using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PuntosPlayer : MonoBehaviour
{
    public int maxExperiencia;
    public float experiencia;
    public Image BarraDeExperiencia;
    public TMP_Text nivelTexto;
    public int maxNivel;
    public float experienciaPorObjeto;

    public GameObject tiendaDiosa_1;
    public GameObject tiendaDiosa_2;
    public GameObject tiendaDiosa_3;
    public GameObject tiendaDiosa_4;
    public int nivel = 1;

    bool tiendaDiosa1Desbloqueada = false;
    bool tiendaDiosa2Desbloqueada = false;
    bool tiendaDiosa3Desbloqueada = false;
    bool tiendaDiosa4Desbloqueada = false;

    bool tiendaDiosa1Activada = false;
    bool tiendaDiosa2Activada = false;
    bool tiendaDiosa3Activada = false;
    bool tiendaDiosa4Activada = false;

    [Header("Pop Up's Tienda Diosa")]
    public GameObject popUpParent;
    public GameObject popUpMensajeTiendaDiosa_1;
    public GameObject popUpMensajeTiendaDiosa_2;
    public GameObject popUpMensajeTiendaDiosa_3;
    public GameObject popUpMensajeTiendaDiosa_4;
    public GameObject popUpPreesF;

    public Animator textAnimator;
    FreezeEnemies [] freezeEnemies;
    GameManager gameManager;

    public GameObject minimapa;

    private void Update()
    {
        freezeEnemies = FindObjectsOfType<FreezeEnemies>();
        gameManager = FindObjectOfType<GameManager>();

        if (nivel >= 5 && !tiendaDiosa1Activada)
        {
            minimapa.SetActive(false);
            tiendaDiosa1Desbloqueada = true;
            popUpParent.SetActive(true);
            popUpMensajeTiendaDiosa_1.SetActive(true);
            popUpPreesF.SetActive(true);
            textAnimator.Play("PopUp_1_Animation");


            if (tiendaDiosa1Desbloqueada)
            {
                for (int i = 0; i < freezeEnemies.Length; i++)
                {
                    Debug.Log("Congelando Enemigos");
                    freezeEnemies[i].DesactivarComponentes();

                }

                gameManager.enabled = false;
                tiendaDiosa_1.SetActive(true);
                popUpMensajeTiendaDiosa_1.SetActive(false);
                popUpPreesF.SetActive(false);
                tiendaDiosa1Activada = true;
            }
        }

        else if (nivel >= 10 && !tiendaDiosa2Activada)
        {
            minimapa.SetActive(false);
            popUpParent.SetActive(true);
            tiendaDiosa2Desbloqueada = true;
            popUpMensajeTiendaDiosa_2.SetActive(true);
            popUpPreesF.SetActive(true);
            textAnimator.Play("PopUp_1_Animation");

            if (tiendaDiosa2Desbloqueada)
            {
                for (int i = 0; i < freezeEnemies.Length; i++)
                {
                    freezeEnemies[i].DesactivarComponentes();
                }

                gameManager.enabled = false;
                Debug.Log("Abriendo tienda level 10");
                tiendaDiosa_2.SetActive(true);
                popUpMensajeTiendaDiosa_2.SetActive(false);
                popUpPreesF.SetActive(false);
                Time.timeScale = 0.0f;
                tiendaDiosa2Activada = true;
            }
            
        }
        else if (nivel >= 15 && !tiendaDiosa3Activada)
        {
            minimapa.SetActive(false);
            popUpParent.SetActive(true);
            tiendaDiosa3Desbloqueada = true;
            popUpMensajeTiendaDiosa_3.SetActive(true);
            popUpPreesF.SetActive(true);
            textAnimator.Play("PopUp_1_Animation");
            
            if (tiendaDiosa3Desbloqueada)
            {
                for (int i = 0; i < freezeEnemies.Length; i++)
                {
                    freezeEnemies[i].DesactivarComponentes();
                }
                gameManager.enabled = false;
                popUpMensajeTiendaDiosa_3.SetActive(false);
                tiendaDiosa_3.SetActive(true);
                Time.timeScale = 0.0f;
                popUpPreesF.SetActive(false);
                tiendaDiosa3Activada = true;

            }
            
        }
        else if (nivel >= 20 && !tiendaDiosa4Activada)
        {
            minimapa.SetActive(false);
            popUpParent.SetActive(true);
            tiendaDiosa4Desbloqueada = true;
            popUpMensajeTiendaDiosa_4.SetActive(true);
            popUpPreesF.SetActive(true);
            textAnimator.Play("PopUp_1_Animation");

            if (tiendaDiosa4Desbloqueada)
            {
                for (int i = 0; i < freezeEnemies.Length; i++)
                {
                    freezeEnemies[i].DesactivarComponentes();
                }
                gameManager.enabled = false;
                popUpMensajeTiendaDiosa_4.SetActive(false);
                tiendaDiosa_4.SetActive(true);
                popUpPreesF.SetActive(false);
                Time.timeScale = 0.0f;
                tiendaDiosa4Activada = true;
            }


        }
    }
    private void UpdateBarraDeExperiencia()
    {
        float fillAmount = (float)experiencia / maxExperiencia;
        BarraDeExperiencia.fillAmount = fillAmount;
    }

    public void RecogerExperiencia(float cantidad)
    {
        experiencia += cantidad;
        while (experiencia >= maxExperiencia)
        {
            nivel++;
            experiencia -= maxExperiencia; // Reset de la experiencia máxima
            maxExperiencia *= 2;
            nivelTexto.text = nivel.ToString();
        }
        UpdateBarraDeExperiencia();

        if (nivel >= maxNivel)
        {
            ResetearExperiencia();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Experiencia"))
        {
            RecogerExperiencia(experienciaPorObjeto);
            Destroy(collision.gameObject);
        }
    }

    // Método para restablecer la experiencia y el nivel
    private void ResetearExperiencia()
    {
        nivel = 1;
        maxExperiencia = 100;
        experiencia = 0;
        nivelTexto.text = nivel.ToString();
        UpdateBarraDeExperiencia();
    }

    public void DesbloquearTiendaDiosa_1()
   {
        tiendaDiosa1Desbloqueada = true;
   }
    public void DesbloquearTiendaDiosa_2()
    {
        tiendaDiosa2Desbloqueada = true;
    }
    public void DesbloquearTiendaDiosa_3()
    {
        tiendaDiosa3Desbloqueada = true;
    }
    public void DesbloquearTiendaDiosa_4()
    {
        tiendaDiosa4Desbloqueada = true;
    }


}
