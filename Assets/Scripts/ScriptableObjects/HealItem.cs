using System;
using UnityEngine;

[CreateAssetMenu(fileName = "new HealItem", menuName = "Items/HealItem")]
public class HealItem : ScriptableObject, IItem
{
    public HealItem(HealItem item)
    {
        _id = item.Id;
        _name = item.Name;
        _description = item.Description;
        _inventorySprite = item.InventorySprite;
        _dropSprite = item.DropSprite;
        _useSprite = item.UseSprite;

        _healCount = item.HealCount;
        _timeToUse = item.TimeToUse;
        _stopToUse = item.StopToUse;
        _speedMultiplier = item.SpeedMultiplier;
    }
    public int Id => _id;
    public Sprite InventorySprite => _inventorySprite;
    public Sprite DropSprite => _dropSprite;
    public Sprite UseSprite => _useSprite;
    public ItemType Type => ItemType.Heal;
    public string Name => _name;
    public string Description => _description;
    public int MaxStackCount => 4;
    public int HealCount => _healCount;
    public float TimeToUse => _timeToUse;
    public bool StopToUse => _stopToUse;
    public float SpeedMultiplier => _speedMultiplier;

    [SerializeField] private int _id;
    [SerializeField] private string _name;
    [SerializeField] [TextArea(1, 3)] private string _description;
    [SerializeField] private Sprite _inventorySprite;
    [SerializeField] private Sprite _dropSprite;
    [SerializeField] private Sprite _useSprite;

    [SerializeField] private int _healCount;
    [SerializeField] private float _timeToUse;
    [SerializeField] private bool _stopToUse;
    [SerializeField] [Range(0, 2)] private float _speedMultiplier;
    public IItemManager GetItemManager(GameObject owner)
    {
        return new HealItemManager(this, owner);
    }
}
