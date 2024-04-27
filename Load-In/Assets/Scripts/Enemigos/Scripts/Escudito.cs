using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Escudito : MonoBehaviour
{
    public Collider2D attackCollider; //Collider de ataque
    public Collider2D standarCollider; //Colllider normal
    public Transform Player; //Ubicaci�n del jugador
    public float velocidadMovimiento = 5.0f; //Velocidad movimeinto enemigo
    public float distanciaDeseada = 1.0f; //Distancia minima que habr� entre el jugador y el enemigo para evitar problemas
    public float AttackCooldown = 1.0f; //Cooldown de ataque
    float lastTimeAttack = 0.0f; //Ultima vez que ha disparado
    Animator myanimator;//Referencia a nuestro animator
    bool mirarDerecha = true; //Booleana para controlar a que lado mira el jugador  
    private EnemyHealth enemHealth;
    bool protegiendo = false;
    private bool isAnimating;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player").transform;
        myanimator = GetComponent<Animator>();
        isAnimating = false;
        attackCollider.enabled = false; //Al empezar el collider del enemigo siempre estar� desactivado
        enemHealth = GetComponent<EnemyHealth>();

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = Player.position - transform.position; //La diferencia de posici�n entre el jugador y el enemigo

        if (direction.magnitude > distanciaDeseada) //Si la longutud entre el enemigo y el jugador es mayor que la distancia deseada:
        {
            direction.Normalize(); //Normalizamos el vector

            
            // multiplicando por la diferencia entre la posici�n menos la distanciaDeseada (distancia m�nima entre ellos)

            transform.Translate(direction * velocidadMovimiento * Time.deltaTime); //Trasladar al enemigo a la direcci�n a la posici�n del jugador multiplicando por su velocidad 
            //Time.DeltaTime(para que vaya igual en todos los ordenadores

            myanimator.SetBool("IsWalking", true); //Si la condici�n de arriba es verdadera activamos la animaci�nd de andar
            attackCollider.enabled = false;

            if (Time.time >= lastTimeAttack + AttackCooldown) //Si ya ha pasado el cooldwon llamamos al metodo attack
            {

                Attack();
            }
        }

        else
        {
            myanimator.SetBool("IsWalking", false); //Si no lo es la desactivamos
        }

        if (transform.position.x > Player.position.x && mirarDerecha) //Si la posici�on del enemigo es mayor en el eje x que la del jugador y 
            //la booleana es verdadera llamamos al m�todo voltear(flipear al jugador)
        {
            // Voltear al enemigo
            Voltear();
        }
        else if (transform.position.x < Player.position.x && !mirarDerecha) //Si la posici�on del enemigo es menor en el eje x que la del jugador y 
            //la booleana es falsa llamamos al m�todo voltear(flipear al jugador)
        {
            // Voltear al enemigo
            Voltear();
        }

        if (enemHealth != null && enemHealth.health < 20 && !protegiendo)
        {
            Debug.Log("Empieza la corrutina");
            enemHealth.ActivateInvulnerability(5f);
            StartCoroutine(PlayAnimationForDuration("Escudito_Protection", 5f));
        }

        
        
    }

    void Voltear()
    {
        // Invertir la escala en el eje X para voltear al enemigo
        Vector3 nuevaEscala = transform.localScale;
        nuevaEscala.x *= -1;
        transform.localScale = nuevaEscala;

        // Cambiar la direcci�n del enemigo
        mirarDerecha = !mirarDerecha;
    }

    void Attack()
    {
        myanimator.SetBool("IsAttacking", true); 

      
        lastTimeAttack = Time.time; 
    }

    public void EnableCollider()
    {
        attackCollider.enabled = true; //Activamos el collider a principios del ataque llamando a este m�todo mediante un evento en la animaci�n
        
    }

    public void DisableCollider()
    {
        attackCollider.enabled = false; 
    }

    IEnumerator PlayAnimationForDuration(string animationName, float duration)
    {
        isAnimating = true;
        myanimator.Play(animationName);

        yield return new WaitForSeconds(duration); 

        myanimator.StopPlayback();
        isAnimating = false;
    }
}
