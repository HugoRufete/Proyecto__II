using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaShotgun : MonoBehaviour
{
    public int damageAmount = 10;
    public AudioClip shotGun;

    void Start()
    {
        ControladorSonido.Instance.EjecutarSonido(shotGun);
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
