using UnityEngine;

public class PlayerPushAbility : MonoBehaviour
{
    public float pushForce = 10f; // Fuerza de empuje
    public float pushRadius = 3f; // Radio del área de empuje

    public LayerMask enemyLayer; // Capa que contiene a los enemigos

    private bool habilidadEmpujeDesbloqueada = false;

    public GameObject imagenEmpuje;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && habilidadEmpujeDesbloqueada) 
        {
            PushEnemies();
            Debug.Log("Empujando enemigos");
        }
    }

    void PushEnemies()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, pushRadius, enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            Vector2 pushDirection = (enemy.transform.position - transform.position).normalized;
            enemy.GetComponent<EnemyHealth>().PushBack(pushDirection, pushForce, 5f);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, pushRadius);
    }

    public void DesbloquearHabilidadEmpuje()
    {
        imagenEmpuje.SetActive(true);
        habilidadEmpujeDesbloqueada = true;
        Time.timeScale = 1.0f;
    }
}
