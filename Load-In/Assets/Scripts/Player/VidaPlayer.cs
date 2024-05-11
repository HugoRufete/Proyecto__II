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
    

    Animator animator;

    public GameObject imagenReducedDamageActivado;

    public GameObject tiendaUI;

    public Weapon_Wheel_Manager weaponManager;

    public GameObject [] curacionesHUD;

    [SerializeField] private int cantidadCurasMáxima = 0;

    private void Start()
    {
        
        animator = GetComponent<Animator>();
        GameObject weaponWheelObject = GameObject.Find("UI / HUD");

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
                        if (cantidadCurasMáxima > 5)
                        {
                            Debug.Log("Cantidad Máxima de curas alcanzada");
                        }
                    }
                }
                
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Curacion"))
        {
            cantidadCurasMáxima += 1;
            CurarJugador();
            collision.gameObject.SetActive(false);
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
        vida = maxVida;
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
        CameraMovement.Instance.MoverCamara(7, 7, 0.5f);


        if (vida <= 0)
        {
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
        vida = vida + 20;

        
    }

}
