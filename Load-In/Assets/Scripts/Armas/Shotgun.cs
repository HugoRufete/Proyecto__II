using UnityEngine;

public class Shotgun : MonoBehaviour
{
    public GameObject prefabBala;
    public Transform puntoDisparo;
    public float velocidadBala = 10f;

    void Update()
    {
        // Dispara cuando se presiona el bot�n de disparo
        if (Input.GetButtonDown("Fire1"))
        {
            DisparoMultiple();
        }
    }

    void DisparoMultiple ()
    {
        // N�mero de proyectiles a disparar
        int numProyectiles = 3;

        // �ngulo inicial para el primer proyectil
        float anguloInicial = -30f;

        // �ngulo entre los proyectiles
        float anguloEntreProyectiles = 30f;

        for (int i = 0; i < numProyectiles; i++)
        {
            // Calcular el �ngulo para el proyectil actual
            float anguloActual = anguloInicial + i * anguloEntreProyectiles;

            // Calcular la direcci�n del proyectil actual
            Vector2 direccionBala = Quaternion.Euler(0f, 0f, anguloActual) * puntoDisparo.right;

            // Instanciar la bala en la posici�n del punto de disparo con la rotaci�n correspondiente
            GameObject bala = Instantiate(prefabBala, puntoDisparo.position, Quaternion.Euler(0f, 0f, anguloActual));

            Rigidbody2D rbBala = bala.GetComponent<Rigidbody2D>();
            rbBala.velocity = direccionBala * velocidadBala;
        }
    }
}
