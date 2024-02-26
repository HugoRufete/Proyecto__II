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
            // Calcula la direcci�n hacia el jugador
            Vector3 direction = (player.position - transform.position).normalized;

            // Mueve al enemigo en la direcci�n del jugador con la velocidad espec�fica
            transform.Translate(direction * followSpeed * Time.deltaTime);
        }
    }
}
