using System;
using UnityEngine;

[CreateAssetMenu(fileName = "new Consumable", menuName = "Items/Consumable")]
public class ConsumableItem : ScriptableObject, IItem
{
    public ConsumableItem(ConsumableItem item)
    {
        _id = item.Id;
        _name = item.Name;
        _description = item.Description;
        _inventorySprite = item.InventorySprite;
        _dropSprite = item.DropSprite;
        _useSprite = item.UseSprite;
        _maxStackCount = item.MaxStackCount;
    }
    public int Id => _id;
    public Sprite InventorySprite => _inventorySprite;
    public Sprite DropSprite => _dropSprite;
    public Sprite UseSprite => _useSprite;
    public ItemType Type => ItemType.Heal;
    public string Name => _name;
    public string Description => _description;
    public int MaxStackCount => _maxStackCount;

    [SerializeField] private int _id;
    [SerializeField] private string _name;
    [SerializeField] [TextArea(1, 3)] private string _description;
    [SerializeField] private Sprite _inventorySprite;
    [SerializeField] private Sprite _dropSprite;
    [SerializeField] private Sprite _useSprite;
    [SerializeField] private int _maxStackCount;

    public IItemManager GetItemManager(GameObject owner)
    {
        return new HandsItemManager();
    }
}
