using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Wheel_Manager : MonoBehaviour
{
    public GameObject wheelMenu;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            wheelMenu.SetActive(true);
        }
    }
}
