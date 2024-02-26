using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public EnemySpawner enemySpawner;
    public int currentWave = 0;
    public int[] enemiesPerWave; // Puedes ajustar esto según tus necesidades

    void Start()
    {
        StartNextWave();
    }

    void StartNextWave()
    {
        StartCoroutine(SpawnWave());
    }

    IEnumerator SpawnWave()
    {
        if (currentWave < enemiesPerWave.Length)
        {
            for (int i = 0; i < enemiesPerWave[currentWave]; i++)
            {
                // Llama a los métodos públicos de EnemySpawner para spawnear enemigos
                enemySpawner.SpawnBasicEnemy();
                yield return new WaitForSeconds(1f); // Puedes ajustar el tiempo entre enemigos
            }

            currentWave++;
            yield return new WaitForSeconds(3f); // Puedes ajustar el tiempo entre oleadas
            StartNextWave();
        }
        else
        {
            Debug.Log("Fin del juego, todas las oleadas completadas.");
        }
    }
}
