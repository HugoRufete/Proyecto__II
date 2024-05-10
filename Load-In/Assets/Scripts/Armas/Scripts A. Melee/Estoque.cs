using UnityEngine;

public class Estoque : MonoBehaviour
{

    private float anguloMinimo = 90f;
    private float escalaOriginalX;

    private float timeBtwAttack;

    [Header("Velodidad de ataque")]
    public float attackCooldown; // Tiempo de espera entre ataques
    public Collider2D attackCollider; // Collider que determina la zona de ataque
    public LayerMask whatIsEnemies;

    [Header("Daño")]
    public int damage;

    void Start()
    {
        // Guarda la escala original en el eje X
        escalaOriginalX = transform.localScale.x;
    }

    private void Update()
    {
        if (timeBtwAttack <= 0)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Debug.Log("Atacando...");

                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackCollider.transform.position, attackCollider.bounds.extents.x, whatIsEnemies);
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    enemiesToDamage[i].GetComponent<EnemyHealth>().EnemyTakeDamage(damage);
                    Debug.Log("Enemigo Atacado");
                }

                // Establecer el tiempo de espera antes del próximo ataque
                timeBtwAttack = attackCooldown;
            }
        }
        else
        {
            // Disminuir el tiempo de espera
            timeBtwAttack -= Time.deltaTime;
        }

        float angulo = transform.rotation.eulerAngles.z;

        // Verifica si el objeto ha rotado menos de -90 grados
        if (angulo < anguloMinimo)
        {
            // Invierte la escala en el eje X
            transform.localScale = new Vector3(escalaOriginalX * -1f, transform.localScale.y, transform.localScale.z);
        }
        else
        {
            // Restaura la escala original
            transform.localScale = new Vector3(escalaOriginalX, transform.localScale.y, transform.localScale.z);
        }
    }

    
}
