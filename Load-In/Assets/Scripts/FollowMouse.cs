using UnityEngine;

public class LookAtMouse : MonoBehaviour
{
    private void Update()
    {
        // Obtén la posición del ratón en el mundo
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f; // Ajusta la posición en el eje z para que sea igual al objeto

        // Calcula la dirección hacia la posición del ratón
        Vector3 direction = (mousePosition - transform.position).normalized;

        // Calcula el ángulo en radianes y conviértelo a grados
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Establece la rotación del objeto en el eje Z
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
    }
}
