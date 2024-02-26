using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public float followSpeed = 5f; // Velocidad de seguimiento
    private Transform player; // Referencia al transform del jugador

    void Start()
    {
        // Encuentra al jugador al comienzo
        player = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        // Verifica que se haya encontrado al jugador
        if (player != null)
        {
            // Calcula la dirección hacia el jugador
            Vector3 direction = (player.position - transform.position).normalized;

            // Mueve al enemigo en la dirección del jugador con la velocidad específica
            transform.Translate(direction * followSpeed * Time.deltaTime);
        }
    }
}
