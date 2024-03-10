using UnityEngine;
using UnityEngine.UI;

public class Pistol : MonoBehaviour
{
    public GameObject prefabBala;
    public Transform puntoDisparo;
    public float velocidadBala = 10f;

    [Header("Cargador")]
    public int maxBalas = 10;
    public int balasRestantes;
    public Text balasRestantesText;

    [Header("ALcance")]
    public float alcance = 10f;

    void Start()
    {
        balasRestantes = maxBalas;
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
            Invoke("RecargarPistola", 2f);
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

        if (balasRestantesText != null)
        {
            balasRestantesText.text = "Balas restantes: " + balasRestantes.ToString();
        }

        if (balasRestantes == 0)
        {
            Debug.Log("Recargar o recuperar balas");
        }
    }

    public void RecargarPistola()
    {
        balasRestantes = balasRestantes + 10;
    }
}
