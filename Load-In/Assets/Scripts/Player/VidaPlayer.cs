using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VidaPlayer : MonoBehaviour
{
    public float maxVida;
    public float vida;
    public Image BarraDeVida;
    private bool reducedDamageActivated = false; 
    private float reducedDamageMultiplier = 0.5f; // Multiplicador de reducción de daño
    public ParticleSystem bloodPlayer;
    public int Curación;
    private Rigidbody2D rb;
    

    Animator animator;
    public GameObject bloodIndicator15;
    public GameObject bloodIndicator50;
    public GameObject bloodIndicator75;

    public GameObject imagenReducedDamageActivado;

    public GameObject tiendaUI;

    public Weapon_Wheel_Manager weaponManager;

    public GameObject curación_Lata_popUp;

    public GameObject [] curacionesHUD;

    [SerializeField] private int cantidadCurasMáxima = 0;

    private void Start()
    {

        
        animator = GetComponent<Animator>();
        GameObject weaponWheelObject = GameObject.Find("UI / HUD");
        rb = GetComponent<Rigidbody2D>();

        if (weaponWheelObject != null)
        {
            weaponManager = weaponWheelObject.GetComponent<Weapon_Wheel_Manager>();
        }
        ReiniciarVida();
        reducedDamageActivated = false;
    }

    private void Update()
    {
        BarraDeVida.fillAmount = vida / maxVida;

        if (Input.GetKeyDown(KeyCode.F) && cantidadCurasMáxima >= 1 && cantidadCurasMáxima <= 5 && vida < maxVida)
        {
            CurarJugador();
            animator.SetBool("curación", true);
        }
        else if (vida <= 100 && Input.GetKeyDown(KeyCode.F) && cantidadCurasMáxima >= 1)
        {
            Debug.Log("Vida al máximo");
        }
        else if (cantidadCurasMáxima == 0 && Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("No tienes curas");
        }

        if (cantidadCurasMáxima >= 1)
        {
            Debug.Log("1 curaciones");
            curacionesHUD[0].SetActive(true);
            if(cantidadCurasMáxima >= 2)
            {
                Debug.Log("2 curaciones");
                curacionesHUD[1].SetActive(true);
                if (cantidadCurasMáxima >= 3)
                {
                    Debug.Log("3 curaciones");
                    curacionesHUD[2].SetActive(true);
                    if (cantidadCurasMáxima >= 4)
                    {
                        Debug.Log("4 curaciones");
                        curacionesHUD[3].SetActive(true);
                        if (cantidadCurasMáxima >= 5)
                        {
                            Debug.Log("5 curaciones");
                            curacionesHUD[4].SetActive(true);
                        }
                        else if(cantidadCurasMáxima < 5)
                        {
                            curacionesHUD[4].SetActive(false);
                        }
                        if (cantidadCurasMáxima > 5)
                        {
                            Debug.Log("Cantidad Máxima de curas alcanzada");
                        }
                    }
                    else if (cantidadCurasMáxima < 4)
                    {
                        curacionesHUD[3].SetActive(false);
                    }
                }
                else if (cantidadCurasMáxima < 3)
                {
                    curacionesHUD[2].SetActive(false);
                }


            }
            else if (cantidadCurasMáxima < 2)
            {
                curacionesHUD[1].SetActive(false);
            }
        }
        else if (cantidadCurasMáxima < 1)
        {
            curacionesHUD[0].SetActive(false);
        }

        if (vida >= 100)
        {
            bloodIndicator75.SetActive(false);
            bloodIndicator50.SetActive(false);
            bloodIndicator15.SetActive(false);
        }
        if (vida >= 50&& vida < 75)
        {
            bloodIndicator75.SetActive(true);
            bloodIndicator50.SetActive(false);
            bloodIndicator15.SetActive(false);
        }
        else if (vida >= 25 && vida < 50)
        {

             bloodIndicator75.SetActive(false);
             bloodIndicator50.SetActive(true);
             bloodIndicator15.SetActive(false);
    
        }
        else if (vida < 15)
        {
            bloodIndicator75.SetActive(false);
            bloodIndicator50.SetActive(false);
            bloodIndicator15.SetActive(true);
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Lata"))
        {
            if (cantidadCurasMáxima < 5)
            {
                cantidadCurasMáxima += 1;
            }

            collision.gameObject.SetActive(false);
        }
        else if (collision.CompareTag("Cecina"))
        {
            vida = vida + 20;
        }
        else if (collision.CompareTag("Seta"))
        {
            vida = vida + 10;
        }

        else if (collision.CompareTag("HachaPickable"))
        {
            Debug.Log("Desbloqueando Hacha");
            weaponManager.DesbloquearHacha();
            collision.gameObject.SetActive(false);
        }

        else if (collision.CompareTag("RevolverPickable"))
        {
            weaponManager.DesbloquearRevolver();
            collision.gameObject.SetActive(false);
        }


    }

    public void ReiniciarVida()
    {
        //vida = maxVida;
    }

    // Lógica de vida
    public void PlayerTakeDamage(int damage)
    {
        
        if (reducedDamageActivated == true) 
        {
            Debug.Log("---");
            damage = Mathf.RoundToInt(damage * reducedDamageMultiplier); 
        }

        bloodPlayer.Play();
        vida -= damage;
        CameraMovement.Instance.MoverCamara(7, 4, 0.3f);


        if (vida <= 0)
        {
            rb.velocity = Vector3.zero;
            animator.SetBool("isDead",true);  
        }
    }
 
    

    public void GameOver()
    {
        Destroy(gameObject);
        SceneManager.LoadScene("Game_Over");
    }
    public void ActivateReducedDamage()
    {
        imagenReducedDamageActivado.SetActive(true);
        reducedDamageActivated = true;
        Debug.Log("Reduced damage activated: " + reducedDamageActivated);
        reducedDamageMultiplier = 0.9f; // Reduce el daño recibido en un 10%
        Time.timeScale = 1.0f;
    }

    public void CurarJugador()
    {
        curación_Lata_popUp.SetActive(true);
        StartCoroutine(DestroyObjectCoroutine(curación_Lata_popUp, 2f));
        Debug.Log("Curando Jugador");
        vida = vida + Curación;
        cantidadCurasMáxima -= 1;
    }

    private IEnumerator DestroyObjectCoroutine(GameObject objetoADestruir, float delayInSeconds)
    {
        yield return new WaitForSeconds(delayInSeconds);

        if (objetoADestruir != null)
        {
            objetoADestruir.SetActive(false);
        }
    }

   

}
