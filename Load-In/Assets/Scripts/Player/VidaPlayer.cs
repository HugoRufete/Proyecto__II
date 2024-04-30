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
    private float reducedDamageMultiplier = 0.5f; // Multiplicador de reducci�n de da�o

    public GameObject imagenReducedDamageActivado;

    public GameObject tiendaUI;

    private void Start()
    {
        ReiniciarVida();
        reducedDamageActivated = false;
    }

    private void Update()
    {
        BarraDeVida.fillAmount = vida / maxVida;
    }

    public void ReiniciarVida()
    {
        vida = maxVida;
    }

    // L�gica de vida
    public void PlayerTakeDamage(int damage)
    {
        if (reducedDamageActivated == true) 
        {
            Debug.Log("---");
            damage = Mathf.RoundToInt(damage * reducedDamageMultiplier); 
        }

        vida -= damage;

        if (vida <= 0)
        {
            Debug.Log("Game Over");
            SceneManager.LoadScene("Game_Over");
        }
    }


    public void ActivateReducedDamage()
    {
        imagenReducedDamageActivado.SetActive(true);
        reducedDamageActivated = true;
        Debug.Log("Reduced damage activated: " + reducedDamageActivated);
        reducedDamageMultiplier = 0.9f; // Reduce el da�o recibido en un 10%
        tiendaUI.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void CurarJugador()
    {
        vida = vida + 20;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Curacion"))
        {
            CurarJugador();
            collision.gameObject.SetActive(false);
        }
    }
}
