using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    public GameObject prefabBala;
    public Transform puntoDisparo;
    public float velocidadBala = 10f;
    public int maxBalas = 10;
    public int balasRestantes;
    public Text balasRestantesText;

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
    }

    void Disparar()
    {
        GameObject bala = Instantiate(prefabBala, puntoDisparo.position, Quaternion.Euler(0f, 0f, 90f));

        Rigidbody2D rbBala = bala.GetComponent<Rigidbody2D>();

        Vector2 direccionBala = puntoDisparo.right;

        rbBala.velocity = direccionBala * velocidadBala;

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

    public void Recargar()
    {
        balasRestantes = balasRestantes + 10;
    }
}
