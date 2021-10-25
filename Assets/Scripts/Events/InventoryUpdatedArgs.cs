using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUpdatedArgs : EventArgs
{
    public InventorySlot Slot { get; }
    public InventoryUpdatedArgs(InventorySlot slot)
    {
        Slot = slot;
    }
}
