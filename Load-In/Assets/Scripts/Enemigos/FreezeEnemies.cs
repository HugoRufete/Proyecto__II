using System.Collections;
using UnityEngine;

public class FreezeEnemies : MonoBehaviour
{
    public Component[] components;
    public Rigidbody2D[] rigidbodies;

    Recolector recolector;
    CircleExplosion circleExplosion;

    private bool componentsDisabled = false;

    private void Start()
    {
        recolector = GetComponent<Recolector>();
    }

    private void Update()
    {
        
    }

    public void DesactivarComponentes()
    {
        Debug.Log("Desactivando Componentes");
        foreach (var component in components)
        {
            if (component != null && component.gameObject.activeSelf)
            {
                if (component is Behaviour behaviour)
                {
                    behaviour.enabled = false;
                }
            }
            else
            {
                Debug.Log("Component ha sido destruido o no está activo");
            }
        }
        foreach (var rb in rigidbodies)
        {
            if (rb != null && rb.gameObject.activeSelf)
            {
                rb.velocity = Vector3.zero;
            }
            else
            {
                Debug.Log("Rigidbody ha sido destruido o no está activo");
            }
        }
        componentsDisabled = true;
    }

    public void ActivarComponentes()
    {
        Debug.Log("Activando Componentes");
        foreach (var component in components)
        {
            if (component != null && component.gameObject.activeSelf)
            {
                if (component is Behaviour behaviour)
                {
                    behaviour.enabled = true;
                }
            }
            else
            {
                Debug.Log("Component ha sido destruido o no está activo");
            }
        }
        foreach (var rb in rigidbodies)
        {
            if (rb != null && rb.gameObject.activeSelf)
            {
                rb.velocity = Vector3.one; // o cualquier otra lógica para reactivar el Rigidbody2D
            }
            else
            {
                Debug.Log("No hay rigidbodies activos para establecer la velocidad");
            }
        }
    }

    public IEnumerator ActivarComponentesConRetraso(float delay)
    {
        yield return new WaitForSeconds(delay);
        ActivarComponentes();
    }
}
