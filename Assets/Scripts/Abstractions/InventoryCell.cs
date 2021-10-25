using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryCell : MonoBehaviour, IInventoryCell
{
    public InventorySlot Slot;
    public virtual void OnBeginDrag(PointerEventData eventData)
    {
        throw new NotImplementedException();
    }

    public virtual void OnDrag(PointerEventData eventData)
    {
        throw new NotImplementedException();
    }

    public virtual void OnEndDrag(PointerEventData eventData)
    {
        throw new NotImplementedException();
    }

    public virtual void Render(PlayerInventoryPresenter parent, InventorySlot slot)
    {
        throw new NotImplementedException();
    }
}
