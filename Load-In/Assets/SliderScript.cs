using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderScript : MonoBehaviour
{
    public Slider slider;

    // M�todo para configurar el valor m�ximo y actual del slider
    public void SetMaxEsporas(int esporas)
    {
        if (slider != null)
        {
            slider.maxValue = esporas;
            slider.value = esporas;
        }
        else
        {
            Debug.LogError("El Slider no est� asignado en SliderScript.");
        }
    }

    // M�todo para actualizar el valor actual del slider
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
}
