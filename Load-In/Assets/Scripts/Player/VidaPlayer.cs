using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class VidaPlayer : MonoBehaviour
{
    public int maxVida;
    public int vida;

    public void ReiniciarVida()
    {
        //Método para reniciar la vida cada vez que el jugador muera
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
