using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canvas_Manager : MonoBehaviour
{
    public Animator _animator;
    public Animator _animator_2;

    public GameObject inventory;
    private bool openedinventory = false;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _animator.Play("Animación_Panel_Inicio");
        
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
                Time.timeScale = 0f;
                openedinventory = true;
                inventory.SetActive(true);
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
