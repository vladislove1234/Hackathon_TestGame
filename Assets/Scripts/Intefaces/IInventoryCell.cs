using System;
using UnityEngine;
using UnityEngine.EventSystems;

public interface IInventoryCell : IDragHandler, IBeginDragHandler, IEndDragHandler
{
    void Render(PlayerInventoryPresenter parent, InventorySlot slot);
}
