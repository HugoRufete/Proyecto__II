
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
            // Mueve al enemigo en la dirección del jugador con la velocidad específica
            transform.position = Vector3.MoveTowards(transform.position, player.position, followSpeed * Time.deltaTime);
         

            // Revisa la posicion del player para cambiar de orientación según su posición
            if (transform.position.x > player.position.x)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
            if (transform.position.x < player.position.x)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
        }

    }
}
