using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Wheel_Manager : MonoBehaviour
{
    public GameObject wheelMenu;
    private bool menuActivo = false;

    void Update()
    {
        // Verifica si se presiona la tecla E
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Si el menú está activo, desactívalo
            if (menuActivo)
            {
                wheelMenu.SetActive(false);
                menuActivo = false;
            }
            // Si el menú no está activo, actívalo
            else
            {
                wheelMenu.SetActive(true);
                menuActivo = true;
            }
        }
    }
}
