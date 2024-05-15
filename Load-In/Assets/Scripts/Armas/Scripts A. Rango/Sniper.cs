using UnityEngine;
using UnityEngine.UI;

public class Sniper : MonoBehaviour
{
    private NumMun numMun;
    private Transform player;
    public GameObject prefabBala;
    private GameObject AnimacionRecarga;
    public Transform puntoDisparo;
    public float velocidadBala = 10f;
    private Rigidbody2D rb;

    [Header("Cargador")]
    public int maxBalas = 10;
    public int balasRestantes;

    [Header("Alcance")]
    public float alcance = 10f;

    [Header("Velocidad de Recarga")]
    public float velocidadRecarga = 2f;

    public static event System.Action sniperDisparado;

    public float recoil = 20;

    public AudioClip sniper;

    public int ObtenerMunicionActual()
    {
        return balasRestantes;
    }

    // Método para obtener la munición máxima
    public int ObtenerMunicionMaxima()
    {
        return maxBalas;
    }
    void Start()
    {

        // Cargar el estado de munición almacenado
        AnimacionRecarga = GameObject.Find("Reloading_Image");
        balasRestantes = PlayerPrefs.GetInt("BalasSniper", maxBalas);
        rb = GameObject.Find("Player").GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player").GetComponent<Transform>();
        numMun = GameObject.Find("HUD_Munición").GetComponent<NumMun>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && balasRestantes > 0)
        {
            Disparar();
        }

        if (Input.GetKeyDown(KeyCode.R) && numMun.NumCajas > 0)
        {
            Invoke("RecargarSniper", velocidadRecarga);
        }
    }

    void Disparar()
    {
        GameObject bala = Instantiate(prefabBala, puntoDisparo.position, transform.rotation);
        ControladorSonido.Instance.EjecutarSonido(sniper);

        Rigidbody2D rbBala = bala.GetComponent<Rigidbody2D>();

        Vector2 direccionBala = puntoDisparo.right;

        rbBala.velocity = direccionBala * velocidadBala;

        // Destruye la bala después de alcanzar el alcance máximo
        Destroy(bala, alcance / velocidadBala);

        balasRestantes--;

        // Guardar el nuevo estado de munición
        PlayerPrefs.SetInt("BalasSniper", balasRestantes);
        PlayerPrefs.Save();

        if (balasRestantes == 0)
        {
            Debug.Log("Recargar o recuperar balas");
        }

        if (sniperDisparado != null)
            sniperDisparado();

        CameraMovement.Instance.MoverCamara(4, 5, 0.3f);

        Retroceso();
    }

    public void RecargarSniper()
    {
        balasRestantes = maxBalas;

        numMun.NumCajas--;
        // Guardar el nuevo estado de munición
        PlayerPrefs.SetInt("BalasSniper", balasRestantes);
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
