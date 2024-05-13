using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Escudito : MonoBehaviour
{
    public Collider2D attackCollider; //Collider de ataque
    public Collider2D standarCollider; //Colllider normal
    private Transform Player; //Ubicaci�n del jugador
    public float velocidadMovimiento = 5.0f; //Velocidad movimeinto enemigo
    public float distanciaDeseada = 1.0f; //Distancia minima que habr� entre el jugador y el enemigo para evitar problemas
    public float AttackCooldown = 1.0f; //Cooldown de ataque
    float lastTimeAttack = 0.0f; //Ultima vez que ha disparado
    Animator myanimator;//Referencia a nuestro animator
    bool mirarDerecha = true; //Booleana para controlar a que lado mira el jugador  
    private EnemyHealth enemHealth; //Referencia al script de vida enemigo
    bool protegiendo = false;
    private bool isAnimating;
    bool accionRealizada; //Booleana que hace que la animaci�n de proteger se haga solo una vez y no solo en bucle
    bool isattacking = false;
  
    private AudioSource audiosource;

    public int damageAmount = 10;
    private VidaPlayer vidaPlayer;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player").transform;
        myanimator = GetComponent<Animator>();
        isAnimating = false;
        attackCollider.enabled = false; //Al empezar el collider del enemigo siempre estar� desactivado
        enemHealth = GetComponent<EnemyHealth>();
        accionRealizada = false;
        isattacking = false;
        audiosource = GetComponent<AudioSource>();  

    }

    // Update is called once per frame
    void Update()
    {
        Destroy();

        if (isattacking == true)
        {
            audiosource.Play();
        }

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
        if (enemHealth != null && enemHealth.health < 30 && enemHealth.health > 24 && !protegiendo && !accionRealizada)
        {
            Debug.Log("Empieza la corrutina");
            Invoke("WaitToProtection", 1f);
            StartCoroutine(PlayAnimationForDuration("Escudito_Protection", 5f)); //Llamamos a la corutina que contiene la animaci�n de proteger y los segundos que queremos que dure
            accionRealizada = true; //La activamos a verdadera de forma que la animaci�n solo se realizar� una vez
        }
        //Si la vida enemiga es menor de 20 y mayor de 20 y la booleana es falsa se har� la corutina que incluye la animaci�n de proteger
        if (enemHealth != null && enemHealth.health < 20 && enemHealth.health > 14 && !protegiendo && accionRealizada)
        {
            Debug.Log("Empieza la corrutina");
            Invoke("WaitToProtection", 1f);
            StartCoroutine(PlayAnimationForDuration("Escudito_Protection", 5f)); //Llamamos a la corutina que contiene la animaci�n de proteger y los segundos que queremos que dure
            accionRealizada = false; //La activamos a verdadera de forma que la animaci�n solo se realizar� una vez
        }

        if (enemHealth != null && enemHealth.health < 9 && enemHealth.health > 4 && !protegiendo && !accionRealizada)
        {


            Invoke("WaitToProtection", 1f);
            StartCoroutine(PlayAnimationForDuration("Escudito_Protection", 5f));
            accionRealizada = true;
        }

        if (isAnimating)
        {
            return;
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
        myanimator.SetBool("IsAttacking", true); //Se realiza la animaci�n de atacar
        lastTimeAttack = Time.time;
    }

    public void EnableCollider()
    {
        attackCollider.enabled = true; //Activamos el collider a principios del ataque llamando a este m�todo mediante un evento en la animaci�n
        isattacking = true;
    }

    public void DisableCollider()
    {
        attackCollider.enabled = false; //Desactivamos el collider al final del ataque llamando a este m�todo mediante un evento en la animaci�n
        isattacking = false;
    }

    

    IEnumerator PlayAnimationForDuration(string animationName, float duration) //Corutina que controla la animaci�n de proteger
    {

        isAnimating = true;
        myanimator.Play(animationName);

        yield return new WaitForSeconds(duration);

        myanimator.StopPlayback();
        isAnimating = false;

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            vidaPlayer = other.GetComponent<VidaPlayer>();
            if (vidaPlayer != null)
            {
                // Inflige da�o al jugador
                InflictDamage();
            }
        }
    }



    public void InflictDamage()
    {
        if (isattacking)
        {
            isattacking = true;
            // Inflige da�o al jugador
            vidaPlayer.PlayerTakeDamage(damageAmount);
            Debug.Log("Da�o Inflingido");
            isattacking = false;
        }

    }

    public void WaitToProtection()
    {
        enemHealth.ActivateInvulnerability(3f); //Desactivamos la posibilidad de hacer da�o al enemigo durante 5 segundos a trav�s de una corutina en el script de la vida enemiga
    }

    public void Destroy()
    {
        if (enemHealth != null && enemHealth.health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
