using System.Collections;
using System.Collections.Generic;
//using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
//using static Unity.Burst.Intrinsics.X86.Avx;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public List<Item> Items = new List<Item>();

    public Transform WeaponItemContent;
    public Transform ConsumableItemContent;
    public GameObject InventoryItem;
    public TMP_Text quantityText;
    private void Awake()
    {
        Instance = this;
    }

    public void Add(Item item)
    {
        //Se añade un objeto a la lista
        Items.Add(item);
    }
    
    public void Remove(Item item)
    {
        //Se quita un objeto de la lista
        Items.Remove(item);
    }

    public void ListItems()
    {
        //Se limpia la lista antes de meter los objetos para que no se multipliquen visualmente
        foreach (Transform item in WeaponItemContent)
        {
            Destroy(item.gameObject);
        }
        foreach (Transform item in ConsumableItemContent)
        {
            Destroy(item.gameObject);
        }

        // Utilizamos un diccionario para realizar un seguimiento de objetos ya instanciados por ID
        Dictionary<int, GameObject> instantiatedObjects = new Dictionary<int, GameObject>();

        foreach (var item in Items)
        {
            if (item.itemType == Item.ItemType.Weapon && !instantiatedObjects.ContainsKey(item.id))
            {
                // Si no existe un objeto con el mismo ID, lo instanciamos
                GameObject obj = Instantiate(InventoryItem, WeaponItemContent);
                var itemIcon = obj.transform.Find("Image").GetComponent<Image>();

                itemIcon.sprite = item.icon;

                if (item.quantity > 1)
                {
                    var quantityText = obj.GetComponentInChildren<TMP_Text>();
                    quantityText.text = item.quantity.ToString();
                }
                else
                {
                    var quantityText = obj.GetComponentInChildren<TMP_Text>();
                    quantityText.text = ""; // Oculta el texto si la cantidad es 1
                }

                // Agregamos el objeto al diccionario de objetos instanciados
                instantiatedObjects.Add(item.id, obj);
            }

            if (item.itemType == Item.ItemType.Consumable && !instantiatedObjects.ContainsKey(item.id))
            {
                // Si no existe un objeto con el mismo ID, lo instanciamos
                GameObject obj = Instantiate(InventoryItem, ConsumableItemContent);
                var itemIcon = obj.transform.Find("Image").GetComponent<Image>();

                itemIcon.sprite = item.icon;

                if (item.quantity > 1)
                {
                    var quantityText = obj.GetComponentInChildren<TMP_Text>();
                    quantityText.text = item.quantity.ToString();
                }
                else
                {
                    var quantityText = obj.GetComponentInChildren<TMP_Text>();
                    quantityText.text = ""; // Oculta el texto si la cantidad es 1
                }

                // Agregamos el objeto al diccionario de objetos instanciados
                instantiatedObjects.Add(item.id, obj);
            }
        }
    }
}
