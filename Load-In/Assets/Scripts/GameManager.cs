using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public EnemySpawner enemySpawner;  // Referencia al script de spawn de enemigos
    public int totalEnemiesInWave = 6;  // Cantidad total de enemigos por oleada
    public float timeBetweenWaves = 5f;  // Tiempo entre oleadas

    public int roundsPerType1 = 1;  // Cada cu�ntas rondas aparece una oleada de tipo 1
    public int roundsPerType2 = 3;  // Cada cu�ntas rondas aparece una oleada de tipo 2
    public int roundsPerType3 = 5;  // Cada cu�ntas rondas aparece una oleada de tipo 3

    public int currentRound = 1;  // N�mero de la ronda actual

    private float countdown = 2f;  // Contador para el inicio de la primera oleada

    void Update()
    {
        // Comienza la cuenta atr�s para la siguiente oleada
        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
        }

        countdown -= Time.deltaTime;
    }

    IEnumerator SpawnWave()
    {
        // Determina el tipo de oleada seg�n el n�mero de ronda
        int waveType = DetermineWaveType();

        // Llama a los m�todos de spawn correspondientes en el EnemySpawner seg�n el tipo de oleada
        if (waveType == 1)
        {
            for (int i = 0; i < totalEnemiesInWave; i++)
            {
                enemySpawner.SpawnBasicEnemy();
                yield return new WaitForSeconds(1f);  // Puedes ajustar este tiempo seg�n sea necesario
            }
        }
        else if (waveType == 2)
        {
            int basicCount = totalEnemiesInWave / 2;
            int fireflyCount = totalEnemiesInWave - basicCount;

            for (int i = 0; i < totalEnemiesInWave; i++)
            {
                if (Random.Range(0, 2) == 0 && basicCount > 0)
                {
                    enemySpawner.SpawnBasicEnemy();
                    basicCount--;
                }
                else
                {
                    enemySpawner.SpawnFirefly();
                }

                yield return new WaitForSeconds(1f);  // Puedes ajustar este tiempo seg�n sea necesario
            }
        }
        else if (waveType == 3)
        {
            for (int i = 0; i < totalEnemiesInWave; i++)
            {
                enemySpawner.SpawnFirefly();
                yield return new WaitForSeconds(1f);  // Puedes ajustar este tiempo seg�n sea necesario
            }
        }

        // Incrementa el n�mero de ronda actual al finalizar la oleada
        currentRound++;
        yield return null;
    }

    int DetermineWaveType()
    {
        if (currentRound % roundsPerType3 == 0)
        {
            return 3;  // Tipo de oleada 3
        }
        else if (currentRound % roundsPerType2 == 0)
        {
            return 2;  // Tipo de oleada 2
        }
        else
        {
            return 1;  // Tipo de oleada 1 (predeterminado)
        }
    }
}
