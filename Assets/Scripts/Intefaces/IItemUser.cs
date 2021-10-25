using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItemUser 
{
    IItemManager CurrentItemManager { get; }
    void LockUsage();
    void UnlockUasage();
    void SetMultiplier();
    void SwapItem();
    bool SetCurrentItem(IItem item);
}
