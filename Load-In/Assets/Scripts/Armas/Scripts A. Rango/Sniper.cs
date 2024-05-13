using UnityEngine;
using UnityEngine.UI;

public class Sniper : MonoBehaviour, IRecargable
{
    private Transform player;
    public GameObject prefabBala;
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

    public float recoil = 10;

    public int ObtenerMunicionActual()
    {
        return balasRestantes;
    }

    // M�todo para obtener la munici�n m�xima
    public int ObtenerMunicionMaxima()
    {
        return maxBalas;
    }
    void Start()
    {
        // Cargar el estado de munici�n almacenado
        balasRestantes = PlayerPrefs.GetInt("BalasSniper", maxBalas);
        rb = GameObject.Find("Player").GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player").GetComponent<Transform>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && balasRestantes > 0)
        {
            Disparar();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("Recargando...");
            Invoke("RecargarSniper", velocidadRecarga);
        }
    }

    void Disparar()
    {
        GameObject bala = Instantiate(prefabBala, puntoDisparo.position, transform.rotation);

        Rigidbody2D rbBala = bala.GetComponent<Rigidbody2D>();

        Vector2 direccionBala = puntoDisparo.right;

        rbBala.velocity = direccionBala * velocidadBala;

        // Destruye la bala despu�s de alcanzar el alcance m�ximo
        Destroy(bala, alcance / velocidadBala);

        balasRestantes--;

        // Guardar el nuevo estado de munici�n
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

    public void RecargarArma()
    {
        RecargarSniper();
    }
    public void RecargarSniper()
    {
        balasRestantes = maxBalas;

        // Guardar el nuevo estado de munici�n
        PlayerPrefs.SetInt("BalasSniper", balasRestantes);
        PlayerPrefs.Save();
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
