using UnityEngine;

public class Sword : MonoBehaviour
{
    private float timeBtwAttack;
    public float startTimeBtwAttack;

    public Transform attackPos;
    public LayerMask whatIsEnemies;
    public float attackRange;
    public int damage;

    private void Update()
    {
        if(timeBtwAttack <= 0) 
        {
            if(Input.GetKey(KeyCode.Escape))
            {
                Debug.Log("Atacando...");
                Collider2D [] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);
                for(int i = 0; i < enemiesToDamage.Length; i++)
                {
                    enemiesToDamage[i].GetComponent<EnemyHealth>().EnemyTakeDamage(damage);
                    Debug.Log("Enemigo Atacado");

                }
            }        
            timeBtwAttack = startTimeBtwAttack;
        }
        else
        {
            timeBtwAttack = Time.deltaTime;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
