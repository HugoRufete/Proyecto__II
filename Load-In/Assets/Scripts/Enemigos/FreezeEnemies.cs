using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FreezeEnemies : MonoBehaviour
{
    public Component[] components;
    public Rigidbody2D[] rigidbodies;

    Recolector recolector;

    CircleExplosion circleExplosion;

    private void Start()
    {
        recolector = GetComponent<Recolector>();
        circleExplosion = FindAnyObjectByType<CircleExplosion>();
    }

    private void Update()
    {
        if (circleExplosion != null)
        {
            DesactivarComponentes();
        }
    }
    public void DesactivarComponentes()
    {
        Debug.Log("Desactivating Components");
        for (int i = 0; i < components.Length; i++)
        {
            Component component = components[i];
            if (component != null && component.gameObject.activeSelf)
            {
                Behaviour behaviour = component as Behaviour;
                if (behaviour != null)
                {
                    behaviour.enabled = false;
                }
            }
            else
            {
                Debug.Log("Component " + i + " has been destroyed or is not active");
            }
        }
        foreach (Rigidbody2D rb in rigidbodies)
        {
            if (rb != null && rb.gameObject.activeSelf)
            {
                rb.velocity = Vector3.zero;
            }
            else
            {
                Debug.Log("Rigidbody " + "x"+ " has been destroyed or is not active");
            }
        }
    }

    public void ActivarComponentes()
    {
        for (int i = 0; i < components.Length; i++)
        {
            Component component = components[i];
            if (component != null && component.gameObject.activeSelf)
            {
                Behaviour behaviour = component as Behaviour;
                if (behaviour != null)
                {
                    behaviour.enabled = true;
                }
            }
            else
            {
                Debug.Log("Component " + i + " has been destroyed or is not active");
            }
        }
        foreach (Rigidbody2D rb in rigidbodies)
        {
            if (rb != null && rb.gameObject.activeSelf)
            {
                rb.velocity = Vector3.one * 1f;
            }
            else
            {
                Debug.Log("No active rigidbodies to set velocity");
            }
        }
    }
}