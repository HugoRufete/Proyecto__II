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
        if (nivel >= 5)
        {
            tiendaDiosa_1.SetActive(true);
            //Time.timeScale = 0.0f;
        }
        else if (experiencia >= 10)
        {
            tiendaDiosa_1.SetActive(true);
            Time.timeScale = 0.0f;
        }
        else if (experiencia >= 15)
        {
            tiendaDiosa_1.SetActive(true);
            Time.timeScale = 0.0f;
        }
        else if (experiencia >= 20)
        {
            tiendaDiosa_1.SetActive(true);
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
            experiencia -= maxExperiencia; // Restamos la experiencia máxima para mantener el excedente
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
}
