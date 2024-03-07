using UnityEngine;

public class Shotgun : MonoBehaviour
{
    public GameObject prefabBala;
    public Transform puntoDisparo;
    public float velocidadBala = 10f;

    void Update()
    {
        // Dispara cuando se presiona el botón de disparo
        if (Input.GetButtonDown("Fire1"))
        {
            DisparoMultiple();
        }
    }

    void DisparoMultiple ()
    {
        // Número de proyectiles a disparar
        int numProyectiles = 3;

        // Ángulo inicial para el primer proyectil
        float anguloInicial = -30f;

        // Ángulo entre los proyectiles
        float anguloEntreProyectiles = 30f;

        for (int i = 0; i < numProyectiles; i++)
        {
            // Calcular el ángulo para el proyectil actual
            float anguloActual = anguloInicial + i * anguloEntreProyectiles;

            // Calcular la dirección del proyectil actual
            Vector2 direccionBala = Quaternion.Euler(0f, 0f, anguloActual) * puntoDisparo.right;

            // Instanciar la bala en la posición del punto de disparo con la rotación correspondiente
            GameObject bala = Instantiate(prefabBala, puntoDisparo.position, Quaternion.Euler(0f, 0f, anguloActual));

            Rigidbody2D rbBala = bala.GetComponent<Rigidbody2D>();
            rbBala.velocity = direccionBala * velocidadBala;
        }
    }
}
