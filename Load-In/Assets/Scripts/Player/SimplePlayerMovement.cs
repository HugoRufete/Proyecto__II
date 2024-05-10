using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimplePlayerMovement : MonoBehaviour
{
    // Variables de movimiento
    public float movSpeed;
    public float dashSpeed;
    public float dashDuration;
    public float dashCooldown = 1f; // Tiempo de reutilizaci�n del dash
    private bool isDashing = false;
    private float dashEndTime;
    private float nextDashTime = 0f; // Tiempo en el que se puede usar el dash nuevamente
    public float remainingCooldown; // Tiempo restante para el pr�ximo dash

    public Image energyBar; // Referencia a la imagen Fill de la barra de energ�a

    private float SpeedX, SpeedY;

    // Hacia donde va a mirar el personaje
    private Vector3 objective;
    [SerializeField] private new Camera camera;

    private SpriteRenderer spriteRenderer; // Referencia al SpriteRenderer
    private Animator anim;
    private Rigidbody2D rb;
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
        if (!isDashing)
        {
            Movement();
        }

        UpdateLayer(); // Llamar a la funci�n para actualizar la capa del sprite

        // Actualizar el tiempo restante para el pr�ximo dash
        remainingCooldown = Mathf.Max(0, nextDashTime - Time.time);

        // Actualizar la barra de energ�a
        UpdateEnergyBar();

        if (Time.time >= nextDashTime && Input.GetKeyDown(KeyCode.LeftShift) && !isDashing)
        {
            Dash();
            
        }

       
        if (isDashing || Time.time >= nextDashTime)
        {
            Shadow.me.Sombras_Skill();
        }
            
        

        
    }

    // Movimiento simple de personaje + rotacion de este a trav�s del cursor
    void Movement()
    {
        // Cogemos las inputs y las multiplicamos por la velocidad
        SpeedX = Input.GetAxisRaw("Horizontal") * movSpeed;
        SpeedY = Input.GetAxisRaw("Vertical") * movSpeed;

        // Le aplicamos la velocidad al RB
        rb.velocity = new Vector2(SpeedX, SpeedY);

        // Indicamos donde se encuentra el objetivo al que queremos mirar
        objective = camera.ScreenToWorldPoint(Input.mousePosition);
        // Cambiamos la rotaci�n del personaje dependiendo de donde esta el objetivo
        if (objective.x < transform.position.x)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        if (objective.x > transform.position.x)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }

        // Control de la animaci�n de movimiento
        if (!isDashing)
        {
            if (SpeedX != 0 || SpeedY != 0)
            {
                if (SpeedX > 0 && objective.x < transform.position.x)
                {
                    anim.Play("Back_Walk");
                }
                if (SpeedX > 0 && objective.x > transform.position.x)
                {
                    anim.Play("Walk");
                }
                if (SpeedX < 0 && objective.x > transform.position.x)
                {
                    anim.Play("Back_Walk");
                }
                if (SpeedX < 0 && objective.x < transform.position.x)
                {
                    anim.Play("Walk");
                }
                if (SpeedY != 0 && SpeedX == 0)
                {
                    anim.Play("Walk");
                }
            }
            else
            {
                anim.Play("Idle");
            }
        }
    }

    // Funci�n para actualizar la capa del sprite
    void UpdateLayer()
    {
        Vector3 directionToMouse = (objective - transform.position).normalized;

        if (directionToMouse.y > 0) // Si el rat�n est� por encima del jugador
        {
            spriteRenderer.sortingOrder = 3; // Cambiar la capa a 3 (delante)
        }
        else // Si el rat�n est� por debajo del jugador
        {
            spriteRenderer.sortingOrder = 1; // Cambiar la capa a 1 (detr�s)
        }
    }

    // Funci�n para el Dash
    void Dash()
    {

        isDashing = true;
       
        dashEndTime = Time.time + dashDuration;

        // Desactivar el Collider durante el Dash
       

        Vector2 dashDirection;

        if (SpeedX != 0 || SpeedY != 0)
        {
            
            dashDirection = new Vector2(SpeedX, SpeedY).normalized;
        }

        else
        {
            
            if (transform.eulerAngles.y == 0)
            {
                dashDirection = Vector2.right;
            }
            else
            {
                dashDirection = Vector2.left;
            }
        }

        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("EnemyLayer"), true);
        // Aplicar la fuerza del dash en la direcci�n determinada
        rb.AddForce(dashDirection * dashSpeed, ForceMode2D.Impulse);

        // Establecer la orientaci�n del sprite seg�n la direcci�n del dash
        if (dashDirection.x > 0)
        {

            transform.eulerAngles = new Vector3(0, 0, 0); // Mirando a la derecha
        }
        else if (dashDirection.x < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0); // Mirando a la izquierda
        }

        // Establecer el tiempo para el pr�ximo dash
        nextDashTime = Time.time + dashCooldown;

        // Reproducir la animaci�n de dash
        anim.Play("Dash");

        // Actualizar la barra de energ�a
        UpdateEnergyBar();
    }

    // M�todo para controlar el final del Dash
    void LateUpdate()
    {
        if (isDashing && Time.time >= dashEndTime)
        {
            isDashing = false;
            rb.velocity = Vector2.zero;
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("EnemyLayer"), false);
            
        }
    }

    // Actualiza la barra de energ�a basada en remainingCooldown
    void UpdateEnergyBar()
    {
        if (energyBar != null)
        {
            // Ajustar FillAmount basado en remainingCooldown
            energyBar.fillAmount = 1 - (remainingCooldown / dashCooldown);
        }

    }
}
