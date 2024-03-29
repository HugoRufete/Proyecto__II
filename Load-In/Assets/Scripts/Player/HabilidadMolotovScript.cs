using UnityEngine;

public class HabilidadMolotovScript : MonoBehaviour
{
    public GameObject molotovPrefab; // Prefab del objeto arrojadizo (Molotov)
    public float maxThrowRange = 5f; // Rango máximo de lanzamiento
    public float throwForce = 10f; // Fuerza del lanzamiento
    public float explosionDelay = 2f; // Retraso antes de la explosión

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f; // Asegurarse de que el objeto esté en el plano XY

            Vector3 playerPosition = transform.position;

            Vector3 throwDirection = mousePosition - playerPosition;
            throwDirection = Vector3.ClampMagnitude(throwDirection, maxThrowRange); // Limitar la distancia del lanzamiento

            ThrowMolotov(playerPosition, throwDirection.normalized);
        }
    }

    void ThrowMolotov(Vector3 origin, Vector3 direction)
    {
        // Instanciar el objeto arrojadizo (Molotov) en la posición del jugador
        GameObject molotovInstance = Instantiate(molotovPrefab, origin, Quaternion.identity);

        // Desactivar el collider para que no afecte a los enemigos hasta que caiga
        molotovInstance.GetComponent<Collider2D>().enabled = false;

        Rigidbody2D rb = molotovInstance.GetComponent<Rigidbody2D>();
        rb.velocity = direction * throwForce;

        // Llamar al método ExplodeMolotov después del retraso especificado
        Invoke("ExplodeMolotov", explosionDelay);
    }

    void ExplodeMolotov(GameObject molotovInstance)
    {
        // Activar el collider para que pueda afectar a los enemigos
        molotovInstance.GetComponent<Collider2D>().enabled = true;

        // Hacer explotar el Molotov
        molotovInstance.GetComponent<Molotov>().Explode();
    }
}
