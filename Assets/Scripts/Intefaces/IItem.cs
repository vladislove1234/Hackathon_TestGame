using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItem  
{
    public int Id { get; }
    public Sprite InventorySprite { get; }
    public Sprite DropSprite { get; }
    public Sprite UseSprite { get; }
    public ItemType Type { get; }
    public string Name { get; }
    public string Description { get; }
    public IItemManager GetItemManager(GameObject owner);
    public int MaxStackCount { get; }
}
public enum ItemType
{
    Weapon,
    Heal,
    Consumables,
    Lighter
}

