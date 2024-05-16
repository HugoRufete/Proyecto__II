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
    public int RondaActual = 1;  // Número de la ronda actual
    public float waveUpdateInterval = 5f;
    private float waveUpdateTimer = 0f;

    public bool jugadorViendaSpawner = false;

    public int maxEnemies;


    private int numDeEnemigos;
    private int tipDeEnemigos;

    private bool jugadorEnBosque = false;
    private bool jugadorEnHierbaRoja = false;
    private bool jugadorEnCementerio = false;
    private bool jugadorEnCasaAterradora = false;
    private bool jugadorEnLago = false;
    private bool jugadorEnDesierto = false;
    private bool jugadorEnCabañaCazador = false;
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
        else if (NombreZona == "CasaCazador" && collision.CompareTag("Player") && !jugadorEnCabañaCazador)
        {
            jugadorEnCabañaCazador = true;
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
        else if (NombreZona == "CasaCazador" && collision.CompareTag("Player") && jugadorEnCabañaCazador)
        {
            jugadorEnCabañaCazador = false;
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
        while (jugadorEnBosque)
        {

            numDeEnemigos = Random.Range(1, 2);
            tipDeEnemigos = Random.Range(1, 4);

            if (numDeEnemigos == 1)
            {
                if (tipDeEnemigos == 1)
                {
                    enemySpawner.SpawnFirefly();
                }
                if (tipDeEnemigos == 2)
                {
                    enemySpawner.SpawnGordetEnemy();
                }
                if (tipDeEnemigos == 3)
                {
                    enemySpawner.SpawnEscudito();
                }
                maxEnemies++;
            }
            if (numDeEnemigos == 2)
            {
                if (tipDeEnemigos == 1)
                {
                    enemySpawner.SpawnFirefly();
                    enemySpawner.SpawnGuadañaEnemy();
                }
                if (tipDeEnemigos == 2)
                {
                    enemySpawner.SpawnGordetEnemy();
                    enemySpawner.SpawnGuadañaEnemy();
                }
                if (tipDeEnemigos == 3)
                {
                    enemySpawner.SpawnGuadañaEnemy();
                    enemySpawner.SpawnSpiritEnemy();
                }
                maxEnemies = maxEnemies + 2;
            }

            yield return new WaitForSeconds(waitTime);
        }

        //Lógica Spawners HierbaRoja
        while (jugadorEnHierbaRoja)
        {

            numDeEnemigos = Random.Range(1, 2);
            tipDeEnemigos = Random.Range(1, 4);

            if (numDeEnemigos == 1)
            {
                if (tipDeEnemigos == 1)
                {
                    enemySpawner.SpawnEscudito();
                }
                if (tipDeEnemigos == 2)
                {
                    enemySpawner.SpawnFirefly();
                }
                if (tipDeEnemigos == 3)
                {
                    enemySpawner.SpawnTentaculin();
                    
                }
                maxEnemies++;
            }
            if (numDeEnemigos == 2)
            {
                if (tipDeEnemigos == 1)
                {
                    enemySpawner.SpawnFirefly();
                    enemySpawner.SpawnFirefly();
                }
                if (tipDeEnemigos == 2)
                {
                    enemySpawner.SpawnFirefly();
                    enemySpawner.SpawnEscudito();
                }
                if (tipDeEnemigos == 3)
                {
                    enemySpawner.SpawnFirefly();
                    enemySpawner.SpawnEscudito();
                }
                maxEnemies = maxEnemies + 2;
            }
        }

        //Lógica Spawners Cementerio
        while (jugadorEnCementerio)
        {
            numDeEnemigos = Random.Range(1, 2);
            tipDeEnemigos = Random.Range(1, 4);

            if (numDeEnemigos == 1)
            {
                if (tipDeEnemigos == 1)
                {
                    enemySpawner.SpawnSpiritEnemy();
                }
                if (tipDeEnemigos == 2)
                {
                    enemySpawner.SpawnEscudito();
                }
                if (tipDeEnemigos == 3)
                {
                    enemySpawner.SpawnGuadañaEnemy();
                }
                maxEnemies++;
            }
            if (numDeEnemigos == 2)
            {
                if (tipDeEnemigos == 1)
                {
                    enemySpawner.SpawnSpiritEnemy();
                    enemySpawner.SpawnEscudito();
                }
                if (tipDeEnemigos == 2)
                {
                    enemySpawner.SpawnGuadañaEnemy();
                    enemySpawner.SpawnEscudito();
                }
                if (tipDeEnemigos == 3)
                {
                    enemySpawner.SpawnSpiritEnemy();
                    enemySpawner.SpawnGuadañaEnemy();
                }
                maxEnemies = maxEnemies + 2;
            }
        }

        //Lógica Spawners Casa Aterradora
        while (jugadorEnCasaAterradora)
        {

            numDeEnemigos = Random.Range(1, 2);
            tipDeEnemigos = Random.Range(1, 4);

            if (numDeEnemigos == 1)
            {
                if (tipDeEnemigos == 1)
                {
                    enemySpawner.SpawnGordetEnemy();
                }
                if (tipDeEnemigos == 2)
                {
                    enemySpawner.SpawnGuadañaEnemy();
                }
                if (tipDeEnemigos == 3)
                {
                    enemySpawner.SpawnFirefly();
                }
                maxEnemies++;
            }
            if (numDeEnemigos == 2)
            {
                if (tipDeEnemigos == 1)
                {
                    enemySpawner.SpawnFirefly();
                    enemySpawner.SpawnGordetEnemy();
                }
                if (tipDeEnemigos == 2)
                {
                    enemySpawner.SpawnGordetEnemy();
                    enemySpawner.SpawnGuadañaEnemy();
                }
                if (tipDeEnemigos == 3)
                {
                    enemySpawner.SpawnGordetEnemy();
                    enemySpawner.SpawnSpiritEnemy();
                }
                maxEnemies = maxEnemies + 2;
            }
        }

        //Lógica Spawners Lago
        while (jugadorEnLago)
        {

            enemySpawner.SpawnGordetEnemy();
            Debug.Log("Instanciando Firefly");
            cantidadDeLlamadas++;

            if (cantidadDeLlamadas == 7 && waitTime > 2)
            {
                waitTime -= 1f;
                cantidadDeLlamadas = 0;
            }

            yield return new WaitForSeconds(waitTime);
        }

        //Lógica spawners desierto
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
                    Debug.Log("Instanciando Guadaña");
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

        //Lógica jugador en Cabaña Cazador
        while (jugadorEnCabañaCazador)
        {
            float randomValue = Random.value;
            Debug.Log("Cogiendo Valor aleatorio");
            if (randomValue >= 0.25f)
            {
                enemySpawner.SpawnGordetEnemy();
                Debug.Log("Instanciando Guadaña");
            }
            else if (randomValue < 0.2 && randomValue > 0.05)
            {
                enemySpawner.SpawnFirefly();
                Debug.Log("Instanciando Spirit");
            }
            else if (randomValue <= 0.05)
            {
                enemySpawner.SpawnGuadañaEnemy();
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

        //Lógica spawners Monasterio
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
                    enemySpawner.SpawnGordetEnemy();
                    initialSpawnCountMonasterio--;
                }

            }
            else
            {
                float randomValue = Random.value;
                Debug.Log("Cogiendo Valor aleatorio");
                if (randomValue >= 0.4f)
                {
                    enemySpawner.SpawnFirefly();
                    Debug.Log("Instanciando Guadaña");
                }
                else if (randomValue < 0.4 && randomValue > 0.1)
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

        //Lógica spawners zona infectada
        while (jugadorEnZonaInfectada)
        {

            numDeEnemigos = Random.Range(1, 2);
            tipDeEnemigos = Random.Range(1, 4);

            if (numDeEnemigos == 1)
            {
                if (tipDeEnemigos == 1)
                {
                    enemySpawner.SpawnGordetEnemy();
                }
                if (tipDeEnemigos == 2)
                {
                    enemySpawner.SpawnGuadañaEnemy();
                }
                if (tipDeEnemigos == 3)
                {
                    enemySpawner.SpawnFirefly();
                }
                maxEnemies++;
            }
            if (numDeEnemigos == 2)
            {
                if (tipDeEnemigos == 1)
                {
                    enemySpawner.SpawnFirefly();
                    enemySpawner.SpawnGordetEnemy();
                }
                if (tipDeEnemigos == 2)
                {
                    enemySpawner.SpawnGordetEnemy();
                    enemySpawner.SpawnGuadañaEnemy();
                }
                if (tipDeEnemigos == 3)
                {
                    enemySpawner.SpawnFirefly();
                    enemySpawner.SpawnSpiritEnemy();
                }
                maxEnemies = maxEnemies + 2;
            }

            yield return new WaitForSeconds(waitTime);
        }

        //Lógica spawners hielo
        while (jugadorEnZonaHielo)
        {

            numDeEnemigos = Random.Range(1, 2);
            tipDeEnemigos = Random.Range(1, 4);

            if (numDeEnemigos == 1)
            {
                if (tipDeEnemigos == 1)
                {
                    enemySpawner.SpawnGordetEnemy();
                }
                if (tipDeEnemigos == 2)
                {
                    enemySpawner.SpawnGuadañaEnemy();
                }
                if (tipDeEnemigos == 3)
                {
                    enemySpawner.SpawnFirefly();
                }
                maxEnemies++;
            }
            if (numDeEnemigos == 2)
            {
                if (tipDeEnemigos == 1)
                {
                    enemySpawner.SpawnFirefly();
                    enemySpawner.SpawnGuadañaEnemy();
                }
                if (tipDeEnemigos == 2)
                {
                    enemySpawner.SpawnGordetEnemy();
                    enemySpawner.SpawnGuadañaEnemy();
                }
                if (tipDeEnemigos == 3)
                {
                    enemySpawner.SpawnFirefly();
                    enemySpawner.SpawnGordetEnemy();
                }
                maxEnemies = maxEnemies + 2;
            }
        }

        //Lógica spawners laboratorio
        while (jugadorEnLaboratorio)
        {
            enemySpawner.SpawnGuadañaEnemy();

            yield return new WaitForSeconds(waitTime);
        }

        //Lógica spawners aldea

        while (jugadorEnAldea)
        {

            numDeEnemigos = Random.Range(1, 2);
            tipDeEnemigos = Random.Range(1, 4);

            if (numDeEnemigos == 1)
            {
                if (tipDeEnemigos == 1)
                {
                    enemySpawner.SpawnGordetEnemy();
                }
                if (tipDeEnemigos == 2)
                {
                    enemySpawner.SpawnSpiritEnemy();
                }
                if (tipDeEnemigos == 3)
                {
                    enemySpawner.SpawnEscudito();
                }
                maxEnemies++;
            }
            if (numDeEnemigos == 2)
            {
                if (tipDeEnemigos == 1)
                {
                    enemySpawner.SpawnEscudito();
                    enemySpawner.SpawnGordetEnemy();
                }
                if (tipDeEnemigos == 2)
                {
                    enemySpawner.SpawnEscudito();
                    enemySpawner.SpawnEscudito();
                }
                if (tipDeEnemigos == 3)
                {
                    enemySpawner.SpawnGordetEnemy();
                    enemySpawner.SpawnSpiritEnemy();
                }

            }
        }

        //Lógica spawners granja
        while (jugadorEnGranja)
        {
            if (jugadorEnGranja)
            {
                float randomValue = Random.value;
                if (randomValue <= 0.55f)
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

    //Lógica Spawners Bosque

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

