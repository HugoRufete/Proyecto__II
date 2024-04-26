using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MacheteParent : MonoBehaviour
{
    private Machete machete; // Mant�n una referencia al componente Machete

    private void Start()
    {
        // Intenta obtener la referencia al componente Machete
        machete = GetComponentInChildren<Machete>();

        // Verifica si se encontr� el componente Machete
        if (machete != null)
        {
            Debug.Log("Se encontr� el componente Machete correctamente.");
        }
        else
        {
            Debug.LogWarning("No se encontr� el componente Machete en ning�n objeto hijo.");
        }
    }


    public void AumentarDa�oMacheteParent()
    {
        // Verifica si la referencia a Machete es v�lida
        if (machete != null)
        {
            // Llama al m�todo AumentarDa�oMachete() del componente Machete
            machete.AumentarDa�oMachete();
            Debug.Log("Da�o del machete aumentado!");
        }
        else
        {
            Debug.LogWarning("No se encontr� el componente Machete.");
        }
    }
}
