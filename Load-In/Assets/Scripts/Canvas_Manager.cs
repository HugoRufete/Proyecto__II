using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canvas_Manager : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public GameObject inventory;

    private void Start()
    {
         inventoryManager = GetComponent<InventoryManager>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventory.SetActive(true);
            inventoryManager.ListItems();
        }
    }
}
