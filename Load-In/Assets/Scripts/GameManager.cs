using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    // Singleton para asegurar que solo haya una instancia del GameManager.
    public static GameManager Instance;
    [Header("Player Config")]
    public VidaPlayer vidaPlayer;
    [Header("Enemy Config")]
    public EnemySpawner enemySpawner;  

    public int totalEnemiesInWave = 6;  // Cantidad total de enemigos por oleada
    public float timeBetweenWaves = 5f;  // Tiempo entre oleadas

    [Header("Ronda Actual")]
    public int RondaActual = 1;  // Número de la ronda actual

    [Header("Puerta Zona de Extración")]
    public GameObject puertaZonaExtr;

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

    private void Start()
    {
        
        IniciarJuego();

        // Buscar el componente Recolector en la escena y se suscribe al evento
        Recolector recolector = FindObjectOfType<Recolector>();
        if (recolector != null)
        {
            recolector.onFragmentosRecogidos.AddListener(DesactivarObjeto);
        }
        else
        {
            Debug.LogWarning("No se encontró el componente Recolector en la escena.");
        }
    }



    void Update()
    {
        
        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
        }



        if(Input.GetKeyDown(KeyCode.H))
        {
            Time.timeScale = 0f;
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            Time.timeScale = 1f;
        }

        countdown -= Time.deltaTime;
    }

    IEnumerator SpawnWave()
    {
        
        int waveType = DetermineWaveType();

        
        if (waveType == 1)
        {
            for (int i = 0; i < totalEnemiesInWave; i++)
            {
                if (RondaActual > 5)
                {
                    enemySpawner.SpawnFirefly();  
                }
                else
                {
                    enemySpawner.SpawnBasicEnemy();
                }
                yield return new WaitForSeconds(1f);  
            }
        }
        else if (waveType == 2)
        {
            int basicCount = totalEnemiesInWave / 2;
            int fireflyCount = totalEnemiesInWave - basicCount;

            for (int i = 0; i < totalEnemiesInWave; i++)
            {
                if (RondaActual > 5)
                {
                    enemySpawner.SpawnFirefly(); 
                }
                else
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
                }

                yield return new WaitForSeconds(1f);  
            }
        }
        else if (waveType == 3)
        {
            for (int i = 0; i < totalEnemiesInWave; i++)
            {
                enemySpawner.SpawnFirefly();
                yield return new WaitForSeconds(1f);  
            }
        }
        else if (waveType == 4)
        {
            for (int i = 0; i < totalEnemiesInWave; i++)
            {
                enemySpawner.SpawnExtraEnemy();  
                yield return new WaitForSeconds(1f);  
            }
        }
        else if (waveType == 5)
        {
            for (int i = 0; i < totalEnemiesInWave; i++)
            {
                enemySpawner.SpawnExtraEnemy();  
                yield return new WaitForSeconds(1f);  
            }
        }

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

    

    public void IniciarJuego()
    {
        // Reiniciar la vida del jugador al comenzar el juego.
        vidaPlayer.ReiniciarVida();
    }

    private void OnEnable()
    {
        // Registrarse para escuchar el evento cuando este script se activa
        FindObjectOfType<Recolector>().onFragmentosRecogidos.AddListener(HandleFragmentosRecogidos);
    }

    private void OnDisable()
    {
        // Desregistrarse del evento cuando se desactiva el script
        FindObjectOfType<Recolector>().onFragmentosRecogidos.RemoveListener(HandleFragmentosRecogidos);
    }

    private void HandleFragmentosRecogidos(int cantidad)
    {
        // Hacer la lógica cuando se recogen los 3 fragmentos
        Debug.Log("¡Se han recogido los tres fragmentos!");
    }

    private void DesactivarObjeto(int cantidad)
    {
        if (puertaZonaExtr != null)
        {
            puertaZonaExtr.SetActive(false);
        }
        else
        {
            Debug.LogWarning("El objeto a desactivar no ha sido asignado en el inspector.");
        }
    }
}
