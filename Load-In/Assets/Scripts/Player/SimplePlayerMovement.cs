using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePlayerMovement : MonoBehaviour
{
    //Variables de movimiento
    public float movSpeed;
    float SpeedX, SpeedY;

    //Hacia donde va a mirar el personaje
    private Vector3 objective;
    [SerializeField] private new Camera camera;

    private SpriteRenderer spriteRenderer; // Referencia al SpriteRenderer

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>(); // Obtener la referencia al SpriteRenderer
    }

    void Update()
    {
        Movement();
        UpdateLayer(); // Llamar a la función para actualizar la capa del sprite
    }

    //Movimiento simple de personaje + rotacion de este a traves del cursor
    void Movement()
    {
        //Cogemos las inputs y las multiplicamos por la velocidad
        SpeedX = Input.GetAxisRaw("Horizontal") * movSpeed;
        SpeedY = Input.GetAxisRaw("Vertical") * movSpeed;

        //Le aplicamos la velocidad al RB
        rb.velocity = new Vector2(SpeedX, SpeedY);

        //Indicamos donde se encuentra el objetivo al que queremos mirar
        objective = camera.ScreenToWorldPoint(Input.mousePosition);
        //Cambiamos la rotacion del personaje dependiendo de donde esta el objetivo
        if (objective.x < transform.position.x)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        if (objective.x > transform.position.x)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }

    // Función para actualizar la capa del sprite
    void UpdateLayer()
    {
        Vector3 directionToMouse = (objective - transform.position).normalized;

        if (directionToMouse.y > 0) // Si el ratón está por encima del jugador
        {
            spriteRenderer.sortingOrder = 3; // Cambiar la capa a 3 (delante)
        }
        else // Si el ratón está por debajo del jugador
        {
            spriteRenderer.sortingOrder = 1; // Cambiar la capa a 1 (detrás)
        }
    }
}
