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

    public int totalEnemiesInWave_1 = 1;  // Cantidad total de enemigos por oleada
    public int totalEnemiesInWave_2 = 1;  // Cantidad total de enemigos por oleada
    public int totalEnemiesInWave_3 = 1;  // Cantidad total de enemigos por oleada
    public int totalEnemiesInWave_4 = 1;  // Cantidad total de enemigos por oleada
    public int totalEnemiesInWave_5 = 1;  // Cantidad total de enemigos por oleada
    public int totalEnemiesInWave_6 = 1;  // Cantidad total de enemigos por oleada
    public float timeBetweenWaves = 10f;  // Tiempo entre oleadas

    [Header("Ronda Actual")]
    public int RondaActual = 1;  // N�mero de la ronda actual
    public float waveUpdateInterval = 5f;
    private float waveUpdateTimer = 0f; // Timer for updating wave

    [Header("Configuraci�n Oleadas")]
    public int roundsPerType1 = 1;  // Cada cu�ntas rondas aparece una oleada de tipo 1
    public int roundsPerType2 = 3;  // Cada cu�ntas rondas aparece una oleada de tipo 2
    public int roundsPerType3 = 5;  // Cada cu�ntas rondas aparece una oleada de tipo 3
    public int roundsPerType4 = 7;  // Cada cu�ntas rondas aparece una oleada de tipo 4
    public int roundsPerType5 = 10; // Cada cu�ntas rondas aparece una oleada de tipo 5
    public int roundsPerType6 = 12; // Cada cu�ntas rondas aparece una oleada de tipo 6

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
        // Update wave every 5 seconds
        waveUpdateTimer += Time.deltaTime;
        if (waveUpdateTimer >= waveUpdateInterval)
        {
            waveUpdateTimer = 0f;
            RondaActual++;

            // Spawn wave at the start of each round
            int waveType = DetermineWaveType();
            SpawnEnemies(waveType);
        }
    }

    void SpawnEnemies(int waveType)
    {
        // Llama a los m�todos de spawn correspondientes en el EnemySpawner seg�n el tipo de oleada
        if (waveType == 1)
        {
            for (int i = 0; i < totalEnemiesInWave_1; i++)
            {
                if (RondaActual > 5)
                {
                    enemySpawner.SpawnGuada�aEnemy();  // A partir de la ronda 6, instanciar enemigos Firefly en lugar de BasicEnemy
                }
                else
                {
                    enemySpawner.SpawnFirefly();
                }
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
                    enemySpawner.SpawnGuada�aEnemy();  // A partir de la ronda 6, instanciar enemigos Firefly en lugar de BasicEnemy
                }
                else
                {
                    if (Random.Range(0, 2) == 0 && basicCount > 0)
                    {
                        enemySpawner.SpawnGuada�aEnemy();
                        basicCount--;
                    }
                    else
                    {
                        enemySpawner.SpawnGordetEnemy();
                    }
                }
            }
        }
    }


    int DetermineWaveType()
    {
        if (RondaActual % roundsPerType6 == 0)
        {
            return 6;
        }
        else if (RondaActual % roundsPerType5 == 0)
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
        // Iniciar el juego aqu�.
        IniciarJuego();
    }

    public void IniciarJuego()
    {
        // Reiniciar la vida del jugador al comenzar el juego.
        vidaPlayer.ReiniciarVida();
    }
}