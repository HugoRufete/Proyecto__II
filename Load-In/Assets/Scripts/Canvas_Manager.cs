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
        if (Input.GetKeyDown(KeyCode.I))
        {

            InventoryManager inventoryManager = FindObjectOfType<InventoryManager>();
            if (openedinventory == false)
            {
                openedinventory = true;
                inventory.SetActive(true);
                inventoryManager.ListItems();
            }
            else if (openedinventory == true) 
            {
                openedinventory = false;
                inventory.SetActive(false);
            }
        }
    }
}
