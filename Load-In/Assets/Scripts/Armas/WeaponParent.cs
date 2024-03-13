using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponParent : MonoBehaviour
{
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
        transform.rotation = Quaternion.AngleAxis(angulo, Vector3.forward);
    }
}
