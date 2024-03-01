using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Tipos de enemigos")]
    public GameObject BasicEnemy;
    public GameObject Firefly;

    public List<Transform> spawnPoints;  // Lista de puntos de spawn

    private void Start()
    {
        // Si no has asignado manualmente puntos de spawn, intenta buscarlos en los hijos del objeto actual
        if (spawnPoints == null || spawnPoints.Count == 0)
        {
            Transform[] childTransforms = GetComponentsInChildren<Transform>();
            spawnPoints = new List<Transform>(childTransforms);
            spawnPoints.Remove(transform);  // Elimina el propio transform para que no sea considerado como punto de spawn
        }
    }

    public void SpawnBasicEnemy()
    {
        if (BasicEnemy != null && spawnPoints.Count > 0)
        {
            // Elije un punto de spawn al azar
            int randomIndex = Random.Range(0, spawnPoints.Count);

            // Asegúrate de que el índice esté dentro del rango
            randomIndex = Mathf.Clamp(randomIndex, 0, spawnPoints.Count - 1);

            // Obtén el punto de spawn correspondiente al índice
            Transform spawnPoint = spawnPoints[randomIndex];

            // Instancia el enemigo en el punto de spawn y con la rotación predeterminada
            Instantiate(BasicEnemy, spawnPoint.position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("Falta asignar el prefab de BasicEnemy o los puntos de spawn.");
        }
    }

    public void SpawnFirefly()
    {
        if (Firefly != null && spawnPoints.Count > 0)
        {
            // Elije un punto de spawn al azar
            int randomIndex = Random.Range(0, spawnPoints.Count);

            // Asegúrate de que el índice esté dentro del rango
            randomIndex = Mathf.Clamp(randomIndex, 0, spawnPoints.Count - 1);

            // Obtén el punto de spawn correspondiente al índice
            Transform spawnPoint = spawnPoints[randomIndex];

            // Instancia el enemigo en el punto de spawn y con la rotación predeterminada
            Instantiate(Firefly, spawnPoint.position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("Falta asignar el prefab de Firefly o los puntos de spawn.");
        }
    }

}
