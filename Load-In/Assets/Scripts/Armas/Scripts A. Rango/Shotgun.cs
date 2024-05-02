using UnityEngine;

public class Shotgun : MonoBehaviour, IRecargable
{
    public GameObject prefabBala;
    public Transform puntoDisparo;
    public float velocidadBala = 10f;
    public float alcance = 10f; // Alcance m�ximo de las balas
    public int maxBullets = 10; // Cantidad m�xima de balas en el cargador
    [SerializeField] public int bulletsInMagazine; // Balas restantes en el cargador

    public static event System.Action shotgunDisparada;


    public int ObtenerMunicionActual()
    {
        return bulletsInMagazine;
    }

    // M�todo para obtener la munici�n m�xima
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
        // Dispara cuando se presiona el bot�n de disparo y hay balas en el cargador
        if (Input.GetButtonDown("Fire1") && bulletsInMagazine >= 2)
        {
            DisparoMultiple();
        }
    }

    void DisparoMultiple()
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

            // Aplicar el alcance m�ximo a las balas
            Destroy(bala, alcance / velocidadBala); // Destruye la bala despu�s de alcanzar el alcance m�ximo
        }

        bulletsInMagazine -= numProyectiles;

        if (shotgunDisparada != null)
            shotgunDisparada();
    }

    public void RecargarArma()
    {
        RecargarShotgun();
    }
    public void RecargarShotgun()
    {
        bulletsInMagazine = Mathf.Min(bulletsInMagazine + maxBullets, maxBullets); 
    }

    // M�todo para cargar balas guardadas
    public void CargarBalasEscopeta(int cantidad)
    {
        bulletsInMagazine = Mathf.Clamp(cantidad, 0, maxBullets); 
    }
}
