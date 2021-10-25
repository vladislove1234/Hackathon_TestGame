using System;
using UnityEngine;

public class ConsumableItemManager : IItemManager
{
    public float SpeedMultiplier { get; set; }
    public ConsumableItem Item { get; private set; }

    public string ItemText => Item.Name;

    public IItem ManagerItem => Item;

    private GameObject _owner;
    public ConsumableItemManager(ConsumableItem item, GameObject owner)
    {
        Item = item;
        _owner = owner;
    }
    public void Update(float deltaTime)
    {
        if (Input.GetMouseButtonDown(0))
            Debug.Log($"Used {Item.Name}");
    }

    public void End()
    {
        
    }
}
