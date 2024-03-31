using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Tipos de enemigos")]
    public GameObject firefly;
    public GameObject gordet;
    public GameObject guadaña_;
    public GameObject spirit;

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

    public void SpawnFirefly()
    {
        if (firefly != null && spawnPoints.Count > 0)
        {
            // Elije un punto de spawn al azar
            int randomIndex = Random.Range(0, spawnPoints.Count);

            // Asegúrate de que el índice esté dentro del rango
            randomIndex = Mathf.Clamp(randomIndex, 0, spawnPoints.Count - 1);

            // Obtén el punto de spawn correspondiente al índice
            Transform spawnPoint = spawnPoints[randomIndex];

            // Instancia el enemigo en el punto de spawn y con la rotación predeterminada
            Instantiate(firefly, spawnPoint.position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("Falta asignar el prefab de Firefly o los puntos de spawn.");
        }
    }

    public void SpawnGuadañaEnemy()
    {
        if (guadaña_ != null && spawnPoints.Count > 0)
        {
            // Elije un punto de spawn al azar
            int randomIndex = Random.Range(0, spawnPoints.Count);

            // Asegúrate de que el índice esté dentro del rango
            randomIndex = Mathf.Clamp(randomIndex, 0, spawnPoints.Count - 1);

            // Obtén el punto de spawn correspondiente al índice
            Transform spawnPoint = spawnPoints[randomIndex];

            // Instancia el enemigo en el punto de spawn y con la rotación predeterminada
            Instantiate(guadaña_, spawnPoint.position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("Falta asignar el prefab de BasicEnemy o los puntos de spawn.");
        }
    }

    public void SpawnSpiritEnemy()
    {
        if (spirit != null && spawnPoints.Count > 0)
        {
            // Elije un punto de spawn al azar
            int randomIndex = Random.Range(0, spawnPoints.Count);

            // Asegúrate de que el índice esté dentro del rango
            randomIndex = Mathf.Clamp(randomIndex, 0, spawnPoints.Count - 1);

            // Obtén el punto de spawn correspondiente al índice
            Transform spawnPoint = spawnPoints[randomIndex];

            // Instancia el enemigo en el punto de spawn y con la rotación predeterminada
            Instantiate(spirit, spawnPoint.position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("Falta asignar el prefab de BasicEnemy o los puntos de spawn.");
        }
    }


    public void SpawnGordetEnemy()
    {
        if (gordet != null && spawnPoints.Count > 0)
        {
            // Elije un punto de spawn al azar
            int randomIndex = Random.Range(0, spawnPoints.Count);

            // Asegúrate de que el índice esté dentro del rango
            randomIndex = Mathf.Clamp(randomIndex, 0, spawnPoints.Count - 1);

            // Obtén el punto de spawn correspondiente al índice
            Transform spawnPoint = spawnPoints[randomIndex];

            // Instancia el enemigo en el punto de spawn y con la rotación predeterminada
            Instantiate(gordet, spawnPoint.position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("Falta asignar el prefab de BasicEnemy o los puntos de spawn.");
        }
    }




}
