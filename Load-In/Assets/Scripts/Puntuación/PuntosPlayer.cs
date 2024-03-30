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

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            tiendaDiosa_4.SetActive(true);
        }

        if (nivel >= 5)
        {
            ActivarTiendaDiosa_Lev5();
            //tiendaDiosa_1.SetActive(true);
            Time.timeScale = 0.0f;
        }
        else if (nivel >= 10)
        {
            tiendaDiosa_2.SetActive(true);
            Time.timeScale = 0.0f;
        }
        else if (nivel >= 15)
        {
            tiendaDiosa_3.SetActive(true);
            Time.timeScale = 0.0f;
        }
        else if (nivel >= 20)
        {
            tiendaDiosa_4.SetActive(true);
            Time.timeScale = 0.0f;
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

    private void ActivarTiendaDiosa_Lev5()
    {
        tiendaDiosa_1.SetActive(true);
    }
}
