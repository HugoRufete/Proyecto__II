using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponParent : MonoBehaviour
{
    // Variable para indicar si se ha aplicado la rotaci�n en el eje Y
    bool rotatedY = false;
    // Variable para almacenar la rotaci�n en Z antes de aplicar la rotaci�n en Y
    float prevAngleZ = 0f;

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
        float anguloZ = Mathf.Atan2(direccion.y, direccion.x) * Mathf.Rad2Deg;

        // Si el �ngulo supera los 90 grados o es menor que -90 grados en Z, ajusta la rotaci�n en Y
        if (anguloZ > 90 || anguloZ < -90)
        {
            // Rotaci�n en Y necesaria para mantener coherencia
            float anguloY = 180 - transform.rotation.eulerAngles.y;

            // Aplicar rotaci�n en Y
            transform.Rotate(Vector3.up, anguloY, Space.World);

            // Marcar como rotado en Y
            rotatedY = true;

            // "Reiniciar" la rotaci�n en Z
            prevAngleZ = anguloZ;
        }
        else
        {
            // Si la rotaci�n en el eje Z ha "reiniciado", restaurar la rotaci�n en Z al valor anterior
            if (rotatedY)
            {
                float deltaZ = anguloZ - prevAngleZ;
                transform.Rotate(Vector3.forward, -deltaZ, Space.World);
                prevAngleZ = anguloZ;
            }
            // Restaurar rotaci�n en Y si se hab�a rotado previamente
            if (rotatedY)
            {
                transform.Rotate(Vector3.up, -180, Space.World);
                rotatedY = false;
            }
        }

        // Aplicar rotaci�n en Z
        transform.rotation = Quaternion.AngleAxis(anguloZ, Vector3.forward);
    }
}
