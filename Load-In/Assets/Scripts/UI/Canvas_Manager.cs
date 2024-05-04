using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canvas_Manager : MonoBehaviour
{
    public Animator _animator;

    public GameObject inventory;
    public GameObject weaponContent;
    private bool openedinventory = false;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }
    void Update()
    {
        //Input I
        if (Input.GetKeyDown(KeyCode.I))
        {
            InventoryManager inventoryManager = FindObjectOfType<InventoryManager>();
            if (openedinventory == false)
            {
                Time.timeScale = 0f;
                openedinventory = true;
                inventory.SetActive(true);
                weaponContent.SetActive(false);
                inventoryManager.ListItems();
            }
            //Si esta abierto lo cierra
            else if (openedinventory == true) 
            {
                Time.timeScale=1f;
                openedinventory = false;
                inventory.SetActive(false);
            }
        }
    }
}
