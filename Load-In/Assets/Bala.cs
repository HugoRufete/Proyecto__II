using UnityEngine;

public class Bala : MonoBehaviour
{
    public int damageAmount = 10;

    void Start()
    {
        Invoke("DesactivarCollider", 0.2f);
        Destroy(gameObject, 3f);
    }

    void DesactivarCollider()
    {
        GetComponent<Collider2D>().enabled = false;
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
