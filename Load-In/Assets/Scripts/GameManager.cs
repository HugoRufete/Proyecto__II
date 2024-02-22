using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public List<EnemySpawner> spawners = new List<EnemySpawner>(); // Lista de spawners

    void Start()
    {
        // Invoca la función SpawnEnemy repetidamente cada 5 segundos
        InvokeRepeating("SpawnEnemies", 0f, 5f);
    }

    void SpawnEnemies()
    {
        // Llama a la función de SpawnManager en todos los instanciadores
        foreach (var spawner in spawners)
        {
            spawner.SpawnEnemy();
        }
    }
}
