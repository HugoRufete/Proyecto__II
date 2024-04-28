using UnityEngine;

[CreateAssetMenu(fileName ="New Item", menuName ="Item/Create New Item")]

public class Item : ScriptableObject
{
    //Diferentes caracteristicas del item
    public int id;
    public string itenName;
    public int value;
    public Sprite icon;
    public int quantity;
    public ItemType itemType { get; set; }
    public ItemType type { get; set; }
    public enum ItemType
    {
        Weapon,
        Consumable,
        Revolver,

    }
}
