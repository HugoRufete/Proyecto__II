using UnityEngine;

public class Shotgun : MonoBehaviour
{
    private NumMun numMun;
    private Transform player;
    public GameObject prefabBala;
    public Transform puntoDisparo;
    public float velocidadBala = 10f;
    public float alcance = 10f; // Alcance m�ximo de las balas
    public int maxBullets = 10; // Cantidad m�xima de balas en el cargador
    [SerializeField] public int bulletsInMagazine; // Balas restantes en el cargador
    private Rigidbody2D rb;

    public static event System.Action shotgunDisparada;

    public float velocidadRecarga = 3f;

    public float recoil = 10;

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
        numMun = GameObject.Find("HUD_Munici�n").GetComponent<NumMun>();
        bulletsInMagazine = maxBullets;
        rb = GameObject.Find("Player").GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player").GetComponent<Transform>();
    }

    void Update()
    {
        // Dispara cuando se presiona el bot�n de disparo y hay balas en el cargador
        if (Input.GetButtonDown("Fire1") && bulletsInMagazine >= 2)
        {
            DisparoMultiple();
        }
        if (Input.GetKeyDown(KeyCode.R) && numMun.NumCajas > 0)
        {
            Invoke("RecargarShotgun", velocidadRecarga);
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
            GameObject bala = Instantiate(prefabBala, puntoDisparo.position, transform.rotation);

            CameraMovement.Instance.MoverCamara(4, 5, 0.3f);

            Retroceso();

            Rigidbody2D rbBala = bala.GetComponent<Rigidbody2D>();
            rbBala.velocity = direccionBala * velocidadBala;

            // Aplicar el alcance m�ximo a las balas
            Destroy(bala, alcance / velocidadBala); // Destruye la bala despu�s de alcanzar el alcance m�ximo
        }

        bulletsInMagazine -= numProyectiles;

        if (shotgunDisparada != null)
            shotgunDisparada();
    }

  
    public void RecargarShotgun()
    {
        bulletsInMagazine = Mathf.Min(bulletsInMagazine + maxBullets, maxBullets); 

        numMun.NumCajas--;
    }

    private void Retroceso()
    {
        // Obtiene la posici�n del rat�n en el mundo
        Vector3 posicionRaton = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        posicionRaton.z = 0; // Ajusta la posici�n Z a 0 para que est� en el mismo plano que el jugador

        Vector3 direction = posicionRaton - player.transform.position;
        rb.AddForce(-direction * recoil, ForceMode2D.Force);



    }
}
