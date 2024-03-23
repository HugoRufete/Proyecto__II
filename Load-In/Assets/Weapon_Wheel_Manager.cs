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
            // Si el men� est� activo, desact�valo
            if (menuActivo)
            {
                wheelMenu.SetActive(false);
                menuActivo = false;
            }
            // Si el men� no est� activo, act�valo
            else
            {
                wheelMenu.SetActive(true);
                menuActivo = true;
            }
        }
    }
}
