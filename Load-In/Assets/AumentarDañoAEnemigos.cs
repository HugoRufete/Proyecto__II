using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AumentarDañoAEnemigos : MonoBehaviour
{
    public List<EnemyHealth> enemyHealthList; // Lista de enemigos con el script EnemyHealth

    public static AumentarDañoAEnemigos Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void ActivateAdditionalDamage()
    {
        // Activar la habilidad que aumenta el daño al enemigo
        foreach (var enemyHealth in enemyHealthList)
        {
            enemyHealth.additionalDamageActivated = true;
        }
    }
        

}
