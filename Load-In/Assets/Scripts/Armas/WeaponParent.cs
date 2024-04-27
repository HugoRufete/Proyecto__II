using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponParent : MonoBehaviour
{
    private bool yScaleModified = false;

    WeaponParent weaponParent;

    private void Start()
    {
        weaponParent.enabled = false;
    }
    void Update()
    {
        // Obtiene la posici�n del rat�n en el mundo
        Vector3 posicionRaton = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        posicionRaton.z = 0; // Ajusta la posici�n Z a 0 para que est� en el mismo plano que el jugador

        // Apunta el objeto hacia la posici�n del rat�n
        ApuntarAlRaton(posicionRaton);
    }

    void ApuntarAlRaton(Vector3 objetivo)
    {
        // Obtiene la direcci�n hacia el rat�n y ajusta la rotaci�n del objeto
        Vector3 direccion = objetivo - transform.position;
        float angulo = Mathf.Atan2(direccion.y, direccion.x) * Mathf.Rad2Deg;

        // Calcula el cuaterni�n de rotaci�n
        Quaternion targetRotation = Quaternion.AngleAxis(angulo, Vector3.forward);

        // Verifica si el �ngulo en Z es mayor que 90 o menor que -90 y ajusta la escala en Y
        if (!yScaleModified && (angulo > 90f || angulo < -90f))
        {
            Vector3 nuevaEscala = transform.localScale;
            nuevaEscala.y = -1;
            transform.localScale = nuevaEscala;
            yScaleModified = true;
        }
        else if (yScaleModified && (angulo <= 90f && angulo >= -90f))
        {
            Vector3 nuevaEscala = transform.localScale;
            nuevaEscala.y = 1;
            transform.localScale = nuevaEscala;
            yScaleModified = false;
        }

        // Aplica la rotaci�n
        transform.rotation = targetRotation;
    }
}
