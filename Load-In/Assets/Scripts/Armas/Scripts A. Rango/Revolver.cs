using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;
using UnityEngine.UI;

public class Revolver : MonoBehaviour
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

    public static event System.Action revolverDisparado;

    private WeaponParent weaponParent;

    private Revolver revolver;
    
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
        balasRestantes = PlayerPrefs.GetInt("BalasRevolver", maxBalas);
        EnemyHealth.enemyDeadEvent += RecargarBalasRevolver;
        weaponParent = GetComponent<WeaponParent>();
        revolver = GetComponent<Revolver>();
        
        if(weaponParent != null)
        {
            revolver.enabled = true;
        }
    }

    void OnDestroy()
    {
        EnemyHealth.enemyDeadEvent -= RecargarBalasRevolver;
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
            Invoke("RecargarRevolver", velocidadRecarga);
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
        PlayerPrefs.SetInt("BalasRevolver", balasRestantes);
        PlayerPrefs.Save();

        if (balasRestantes == 0)
        {
            Debug.Log("Recargar o recuperar balas");
        }

        if (revolverDisparado != null)
            revolverDisparado();
    }

    public void RecargarRevolver()
    {
        balasRestantes = maxBalas;

        // Guardar el nuevo estado de munición
        PlayerPrefs.SetInt("BalasRevolver", balasRestantes);
        PlayerPrefs.Save();
    }

    // Método para recargar las balas cuando un enemigo muere
    void RecargarBalasRevolver(int balasRecargadas)
    {
        balasRestantes += balasRecargadas;
        balasRestantes = Mathf.Min(balasRestantes, maxBalas);

        // Guardar el nuevo estado de munición
        PlayerPrefs.SetInt("BalasRevolver", balasRestantes);
        PlayerPrefs.Save();
    }
}
