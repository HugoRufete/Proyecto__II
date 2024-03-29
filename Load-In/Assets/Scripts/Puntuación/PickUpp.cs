using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpp : MonoBehaviour
{
    public int cantidadExperiencia = 10; // Cantidad de experiencia que da este objeto.

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PuntosPlayer jugador = other.GetComponent<PuntosPlayer>();
            if (jugador != null)
            {
                jugador.RecogerExperiencia(cantidadExperiencia);
                Destroy(gameObject);
            }
        }
    }
}
