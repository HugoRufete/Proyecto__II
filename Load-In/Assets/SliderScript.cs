using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderScript : MonoBehaviour
{
    public Slider slider;

    // M�todo para configurar el valor m�ximo, m�nimo y actual del slider
    public void SetMinMaxEsporas(int minEsporas, int maxEsporas)
    {
        if (slider != null)
        {
            slider.minValue = minEsporas;
            slider.maxValue = maxEsporas;
            slider.value = maxEsporas; // Establecer el valor inicial como el m�ximo
        }
        else
        {
            Debug.LogError("El Slider no est� asignado en SliderScript.");
        }
    }

    public void SetEsporas(int esporas)
    {
        if (slider != null)
        {
            slider.value = esporas;
        }
        else
        {
            Debug.LogError("El Slider no est� asignado en SliderScript.");
        }
    }

    // M�todo para obtener el valor m�nimo del slider
    public float GetMinValue()
    {
        if (slider != null)
        {
            return slider.minValue;
        }
        else
        {
            Debug.LogError("El Slider no est� asignado en SliderScript.");
            return 0f;
        }
    }
}
