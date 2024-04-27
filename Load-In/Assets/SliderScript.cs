using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderScript : MonoBehaviour
{
    public Slider slider;

    // Método para configurar el valor máximo y actual del slider
    public void SetMaxEsporas(int esporas)
    {
        if (slider != null)
        {
            slider.maxValue = esporas;
            slider.value = esporas;
        }
        else
        {
            Debug.LogError("El Slider no está asignado en SliderScript.");
        }
    }

    // Método para actualizar el valor actual del slider
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
}
