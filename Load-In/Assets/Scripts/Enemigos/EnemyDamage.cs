using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public int damageAmount = 10;
    private VidaPlayer vidaPlayer;

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

    private void InflictDamage()
    {
        // Inflige da�o al jugador
        vidaPlayer.PlayerTakeDamage(damageAmount);
        Debug.Log("Da�o Inflingido");
    }
}
