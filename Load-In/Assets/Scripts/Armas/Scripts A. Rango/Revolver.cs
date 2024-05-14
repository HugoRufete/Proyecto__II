using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;
using UnityEngine.UI;

public class Revolver : MonoBehaviour
{
    private NumMun numMun;
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

    public static event System.Action revolverDisparado;

    private WeaponParent weaponParent;

    private Revolver revolver;

    public float recoil = 10;

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
        numMun = GameObject.Find("HUD_Munición").GetComponent<NumMun>();
        balasRestantes = PlayerPrefs.GetInt("BalasRevolver", maxBalas);
        weaponParent = GetComponent<WeaponParent>();
        revolver = GetComponent<Revolver>();
        rb = GameObject.Find("Player").GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player").GetComponent<Transform>();

        if (weaponParent != null)
        {
            revolver.enabled = true;
        }
    }



    void Update()
    {
        if (Input.GetButtonDown("Fire1") && balasRestantes > 0)
        {
            Disparar();
        }

        if (Input.GetKeyDown(KeyCode.R) && numMun.NumCajas > 0)
        {
            Invoke("RecargarRevolver", velocidadRecarga);
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

        balasRestantes--;

        CameraMovement.Instance.MoverCamara(3, 3, 0.2f);
        Retroceso();

        // Guardar el nuevo estado de munición
        PlayerPrefs.SetInt("BalasRevolver", balasRestantes);
        PlayerPrefs.Save();

        if (balasRestantes == 0)
        {
            Debug.Log("Recargar o recuperar balas");
        }

        if (revolverDisparado != null)
            revolverDisparado();
    }


    public void RecargarArma()
    {
        RecargarRevolver();
    }
    public void RecargarRevolver()
    {
        balasRestantes = maxBalas;

        numMun.NumCajas--;
        // Guardar el nuevo estado de munición
        PlayerPrefs.SetInt("BalasRevolver", balasRestantes);
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
