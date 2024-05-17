using UnityEngine;

public class MoveUp : MonoBehaviour
{
    public float speed = 20f; // Velocidad de movimiento hacia arriba

    void Update()
    {
        // Calcula el desplazamiento hacia arriba basado en la velocidad y el tiempo
        Vector3 displacement = Vector3.up * speed * Time.deltaTime;

        // Actualiza la posición del objeto sumando el desplazamiento
        transform.position += displacement;
    }
}
