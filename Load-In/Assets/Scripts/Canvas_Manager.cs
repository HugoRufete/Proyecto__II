using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canvas_Manager : MonoBehaviour
{
    public GameObject inventory;

    private void Start()
    {

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            InventoryManager inventoryManager = FindObjectOfType<InventoryManager>();
            inventory.SetActive(true);
            inventoryManager.ListItems();
        }
    }
}
