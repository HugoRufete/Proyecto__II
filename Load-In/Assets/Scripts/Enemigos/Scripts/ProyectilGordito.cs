using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProyectilGordito : MonoBehaviour
{
    

    private void OnTriggerEnter(Collider other)
    {
        // Comprobar si el objeto con el que colisionamos es el jugador
        if (other.CompareTag("Player"))
        {
            // Obtener el componente de vida del jugador
            VidaPlayer vidaPlayer = other.GetComponent<VidaPlayer>();

            // Si encontramos el componente de vida, le quitamos vida al jugador
            if (vidaPlayer != null)
            {
                vidaPlayer.PlayerTakeDamage(1);
            }
            else
            {
                Debug.LogWarning("El jugador no tiene un componente VidaJugador adjunto.");
            }

            // Destruir el proyectil después de colisionar con el jugador
            Destroy(gameObject);
        }
    }
}
