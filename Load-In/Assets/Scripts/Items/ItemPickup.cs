using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item item;

    void Pickup()
    {
        //Se añade el objeto a la lista del inventario y se destruye 
        InventoryManager.Instance.Add(item);
        item.quantity++;
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Si el tag de la colision es Player se llama al metodo de arriba
        if (collision.tag == ("Player"))
        {
            Pickup();
        }
    }
}
