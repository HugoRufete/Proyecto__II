using System.Collections;
using System.Collections.Generic;
//using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class VidaPlayer : MonoBehaviour
{
    public float maxVida;
    public float vida;
    public Image BarraDeVida;

    private void Start()
    {
        ReiniciarVida();
    }

    private void Update()
    {
        BarraDeVida.fillAmount = vida/maxVida;
    }

    public void ReiniciarVida()
    {
        vida = maxVida;
    }

    // Logica de vida 
    public void PlayerTakeDamage(int daño)
    {
        vida -= daño;

        if (vida <= 0)
        {
            Debug.Log("Game Over");
        }
    }

    
}
