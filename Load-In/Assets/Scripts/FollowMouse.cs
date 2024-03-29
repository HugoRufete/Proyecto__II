using UnityEngine;

public class LookAtMouse : MonoBehaviour
{
    private void Update()
    {
        // Obt�n la posici�n del rat�n en el mundo
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f; // Ajusta la posici�n en el eje z para que sea igual al objeto

        // Calcula la direcci�n hacia la posici�n del rat�n
        Vector3 direction = (mousePosition - transform.position).normalized;

        // Calcula el �ngulo en radianes y convi�rtelo a grados
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Establece la rotaci�n del objeto en el eje Z
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
    }
}
