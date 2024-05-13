using System.Collections;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{


    // Singleton para asegurar que solo haya una instancia del GameManager.
    public static SpawnerManager Instance;

    [Header("Player Config")]
    public VidaPlayer vidaPlayer;
    [Header("Enemy Config")]
    public EnemySpawner enemySpawner;  // Referencia al script de spawn de enemigos
   
    [Header("Ronda Actual")]
    public int RondaActual = 1;  // N�mero de la ronda actual
    public float waveUpdateInterval = 5f;
    private float waveUpdateTimer = 0f;

    public bool jugadorViendaSpawner = false;

    private bool jugadorEnBosque = false;
    private bool jugadorEnHierbaRoja = false;
    private bool jugadorEnCementerio = false;
    private bool jugadorEnCasaAterradora = false;
    private bool jugadorEnLago = false;
    private bool jugadorEnDesierto = false;
    private bool jugadorEnCaba�aCazador = false;
    private bool jugadorEnMonasterio = false;
    private bool jugadorEnZonaInfectada = false;
    private bool jugadorEnZonaHielo = false;
    private bool jugadorEnGranja = false;
    private bool jugadorEnLaboratorio = false;
    private bool jugadorEnAldea = false;

    public string NombreZona = "";

    private int cantidadDeLlamadas = 0;
    [SerializeField] private float waitTime = 5f;

    private float countdown = 2f;

   
    private void Start()
    {
        jugadorViendaSpawner = false;

        jugadorEnBosque = false;
        jugadorEnHierbaRoja = false;
        jugadorEnZonaInfectada = false;
        jugadorEnAldea = false;
        //IniciarJuego();
    }

    void Update()
    {
        waveUpdateTimer += Time.deltaTime;
        if (waveUpdateTimer >= waveUpdateInterval && jugadorEnBosque)
        {
            waveUpdateTimer = 0f;
            RondaActual++;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (NombreZona == "Bosque" && collision.CompareTag("Player") && !jugadorEnBosque)
        {
            jugadorEnBosque = true;
            StartCoroutine(SpawnEnemieslyOverTime());
        }

        else if (NombreZona == "HierbaRoja" && collision.CompareTag("Player") && !jugadorEnHierbaRoja)
        {
            jugadorEnHierbaRoja = true;
            StartCoroutine(SpawnEnemieslyOverTime());
        }
        else if (NombreZona == "Cementerio" && collision.CompareTag("Player") && !jugadorEnCementerio)
        {
            jugadorEnCementerio = true;
            StartCoroutine(SpawnEnemieslyOverTime());
        }
        else if (NombreZona == "CasaAterradora" && collision.CompareTag("Player") && !jugadorEnCasaAterradora)
        {
            jugadorEnCasaAterradora = true;
            StartCoroutine(SpawnEnemieslyOverTime());
        }
        else if (NombreZona == "Desierto" && collision.CompareTag("Player") && !jugadorEnDesierto)
        {
            jugadorEnDesierto = true;
            StartCoroutine(SpawnEnemieslyOverTime());
        }
        else if (NombreZona == "CasaCazador" && collision.CompareTag("Player") && !jugadorEnCaba�aCazador)
        {
            jugadorEnCaba�aCazador = true;
            StartCoroutine(SpawnEnemieslyOverTime());
        }
        else if (NombreZona == "Monasterio" && collision.CompareTag("Player") && !jugadorEnMonasterio)
        {
            jugadorEnMonasterio = true;
            StartCoroutine(SpawnEnemieslyOverTime());
        }
        else if (NombreZona == "ZonaInfectada" && collision.CompareTag("Player") && !jugadorEnZonaInfectada)
        {
            Debug.Log("Entrando en zona infectada");
            jugadorEnZonaInfectada = true;
            StartCoroutine(SpawnEnemieslyOverTime());
        }
        else if (NombreZona == "Hielo" && collision.CompareTag("Player") && !jugadorEnZonaHielo)
        {
            Debug.Log("Entrando en hielo");
            jugadorEnZonaHielo = true;
            StartCoroutine(SpawnEnemieslyOverTime());
        }
        else if (NombreZona == "Laboratorio" && collision.CompareTag("Player") && !jugadorEnLaboratorio)
        {
            Debug.Log("Entrando en laboratorio");
            jugadorEnLaboratorio = true;
            StartCoroutine(SpawnEnemieslyOverTime());
        }
        else if (NombreZona == "Aldea" && collision.CompareTag("Player") && !jugadorEnAldea)
        {
            Debug.Log("Entrando en Aldea");
            jugadorEnAldea = true;
            StartCoroutine(SpawnEnemieslyOverTime());
        }
        else if (NombreZona == "Granja" && collision.CompareTag("Player") && !jugadorEnGranja)
        {
            Debug.Log("Entrando en Granja");
            jugadorEnGranja = true;
            StartCoroutine(SpawnEnemieslyOverTime());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (NombreZona == "Bosque" && collision.CompareTag("Player") && jugadorEnBosque)
        {
            jugadorEnBosque = false;
            StopCoroutine(SpawnEnemieslyOverTime());
            Debug.Log("Jugador fuera de Bosque");
        }

        else if (NombreZona == "HierbaRoja" && collision.CompareTag("Player") && jugadorEnHierbaRoja)
        {
            jugadorEnHierbaRoja = false;
            StopCoroutine(SpawnEnemieslyOverTime());
            Debug.Log("Jugador fuera de HierbaRoja");
        }
        else if (NombreZona == "Cementerio" && collision.CompareTag("Player") && jugadorEnCementerio)
        {
            jugadorEnCementerio = false;
            StopCoroutine(SpawnEnemieslyOverTime());
            Debug.Log("Jugador fuera de Cementerio");
        }
        else if (NombreZona == "CasaAterradora" && collision.CompareTag("Player") && jugadorEnCasaAterradora)
        {
            jugadorEnCasaAterradora = false;
            StopCoroutine(SpawnEnemieslyOverTime());
            Debug.Log("Jugador fuera de Cementerio");
        }
        else if (NombreZona == "Desierto" && collision.CompareTag("Player") && jugadorEnDesierto)
        {
            jugadorEnDesierto = false;
            StopCoroutine(SpawnEnemieslyOverTime());
            Debug.Log("Jugador fuera de Desierto");
        }
        else if (NombreZona == "CasaCazador" && collision.CompareTag("Player") && jugadorEnCaba�aCazador)
        {
            jugadorEnCaba�aCazador = false;
            StopCoroutine(SpawnEnemieslyOverTime());
            Debug.Log("Jugador fuera de CasaCazador");
        }
        else if (NombreZona == "Monasterio" && collision.CompareTag("Player") && jugadorEnMonasterio)
        {
            jugadorEnMonasterio = false;
            StopCoroutine(SpawnEnemieslyOverTime());
            Debug.Log("Jugador fuera de Monasterio");
        }
        else if (NombreZona == "ZonaInfectada" && collision.CompareTag("Player") && jugadorEnZonaInfectada)
        {
            jugadorEnZonaInfectada = false;
            StopCoroutine(SpawnEnemieslyOverTime());
            Debug.Log("Jugador fuera de Zona Infectada");
        }
        else if (NombreZona == "Hielo" && collision.CompareTag("Player") && jugadorEnZonaHielo)
        {
            jugadorEnZonaHielo = false;
            StopCoroutine(SpawnEnemieslyOverTime());
            Debug.Log("Jugador fuera de Zona de hielo");
        }
        else if (NombreZona == "Laboratorio" && collision.CompareTag("Player") && jugadorEnLaboratorio)
        {
            jugadorEnLaboratorio = false;
            StopCoroutine(SpawnEnemieslyOverTime());
            Debug.Log("Jugador fuera de Zona de hielo");
        }
        else if (NombreZona == "Aldea" && collision.CompareTag("Player") && jugadorEnAldea)
        {
            jugadorEnAldea = false;
            StopCoroutine(SpawnEnemieslyOverTime());
            Debug.Log("Jugador fuera de Zona de hielo");
        }
        else if (NombreZona == "Granja" && collision.CompareTag("Player") && jugadorEnGranja)
        {
            jugadorEnGranja = false;
            StopCoroutine(SpawnEnemieslyOverTime());
            Debug.Log("Jugador fuera de Zona de granja");
        }
    }
    private IEnumerator SpawnEnemieslyOverTime()
    {
        //L�gica Spawners Bosque
        while (jugadorEnBosque)
        {
           
            enemySpawner.SpawnFirefly();
            Debug.Log("Instanciando Firefly");
            cantidadDeLlamadas++;

            if (cantidadDeLlamadas == 7 && waitTime > 2)
            {
                waitTime -= 1f;
                cantidadDeLlamadas = 0;
            }

                yield return new WaitForSeconds(waitTime);
        }
        
        //L�gica Spawners HiervaRoja
        while (jugadorEnHierbaRoja)
        {
            float randomValue = Random.value;
            Debug.Log("Cogiendo Valor aleatorio");
            if (randomValue > 0.35f)
            {
                enemySpawner.SpawnFirefly();
                Debug.Log("Instanciando Firefly");
            }
            else
            {
                enemySpawner.SpawnSpiritEnemy();
                Debug.Log("Instanciando Spirit");
            }

            cantidadDeLlamadas++;

            if (cantidadDeLlamadas == 7 && waitTime > 2)
            {
                waitTime -= 1f;
                cantidadDeLlamadas = 0;
            }

            yield return new WaitForSeconds(waitTime);
        }

        //L�gica Spawners Cementerio
        int initialSpawnCountCementerio = 3;
        while (jugadorEnCementerio)
        {
            if (initialSpawnCountCementerio > 0)
            {
                enemySpawner.SpawnSpiritEnemy();
                Debug.Log("Instanciando Spirit");
                initialSpawnCountCementerio--;
            }
            else
            {
                float randomValue = Random.value;
                Debug.Log("Cogiendo Valor aleatorio");
                if (randomValue > 0.7f)
                {
                    enemySpawner.SpawnGuada�aEnemy();
                    Debug.Log("Instanciando Guada�a");
                }
                else
                {
                    enemySpawner.SpawnSpiritEnemy();
                    Debug.Log("Instanciando Spirit");
                }
            }

            cantidadDeLlamadas++;

            if (cantidadDeLlamadas == 7 && waitTime > 2)
            {
                waitTime -= 1f;
                cantidadDeLlamadas = 0;
            }

            yield return new WaitForSeconds(waitTime);
        }

        //L�gica Spawners Casa Aterradora
        while (jugadorEnCasaAterradora)
        {
            if (initialSpawnCountCementerio > 0)
            {
                enemySpawner.SpawnSpiritEnemy();
                Debug.Log("Instanciando Spirit");
                initialSpawnCountCementerio--;
            }
            else
            {
                float randomValue = Random.value;
                Debug.Log("Cogiendo Valor aleatorio");
                if (randomValue > 0.7f)
                {
                    enemySpawner.SpawnGuada�aEnemy();
                    Debug.Log("Instanciando Guada�a");
                }
                else
                {
                    enemySpawner.SpawnSpiritEnemy();
                    Debug.Log("Instanciando Spirit");
                }
            }

            cantidadDeLlamadas++;

            if (cantidadDeLlamadas == 7 && waitTime > 2)
            {
                waitTime -= 1f;
                cantidadDeLlamadas = 0;
            }

            yield return new WaitForSeconds(waitTime);
        }

        //L�gica Spawners Lago
        while (jugadorEnLago)
        {

            enemySpawner.SpawnTentaculin();
            Debug.Log("Instanciando Firefly");
            cantidadDeLlamadas++;

            if (cantidadDeLlamadas == 7 && waitTime > 2)
            {
                waitTime -= 1f;
                cantidadDeLlamadas = 0;
            }

            yield return new WaitForSeconds(waitTime);
        }

        //L�gica spawners desierto
        int initialSpawnCountDesierto = 3;
        while (jugadorEnDesierto)
        {
            if (initialSpawnCountDesierto > 0)
            {
                enemySpawner.SpawnSpiritEnemy();
                Debug.Log("Instanciando Spirit");
                initialSpawnCountDesierto--;
            }
            else
            {
                float randomValue = Random.value;
                Debug.Log("Cogiendo Valor aleatorio");
                if (randomValue > 0.4f)
                {
                    enemySpawner.SpawnEscudito();
                    Debug.Log("Instanciando Guada�a");
                }
                else
                {
                    enemySpawner.SpawnSpiritEnemy();
                    Debug.Log("Instanciando Spirit");
                }
            }

            cantidadDeLlamadas++;

            if (cantidadDeLlamadas == 7 && waitTime > 2)
            {
                waitTime -= 1f;
                cantidadDeLlamadas = 0;
            }

            yield return new WaitForSeconds(waitTime);
        }

        //L�gica jugador en Caba�a Cazador
        while (jugadorEnCaba�aCazador)
        {
            float randomValue = Random.value;
            Debug.Log("Cogiendo Valor aleatorio");
            if (randomValue >= 0.25f)
            {
                enemySpawner.SpawnGordetEnemy();
                Debug.Log("Instanciando Guada�a");
            }
            else if (randomValue < 0.2 && randomValue > 0.05)
            {
                enemySpawner.SpawnFirefly();
                Debug.Log("Instanciando Spirit");
            }
            else if (randomValue <= 0.05)
            {
                enemySpawner.SpawnGuada�aEnemy();
                Debug.Log("Instanciando Spirit");
            }


            cantidadDeLlamadas++;

            if (cantidadDeLlamadas == 7 && waitTime > 2)
            {
                waitTime -= 1f;
                cantidadDeLlamadas = 0;
            }

            yield return new WaitForSeconds(waitTime);
        }
       
        //L�gica spawners Monasterio
        int initialSpawnCountMonasterio = 10;
        while (jugadorEnMonasterio)
        {
            if (initialSpawnCountMonasterio > 0)
            {
                float randomValue = Random.value;
                Debug.Log("Cogiendo Valor aleatorio");
                if (randomValue >= 0.6f)
                {
                    enemySpawner.SpawnFirefly();
                    initialSpawnCountMonasterio--;
                }
                else
                {
                    enemySpawner.SpawnTentaculin();
                    initialSpawnCountMonasterio--;
                }
               
            }
            else
            {
                float randomValue = Random.value;
                Debug.Log("Cogiendo Valor aleatorio");
                if (randomValue >= 0.4f)
                {
                    enemySpawner.SpawnTentaculin();
                    Debug.Log("Instanciando Guada�a");
                }
                else if (randomValue < 0.4 && randomValue > 0.1 )
                {
                    enemySpawner.SpawnSpiritEnemy();
                    Debug.Log("Instanciando Spirit");
                }
                else if (randomValue < 0.1)
                {
                    enemySpawner.SpawnFirefly();
                    Debug.Log("Instanciando Spirit");
                }
            }

            cantidadDeLlamadas++;

            if (cantidadDeLlamadas == 7 && waitTime > 2)
            {
                waitTime -= 1f;
                cantidadDeLlamadas = 0;
            }

            yield return new WaitForSeconds(waitTime);
        }

        while (jugadorEnZonaInfectada)
        {
            Debug.Log("Instanciando Enemigo");
            if (Random.value < 0.16f)
            {
                // Define an array of the 6 methods you want to choose from
                System.Action[] methods = new System.Action[6]
                {
            enemySpawner.SpawnFirefly,
            enemySpawner.SpawnSpiritEnemy,
            enemySpawner.SpawnEscudito,
            enemySpawner.SpawnGordetEnemy,
            enemySpawner.SpawnTentaculin,
            enemySpawner.SpawnGordetEnemy
                };

                int randomIndex = Random.Range(0, methods.Length);
                methods[randomIndex]();
            }

            // Wait for 5 seconds before checking the condition again
            if (waitTime <= 5f)
            {
                waitTime += 5f;
            }

            cantidadDeLlamadas++;

            if (cantidadDeLlamadas == 7 && waitTime > 2)
            {
                waitTime -= 1f;
                cantidadDeLlamadas = 0;
            }

            yield return new WaitForSeconds(waitTime);
        }

        while(jugadorEnZonaHielo)
        {
            if(jugadorEnZonaHielo)
            {
                float randomValue = Random.value;
                Debug.Log(randomValue);
                if (randomValue >= 0.5f && randomValue <= 1)
                {
                    enemySpawner.SpawnTentaculin();
                }
                else if (randomValue < 50 && randomValue >= 20)
                {
                    enemySpawner.SpawnGordetEnemy();
                    Debug.Log("Instanciando Spirit");
                }
                else if(randomValue < 20)
                {
                    enemySpawner.SpawnGuada�aEnemy();
                }
            }
            cantidadDeLlamadas++;

            if (cantidadDeLlamadas == 7 && waitTime > 2)
            {
                waitTime -= 1f;
                cantidadDeLlamadas = 0;
            }

            yield return new WaitForSeconds(waitTime);
        }

        while (jugadorEnLaboratorio)
        {
            enemySpawner.SpawnGuada�aEnemy();

            yield return new WaitForSeconds(waitTime);
        }
        
        int initialSpawnCountAldea = 10;
        while (jugadorEnAldea)
        {
            Debug.Log(initialSpawnCountAldea);
            if (initialSpawnCountAldea > 5)
            {
                enemySpawner.SpawnFirefly();
            }
            else if (initialSpawnCountAldea <= 5)
            {
                float randomValue = Random.value;
                Debug.Log("Cogiendo Valor aleatorio");
                if (randomValue >= 0.5f)
                {
                    enemySpawner.SpawnFirefly();
                    Debug.Log("Instanciando Guada�a");
                }
                else
                {
                    enemySpawner.SpawnSpiritEnemy();
                    Debug.Log("Instanciando Spirit");
                }
            }
            else if (initialSpawnCountAldea <= 0)
            {
                float randomValue = Random.value;
                Debug.Log("Cogiendo Valor aleatorio");
                if (randomValue > 0 && randomValue <= 0.33f)
                {
                    enemySpawner.SpawnFirefly();
                    Debug.Log("Instanciando Guada�a");
                }
                else if (randomValue > 0.33f && randomValue <= 0.66f)
                {
                    enemySpawner.SpawnSpiritEnemy();
                    Debug.Log("Instanciando Spirit");
                }
                else if(randomValue > 0.66f)
                {
                    enemySpawner.SpawnTentaculin();
                }
            }

            cantidadDeLlamadas++;

            if (cantidadDeLlamadas == 7 && waitTime > 2)
            {
                waitTime -= 1f;
                cantidadDeLlamadas = 0;
            }
            initialSpawnCountAldea--;
            yield return new WaitForSeconds(waitTime);
        }

        while (jugadorEnGranja)
        {
            if(jugadorEnGranja)
            {
                float randomValue = Random.value;
                if(randomValue <= 0.55f)
                {
                    enemySpawner.SpawnEscudito();
                }
                else if (randomValue >= 0.55 && randomValue < 0.85f)
                {
                    enemySpawner.SpawnFirefly();
                }
                else if (randomValue > 0.85f)
                {
                    enemySpawner.SpawnGordetEnemy();
                }

            }
            
            cantidadDeLlamadas++;

            if (cantidadDeLlamadas == 7 && waitTime > 2)
            {
                waitTime -= 1f;
                cantidadDeLlamadas = 0;
            }
            else if (waitTime < 3)
            {
                waitTime = 3;
            }
            yield return new WaitForSeconds(waitTime);
        }
    }

    public void IniciarJuego()
    {
        jugadorEnBosque = false;
        vidaPlayer.ReiniciarVida();
    }

    public void JugadorViendoSpawner()
    {
        jugadorViendaSpawner = true;
    }
}