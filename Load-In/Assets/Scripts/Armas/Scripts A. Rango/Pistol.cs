using UnityEngine;

public interface IRecargable
{
    void RecargarArma();
}
public class Pistol : MonoBehaviour, IRecargable
{
    private Transform player;
    public GameObject prefabBala;
    public Transform puntoDisparo;
    public float velocidadBala = 10f;
    private Rigidbody2D rb;

    [Header("Cargador")]
    public int maxBalas = 20;
    public int currentAmmo;


    [Header("Alcance")]
    public float alcance = 10f;

    [Header("Velocidad de Recarga")]
    public float velocidadRecarga = 500f;

    public static event System.Action pistolaDisparada;


    public float recoil = 10;
    public int ObtenerMunicionActual()
    {
        return currentAmmo;
    }

    // Método para obtener la munición máxima
    public static int ObtenerMaxBalas()
    {
        Pistol pistolInstance = FindObjectOfType<Pistol>();
        if (pistolInstance != null)
        {
            return pistolInstance.maxBalas;
        }
        else
        {
            Debug.LogWarning("No se encontró una instancia de Pistol en la escena. Retornando valor predeterminado.");
            return 10; // Valor predeterminado en caso de que no se encuentre una instancia de Pistol
        }
    }

    void Start()
    {
        // Cargar el estado de munición almacenado
        currentAmmo = PlayerPrefs.GetInt("BalasPistola", maxBalas);
        rb = GameObject.Find("Player").GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player").GetComponent<Transform>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && currentAmmo > 0)
        {
            Disparar();
        }

    }

    void Disparar()
    {
        GameObject bala = Instantiate(prefabBala, puntoDisparo.position, transform.rotation);
        Rigidbody2D rbBala = bala.GetComponent<Rigidbody2D>();
        Vector2 direccionBala = puntoDisparo.right;
        rbBala.velocity = direccionBala * velocidadBala;

        // Destruye la bala después de alcanzar el alcance máximo
        Destroy(bala, alcance / velocidadBala);

        currentAmmo--;

        CameraMovement.Instance.MoverCamara(4, 4, 0.2f);
        // Guardar el nuevo estado de munición
        PlayerPrefs.SetInt("BalasPistola", currentAmmo);
        PlayerPrefs.Save();

        if (currentAmmo == 0)
        {
            Debug.Log("Recargar o recuperar balas");
        }

        
        if (pistolaDisparada != null)
            pistolaDisparada();
        
        Retroceso();
    }
    public void RecargarArma()
    {
        Debug.Log("Recargando Pistola");
        RecargarPistola();
    }
    public void RecargarPistola()
    {
        currentAmmo = maxBalas;
        PlayerPrefs.SetInt("BalasPistola", currentAmmo);
        PlayerPrefs.Save();
    }

    private void Retroceso()
    {
        // Obtiene la posición del ratón en el mundo
        Vector3 posicionRaton = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        posicionRaton.z = 0; // Ajusta la posición Z a 0 para que esté en el mismo plano que el jugador

        Vector3 direction = posicionRaton - player.transform.position;
        rb.AddForce(-direction * recoil, ForceMode2D.Force);



    }
}