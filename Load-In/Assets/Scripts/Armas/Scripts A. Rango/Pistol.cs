using UnityEngine;

public class Pistol : MonoBehaviour
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

    public static event System.Action pistolaDisparada;

    public int ObtenerMunicionActual()
    {
        return balasRestantes;
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
        balasRestantes = PlayerPrefs.GetInt("BalasPistola", maxBalas);
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
            Invoke("RecargarPistola", velocidadRecarga);
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
        PlayerPrefs.SetInt("BalasPistola", balasRestantes);
        PlayerPrefs.Save();

        if (balasRestantes == 0)
        {
            Debug.Log("Recargar o recuperar balas");
        }

        if (pistolaDisparada != null)
            pistolaDisparada();
    }

    public void RecargarPistola()
    {
        balasRestantes = maxBalas;

        // Guardar el nuevo estado de munición
        PlayerPrefs.SetInt("BalasPistola", balasRestantes);
        PlayerPrefs.Save();
    }
}