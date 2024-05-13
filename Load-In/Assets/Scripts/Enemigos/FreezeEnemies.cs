using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FreezeEnemies : MonoBehaviour
{
    public Component[] components;

    Recolector recolector;

    private void Start()
    {
        recolector = GetComponent<Recolector>();
    }
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

        if(recolector != null)
        {
            GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        }
    }

    public void ActivarComponentes()
    {
        Debug.Log("Activando Componentes");
        foreach (Component component in components)
        {
            Behaviour behaviour = component as Behaviour;
            if (behaviour != null && behaviour.gameObject.activeSelf)
            {
                behaviour.enabled = true;
            }
            else
            {
                Debug.Log("No hay enemigos que activar");
            }
        }

        if (recolector != null)
        {
            Debug.Log("Player's health is null, resetting velocity.");
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = Vector3.zero;
            }
        }
    }
}