using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public int damagePerSecond = 10;
    public VidaPlayer vidaPlayer;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Comienza a infligir da�o por segundo al jugador
            InvokeRepeating("InflictDamage", 1f, 1f);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Detiene el da�o cuando el jugador sale del �rea del enemigo
            CancelInvoke("InflictDamage");
        }
    }

    private void InflictDamage()
    {
        // Inflige da�o al jugador cada segundo
        vidaPlayer.PlayerTakeDamage(damagePerSecond);
    }
}
