using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInventory 
{
    List<InventorySlot> InventorySlots { get; }
    int Size { get; }
    int CurrentSlot { get; }
    int AddItem(IItem item, int count = 1);// вертає кількість айтемів, які не додало
    int RemoveItem(IItem item, int count = 1);// вертає кількість елементів, які не забарало
    int CountOfElements(IItem item);
    bool SwapSlots(InventorySlot a, InventorySlot b);
    bool DropItem(InventorySlot slot);
    public delegate void InventoryUpdatedHandler(object sender, InventoryUpdatedArgs args );
    public event InventoryUpdatedHandler InventoryUpdated;
}
