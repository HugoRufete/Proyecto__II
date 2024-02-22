using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // Prefab del enemigo

    public void SpawnEnemy()
    {
        // Instancia el enemigo en la posición del objeto actual y con la rotación predeterminada
        Instantiate(enemyPrefab, transform.position, Quaternion.identity);
    }
}
