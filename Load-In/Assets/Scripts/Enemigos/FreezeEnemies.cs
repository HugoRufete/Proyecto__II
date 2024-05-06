using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FreezeEnemies : MonoBehaviour
{
    public Component[] components;
    public void DesactivarComponentes()
    {
        Debug.Log("Desactivando Componentes");
        foreach (Component component in components)
        {
            if (component is Behaviour behaviour)
            {
                behaviour.enabled = false;
            }
        }
    }

    public void ActivarComponentes()
    {
        Debug.Log("Activando Componentes");
        foreach (Component component in components)
        {
            if (component is Behaviour behaviour)
            {
                behaviour.enabled = true;
            }
        }
    }
}