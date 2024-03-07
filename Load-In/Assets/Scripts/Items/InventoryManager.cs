using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static Unity.Burst.Intrinsics.X86.Avx;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public List<Item> Items = new List<Item>();

    public Transform ItemContent;
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
        foreach (Transform item in ItemContent)
        {
            Destroy(item.gameObject);
        }

        // Utilizamos un diccionario para realizar un seguimiento de objetos ya instanciados por ID
        Dictionary<int, GameObject> instantiatedObjects = new Dictionary<int, GameObject>();

        foreach (var item in Items)
        {
            if (!instantiatedObjects.ContainsKey(item.id))
            {
                // Si no existe un objeto con el mismo ID, lo instanciamos
                GameObject obj = Instantiate(InventoryItem, ItemContent);
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
