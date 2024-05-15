using UnityEngine;

public class Bala : MonoBehaviour
{
    public int damageAmount = 10;
    public AudioClip SniperBullet;

    void Start()
    {
        ControladorSonido.Instance.EjecutarSonido(SniperBullet);
        Invoke("ActivarCollider", 0.05f);
        Destroy(gameObject, 3f);
    }

    void ActivarCollider()
    {
        GetComponent<Collider2D>().enabled = true;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();

        if (enemyHealth != null)
        {
            enemyHealth.EnemyTakeDamage(damageAmount);
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
