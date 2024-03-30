using UnityEngine;

public class Shotgun : MonoBehaviour
{
    public GameObject prefabBala;
    public Transform puntoDisparo;
    public float velocidadBala = 10f;
    public float alcance = 10f; // Alcance máximo de las balas
    public int maxBullets = 10; // Cantidad máxima de balas en el cargador
    [SerializeField] public int bulletsInMagazine; // Balas restantes en el cargador

    public static event System.Action shotgunDisparada;


    public int ObtenerMunicionActual()
    {
        return bulletsInMagazine;
    }

    // Método para obtener la munición máxima
    public int ObtenerMunicionMaxima()
    {
        return maxBullets;
    }

    void Start()
    {
        bulletsInMagazine = maxBullets;
    }

    void Update()
    {
        // Dispara cuando se presiona el botón de disparo y hay balas en el cargador
        if (Input.GetButtonDown("Fire1") && bulletsInMagazine >= 2)
        {
            DisparoMultiple();
        }
    }

    void DisparoMultiple()
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

            // Aplicar el alcance máximo a las balas
            Destroy(bala, alcance / velocidadBala); // Destruye la bala después de alcanzar el alcance máximo
        }

        bulletsInMagazine -= numProyectiles;

        if (shotgunDisparada != null)
            shotgunDisparada();
    }

    public void RecargarPistola()
    {
        bulletsInMagazine = Mathf.Min(bulletsInMagazine + maxBullets, maxBullets); // Asegurar que no exceda el máximo de balas
    }

    // Método para cargar balas guardadas
    public void CargarBalas(int cantidad)
    {
        bulletsInMagazine = Mathf.Clamp(cantidad, 0, maxBullets); // Asegurar que la cantidad no exceda el máximo de balas
    }
}
