using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PuntosPlayer : MonoBehaviour
{

    public int maxExperiencia;
    public int experiencia;
    public Image BarraDeExperiencia;
    public TMP_Text nivelTexto;
    public int maxNivel;

    private int nivel = 1;

    private void UpdateBarraDeExperiencia()
    {
        BarraDeExperiencia.fillAmount = (float)experiencia / maxExperiencia;
    }

    public void RecogerExperiencia(int cantidad)
    {
        experiencia += cantidad;
        if (experiencia >= maxExperiencia)
        {
            nivel++;
            experiencia = 0;
            maxExperiencia *= 2;
            nivelTexto.text = nivel.ToString();
            UpdateBarraDeExperiencia();

            if (nivel == maxNivel)
            {
                ResetearExperiencia();
            }
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
