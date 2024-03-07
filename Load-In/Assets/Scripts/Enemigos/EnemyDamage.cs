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
            // Comienza a infligir daño por segundo al jugador
            InvokeRepeating("InflictDamage", 1f, 1f);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Detiene el daño cuando el jugador sale del área del enemigo
            CancelInvoke("InflictDamage");
        }
    }

    private void InflictDamage()
    {
        // Inflige daño al jugador cada segundo
        vidaPlayer.PlayerTakeDamage(damagePerSecond);
    }
}
