using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerAreaDamage : MonoBehaviour
{
    public float damageRadius;
    public int damageAmount;
    public LayerMask enemyLayer;
    public float cooldownTime; 

    private float cooldownTimer = 0f;
    public TMP_Text cooldownText; // Referencia al objeto de texto en la interfaz de usuario

    [Header("Ataque Especial Area")]
    private bool ataqueEspecialAreaDesbloqueable = false;
    public GameObject uiTimerHabilidadEspecial;

    void Update()
    {
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime; // Actualiza el temporizador de enfriamiento
            UpdateCooldownUI();
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                DealDamageInArea();
                cooldownTimer = cooldownTime; // Reinicia el temporizador de enfriamiento
            }
        }
    }

    void DealDamageInArea()
    {
        cooldownText = GameObject.FindWithTag("CooldownText").GetComponent<TMP_Text>();

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, damageRadius, enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyHealth>().EnemyTakeDamage(damageAmount);
        }
    }

    void UpdateCooldownUI()
    {
        int seconds = Mathf.CeilToInt(cooldownTimer);
        cooldownText.text = seconds.ToString("00"); // Muestra el tiempo restante en formato de dos d�gitos
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, damageRadius);
    }

    public void DesbloquearAtaqueArea()
    {
        ataqueEspecialAreaDesbloqueable = true;
        uiTimerHabilidadEspecial.SetActive(true);
    }
}