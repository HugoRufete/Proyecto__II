using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    // Singleton para asegurar que solo haya una instancia del GameManager.
    public static GameManager Instance;
    
    [Header("Player Config")]
    public VidaPlayer vidaPlayer;
    [Header("Enemy Config")]
    public EnemySpawner enemySpawner;  // Referencia al script de spawn de enemigos

    public int totalEnemiesInWave_1 = 2;  // Cantidad total de enemigos por oleada
    public int totalEnemiesInWave_2 = 10;  // Cantidad total de enemigos por oleada
    public int totalEnemiesInWave_3 = 16;  // Cantidad total de enemigos por oleada
    public int totalEnemiesInWave_4 = 20;  // Cantidad total de enemigos por oleada
    public int totalEnemiesInWave_5 = 30;  // Cantidad total de enemigos por oleada
    public float timeBetweenWaves = 5f;  // Tiempo entre oleadas

    [Header("Ronda Actual")]
    public int RondaActual = 1;  // Número de la ronda actual

    [Header("Configuración Oleadas")]
    public int roundsPerType1 = 1;  // Cada cuántas rondas aparece una oleada de tipo 1
    public int roundsPerType2 = 3;  // Cada cuántas rondas aparece una oleada de tipo 2
    public int roundsPerType3 = 5;  // Cada cuántas rondas aparece una oleada de tipo 3
    public int roundsPerType4 = 7;  // Cada cuántas rondas aparece una oleada de tipo 4
    public int roundsPerType5 = 10; // Cada cuántas rondas aparece una oleada de tipo 5

    private float countdown = 2f;  // Contador para el inicio de la primera oleada
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Update()
    {
        // Comienza la cuenta atrás para la siguiente oleada
        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
        }
        countdown -= Time.deltaTime;
    }

    IEnumerator SpawnWave()
    {
        // Determina el tipo de oleada según el número de ronda
        int waveType = DetermineWaveType();

        // Llama a los métodos de spawn correspondientes en el EnemySpawner según el tipo de oleada
        if (waveType == 1)
        {
            for (int i = 0; i < totalEnemiesInWave_1; i++)
            {
                if (RondaActual > 5)
                {
                    enemySpawner.SpawnInitialtEnemy();  // A partir de la ronda 6, instanciar enemigos Firefly en lugar de BasicEnemy
                }
                else
                {
                    enemySpawner.SpawnInitialtEnemy();
                }
                yield return new WaitForSeconds(1f);  // Puedes ajustar este tiempo según sea necesario
            }
        }
        else if (waveType == 2)
        {
            int basicCount = totalEnemiesInWave_2 / 2;
            int fireflyCount = totalEnemiesInWave_2 - basicCount;

            for (int i = 0; i < totalEnemiesInWave_2; i++)
            {
                if (RondaActual > 5)
                {
                    enemySpawner.SpawnFirefly();  // A partir de la ronda 6, instanciar enemigos Firefly en lugar de BasicEnemy
                }
                else
                {
                    if (Random.Range(0, 2) == 0 && basicCount > 0)
                    {
                        enemySpawner.SpawnFirefly();
                        basicCount--;
                    }
                    else
                    {
                        enemySpawner.SpawnInitialtEnemy();
                    }
                }

                yield return new WaitForSeconds(1f);  // Puedes ajustar este tiempo según sea necesario
            }
        }
        else if (waveType == 3)
        {
            for (int i = 0; i < totalEnemiesInWave_3; i++)
            {
                enemySpawner.SpawnFirefly();
                yield return new WaitForSeconds(1f);  // Puedes ajustar este tiempo según sea necesario
            }
        }
        else if (waveType == 4)
        {
            for (int i = 0; i < totalEnemiesInWave_4; i++)
            {
                enemySpawner.SpawnGuadañaEnemy();  // Método que instancia enemigos de tipo random y firefly
                yield return new WaitForSeconds(1f);  // Puedes ajustar este tiempo según sea necesario
            }
        }
        else if (waveType == 5)
        {
            for (int i = 0; i < totalEnemiesInWave_5; i++)
            {
                enemySpawner.SpawnSpiritEnemy();  // Método que instancia enemigos de tipo random
                yield return new WaitForSeconds(1f);  // Puedes ajustar este tiempo según sea necesario
            }
        }

        // Incrementa el número de ronda actual al finalizar la oleada
        RondaActual++;
        yield return null;
    }

    int DetermineWaveType()
    {
        if (RondaActual % roundsPerType5 == 0)
        {
            return 5;  // Tipo de oleada 5
        }
        else if (RondaActual % roundsPerType4 == 0)
        {
            return 4;  // Tipo de oleada 4
        }
        else if (RondaActual % roundsPerType3 == 0)
        {
            return 3;  // Tipo de oleada 3
        }
        else if (RondaActual % roundsPerType2 == 0)
        {
            return 2;  // Tipo de oleada 2
        }
        else
        {
            return 1;  // Tipo de oleada 1 (predeterminado)
        }
    }

    private void Start()
    {
        // Iniciar el juego aquí.
        IniciarJuego();
    }

    public void IniciarJuego()
    {
        // Reiniciar la vida del jugador al comenzar el juego.
        vidaPlayer.ReiniciarVida();
    }
}
