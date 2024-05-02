using UnityEngine;
using UnityEngine.UI;

public class Sniper : MonoBehaviour, IRecargable
{
    public GameObject prefabBala;
    public Transform puntoDisparo;
    public float velocidadBala = 10f;

    [Header("Cargador")]
    public int maxBalas = 10;
    public int balasRestantes;

    [Header("Alcance")]
    public float alcance = 10f;

    [Header("Velocidad de Recarga")]
    public float velocidadRecarga = 2f;

    public static event System.Action sniperDisparado;

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
        balasRestantes = PlayerPrefs.GetInt("BalasSniper", maxBalas);
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
        GameObject bala = Instantiate(prefabBala, puntoDisparo.position, Quaternion.Euler(0f, 0f, 90f));

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
    }

    public void RecargarArma()
    {
        RecargarSniper();
    }
    public void RecargarSniper()
    {
        balasRestantes = maxBalas;

        // Guardar el nuevo estado de munición
        PlayerPrefs.SetInt("BalasSniper", balasRestantes);
        PlayerPrefs.Save();
    }
}
