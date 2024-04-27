using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderScript : MonoBehaviour
{
    public Slider slider;

    // Método para configurar el valor máximo, mínimo y actual del slider
    public void SetMinMaxEsporas(int minEsporas, int maxEsporas)
    {
        if (slider != null)
        {
            slider.minValue = minEsporas;
            slider.maxValue = maxEsporas;
            slider.value = maxEsporas; // Establecer el valor inicial como el máximo
        }
        else
        {
            Debug.LogError("El Slider no está asignado en SliderScript.");
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
            Debug.LogError("El Slider no está asignado en SliderScript.");
        }
    }

    // Método para obtener el valor mínimo del slider
    public float GetMinValue()
    {
        if (slider != null)
        {
            return slider.minValue;
        }
        else
        {
            Debug.LogError("El Slider no está asignado en SliderScript.");
            return 0f;
        }
    }
}
