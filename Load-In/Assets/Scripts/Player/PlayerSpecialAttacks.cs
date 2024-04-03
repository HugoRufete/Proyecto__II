using UnityEngine;
using TMPro;

public class PlayerAreaDamage : MonoBehaviour
{
    public float damageRadius;
    public int damageAmount;
    public LayerMask enemyLayer;
    public float cooldownTime = 0;

    private float cooldownTimer = 0f;
    public TMP_Text cooldownText;

    [Header("Ataque Especial Area")]
    private bool ataqueEspecialAreaDesbloqueable = false;
    public GameObject uiTimerHabilidadEspecial;
    public GameObject imagenAreaAttackActivado;

    void Update()
    {
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime; // Actualiza el temporizador de enfriamiento
            UpdateCooldownUI();
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Q) && ataqueEspecialAreaDesbloqueable)
            {
                DealDamageInArea();
                cooldownTimer = cooldownTime; // Reinicia el temporizador de enfriamiento
            }
        }
    }

    void DealDamageInArea()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, damageRadius, enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyHealth>().EnemyTakeDamage(damageAmount);
        }
    }

    void UpdateCooldownUI()
    {
        int seconds = Mathf.CeilToInt(cooldownTimer);
        cooldownText.text = seconds.ToString("00");
    }

    public void DesbloquearAtaqueArea()
    {
        imagenAreaAttackActivado.SetActive(true);
        ataqueEspecialAreaDesbloqueable = true;
        uiTimerHabilidadEspecial.SetActive(true);
        Time.timeScale = 1.0f;
    }
}
