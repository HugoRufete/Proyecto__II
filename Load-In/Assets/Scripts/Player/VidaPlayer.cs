using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VidaPlayer : MonoBehaviour
{
    public float maxVida;
    public float vida;
    public Image BarraDeVida;
    private bool reducedDamageActivated = false; 
    private float reducedDamageMultiplier = 0.5f; // Multiplicador de reducción de daño

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

    // Lógica de vida
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
        }
    }


    public void ActivateReducedDamage()
    {
        reducedDamageActivated = true;
        Debug.Log("Reduced damage activated: " + reducedDamageActivated);
        reducedDamageMultiplier = 0.5f; // Reduce el daño recibido en un 50%
        tiendaUI.SetActive(false);
    }

}
