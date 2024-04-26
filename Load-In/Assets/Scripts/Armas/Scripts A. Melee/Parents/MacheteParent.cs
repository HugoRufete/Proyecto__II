using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MacheteParent : MonoBehaviour
{
    private Machete machete; // Mantén una referencia al componente Machete

    private void Start()
    {
        // Intenta obtener la referencia al componente Machete
        machete = GetComponentInChildren<Machete>();

        // Verifica si se encontró el componente Machete
        if (machete != null)
        {
            Debug.Log("Se encontró el componente Machete correctamente.");
        }
        else
        {
            Debug.LogWarning("No se encontró el componente Machete en ningún objeto hijo.");
        }
    }


    public void AumentarDañoMacheteParent()
    {
        // Verifica si la referencia a Machete es válida
        if (machete != null)
        {
            // Llama al método AumentarDañoMachete() del componente Machete
            machete.AumentarDañoMachete();
            Debug.Log("Daño del machete aumentado!");
        }
        else
        {
            Debug.LogWarning("No se encontró el componente Machete.");
        }
    }
}
