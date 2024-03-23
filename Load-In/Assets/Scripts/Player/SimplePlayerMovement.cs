using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePlayerMovement : MonoBehaviour
{
    //Variables de movimiento
    public float movSpeed;
    float SpeedX, SpeedY;

    public float dashSpeed;
    public float dashDuration;
    public float dashCooldown; // Nuevo: Tiempo de reutilizaci�n del dash
    private bool isDashing = false;
    private bool dashCooldownActive = false; // Nuevo: Bandera para el cooldown del dash
    private float dashEndTime;
    private float lastDashTime = -999f; // Nuevo: Mantener registro del �ltimo tiempo de dash

    //Hacia donde va a mirar el enemigo
    private Vector3 objective;
    [SerializeField] private new Camera camera;

    private SpriteRenderer spriteRenderer; // Referencia al SpriteRenderer
    Animator anim;
    Rigidbody2D rb;
    private Collider2D playerCollider; // Referencia al Collider2D

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>(); // Obtener la referencia al SpriteRenderer
        playerCollider = GetComponent<Collider2D>();
    }

    void Update()
    {
        if (!isDashing && !dashCooldownActive) // Nuevo: Verificar que no est� en dash ni en cooldown
        {
            Movement();
        }
        UpdateLayer(); // Llamar a la funci�n para actualizar la capa del sprite

        if (Input.GetKeyDown(KeyCode.C) && !isDashing && !dashCooldownActive) // Nuevo: Agregar verificaci�n de cooldown
        {
            Dash();
        }

        // Nuevo: Verificar si el cooldown ha terminado
        if (dashCooldownActive && Time.time >= lastDashTime + dashCooldown)
        {
            dashCooldownActive = false;
        }
    }

    //Movimiento simple de enemigo + rotacion de este a traves del cursor
    void Movement()
    {
        //Cogemos las inputs y las multiplicamos por la velocidad
        SpeedX = Input.GetAxisRaw("Horizontal") * movSpeed;
        SpeedY = Input.GetAxisRaw("Vertical") * movSpeed;

        //Le aplicamos la velocidad al RB
        rb.velocity = new Vector2(SpeedX, SpeedY);
        if (SpeedX != 0 || SpeedY != 0)
        {
            anim.Play("Walk");
        }
        else
        {
            anim.Play("Idle");
        }

        //Indicamos donde se encuentra el objetivo al que queremos mirar
        objective = camera.ScreenToWorldPoint(Input.mousePosition);
        //Cambiamos la rotacion del enemigo dependiendo de donde esta el objetivo
        if (objective.x < transform.position.x)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        if (objective.x > transform.position.x)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }

    // Funci�n para actualizar la capa del sprite
    void UpdateLayer()
    {
        Vector3 directionToMouse = (objective - transform.position).normalized;

        if (directionToMouse.y > 0) // Si el rat�n est� por encima del enemigo
        {
            spriteRenderer.sortingOrder = 3; // Cambiar la capa a 3 (delante)
        }
        else // Si el rat�n est� por debajo del enemigo
        {
            spriteRenderer.sortingOrder = 1; // Cambiar la capa a 1 (detr�s)
        }
    }

    void Dash()
    {
        isDashing = true;
        dashEndTime = Time.time + dashDuration;

        // Desactivar el Collider durante el Dash
        playerCollider.enabled = false;

        Vector2 dashDirection = new Vector2(SpeedX, SpeedY).normalized;
        rb.AddForce(dashDirection * dashSpeed, ForceMode2D.Impulse);

        // Nuevo: Actualizar variables de cooldown
        dashCooldownActive = true;
        lastDashTime = Time.time;
    }

    // M�todo para controlar el final del Dash
    void LateUpdate()
    {
        if (isDashing && Time.time >= dashEndTime)
        {
            isDashing = false;
            rb.velocity = Vector2.zero;

            playerCollider.enabled = true;
        }
    }
}
