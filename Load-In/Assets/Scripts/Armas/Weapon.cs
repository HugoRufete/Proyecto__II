using UnityEngine;
using UnityEngine.UI; // Importa el m�dulo UI para utilizar Text

public class Weapon : MonoBehaviour
{
    public GameObject prefabBala;
    public Transform puntoDisparo;
    public float velocidadBala = 10f;
    public int maxBalas = 10; // N�mero m�ximo de balas
    public int balasRestantes; // N�mero actual de balas
    public Text balasRestantesText; // Referencia al Text para mostrar el n�mero de balas

    void Start()
    {
        balasRestantes = maxBalas;

        // Aseg�rate de asignar el objeto Text desde el inspector de Unity
        // balasRestantesText = GetComponent<Text>();
        // O bien, puedes arrastrar y soltar el objeto Text desde el inspector de Unity
    }

    void Update()
    {
        // Dispara cuando se presiona el bot�n de disparo y hay balas disponibles
        if (Input.GetButtonDown("Fire1") && balasRestantes > 0)
        {
            Disparar();
        }
    }

    void Disparar()
    {
        // Instancia la bala en la posici�n del punto de disparo con una rotaci�n de 90 grados en Z
        GameObject bala = Instantiate(prefabBala, puntoDisparo.position, Quaternion.Euler(0f, 0f, 90f));

        Rigidbody2D rbBala = bala.GetComponent<Rigidbody2D>();

        Vector2 direccionBala = puntoDisparo.right;

        rbBala.velocity = direccionBala * velocidadBala;

        // Reduce el n�mero de balas restantes
        balasRestantes--;

        // Actualiza el Text para mostrar el n�mero de balas restantes
        if (balasRestantesText != null)
        {
            balasRestantesText.text = "Balas restantes: " + balasRestantes.ToString();
        }

        // Verifica si se alcanz� el l�mite de balas y realiza las acciones necesarias
        if (balasRestantes == 0)
        {
            // Aqu� puedes agregar l�gica para recargar o recuperar balas
            // Por ahora, simplemente imprimo un mensaje
            Debug.Log("Recargar o recuperar balas");
        }
    }

    public void Recargar()
    {
        balasRestantes = balasRestantes + 10;
    }
}
