using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponParent : MonoBehaviour
{
    // Variable para indicar si se ha aplicado la rotación en el eje Y
    bool rotatedY = false;
    // Variable para almacenar la rotación en Z antes de aplicar la rotación en Y
    float prevAngleZ = 0f;

    void Update()
    {
        // Obtiene la posición del ratón en el mundo
        Vector3 posicionRaton = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        posicionRaton.z = 0; // Ajusta la posición Z a 0 para que esté en el mismo plano que el jugador

        // Apunta el objeto hacia la posición del ratón
        ApuntarAlRaton(posicionRaton);
    }

    void ApuntarAlRaton(Vector3 objetivo)
    {
        // Obtiene la dirección hacia el ratón y ajusta la rotación del objeto
        Vector3 direccion = objetivo - transform.position;
        float anguloZ = Mathf.Atan2(direccion.y, direccion.x) * Mathf.Rad2Deg;

        // Si el ángulo supera los 90 grados o es menor que -90 grados en Z, ajusta la rotación en Y
        if (anguloZ > 90 || anguloZ < -90)
        {
            // Rotación en Y necesaria para mantener coherencia
            float anguloY = 180 - transform.rotation.eulerAngles.y;

            // Aplicar rotación en Y
            transform.Rotate(Vector3.up, anguloY, Space.World);

            // Marcar como rotado en Y
            rotatedY = true;

            // "Reiniciar" la rotación en Z
            prevAngleZ = anguloZ;
        }
        else
        {
            // Si la rotación en el eje Z ha "reiniciado", restaurar la rotación en Z al valor anterior
            if (rotatedY)
            {
                float deltaZ = anguloZ - prevAngleZ;
                transform.Rotate(Vector3.forward, -deltaZ, Space.World);
                prevAngleZ = anguloZ;
            }
            // Restaurar rotación en Y si se había rotado previamente
            if (rotatedY)
            {
                transform.Rotate(Vector3.up, -180, Space.World);
                rotatedY = false;
            }
        }

        // Aplicar rotación en Z
        transform.rotation = Quaternion.AngleAxis(anguloZ, Vector3.forward);
    }
}
