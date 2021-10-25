using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlot
{
    public IItem Item { get; set; }
    public int Count { get; set; }
    public int Position { get; set; }
    public int CanPutCount => Item.MaxStackCount - Count;
    public void Clear()
    {
        Item = null;
        Count = 0;
    }
}
