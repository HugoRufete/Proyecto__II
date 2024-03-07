using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canvas_Manager : MonoBehaviour
{
    public GameObject inventory;
    private bool openedinventory = false;

    private void Start()
    {

    }
    void Update()
    {
        //Input I
        if (Input.GetKeyDown(KeyCode.I))
        {
            //Llama a el script inventoryManager
            InventoryManager inventoryManager = FindObjectOfType<InventoryManager>();
            //Si el inventario esta cerrado lo abre 
            if (openedinventory == false)
            {
                openedinventory = true;
                inventory.SetActive(true);
                inventoryManager.ListItems();
            }
            //Si esta abierto lo cierra
            else if (openedinventory == true) 
            {
                openedinventory = false;
                inventory.SetActive(false);
            }
        }
    }
}
