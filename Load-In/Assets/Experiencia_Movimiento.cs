using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Experiencia_Movimiento : MonoBehaviour
{
    private GameObject jugador; // Referencia al jugador
    private bool moverse = false; // Indicador para activar el movimiento
    private float velocidad = 10f; // Velocidad de movimiento

    // Start is called before the first frame update
    void Start()
    {
        jugador = GameObject.FindWithTag("Player");
        if (jugador == null)
        {
            Debug.LogError("No se encontró al jugador.");
        }
        else
        {
            StartCoroutine(MoverDespuesDeDelay(0.5f));
        }
    }

    IEnumerator MoverDespuesDeDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        moverse = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (moverse && jugador != null)
        {
            Vector3 direccion = (jugador.transform.position - transform.position).normalized;
            transform.position = Vector3.MoveTowards(transform.position, jugador.transform.position, velocidad * Time.deltaTime);
        }
    }
}
