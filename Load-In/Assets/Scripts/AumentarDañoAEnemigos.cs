using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AumentarDañoAEnemigos : MonoBehaviour
{
    public List<EnemyHealth> enemyHealthList; // Lista de enemigos con el script EnemyHealth

    public static AumentarDañoAEnemigos Instance { get; private set; }

    public GameObject imagenHabilidadDañoAumentado;
    private void Awake()
    {
        Instance = this;
    }

    public void ActivateAdditionalDamage()
    {
        imagenHabilidadDañoAumentado.SetActive(true);

        foreach (var enemyHealth in enemyHealthList)
        {
            Debug.Log("Daño Extra a enemigos activado");
            enemyHealth.additionalDamageActivated = true;
        }
    }
        

}
