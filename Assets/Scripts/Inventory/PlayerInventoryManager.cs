using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerInventoryManager : MonoBehaviour, IInventory, IItemUser
{
    [SerializeField] private int _quickInventoryItems;
    [SerializeField] private int _inventorySize;
    [SerializeField] private GameObject _dropItemPrefab;
    public int QuickInventoryItems => _quickInventoryItems;
    public int Size => _inventorySize;
    [SerializeField]private int _currentItem;
    private GameObject _currentItemInHand;
    private List<InventorySlot> _inventorySlots;

    public IItemManager CurrentItemManager => _currentItemManager;

    private IItemManager _currentItemManager;

    public event IInventory.InventoryUpdatedHandler InventoryUpdated;

    public List<InventorySlot> InventorySlots => _inventorySlots;

    public int CurrentSlot => _currentItem;
    
    public void Awake()
    {

        _inventorySlots = new List<InventorySlot>(_inventorySize);
        FillInventory();
        UpdateInventory();
    }

    private void FillInventory()
    {
        for(int i = 0; i < _inventorySize; i++)
        {
            _inventorySlots.Add(new InventorySlot() { Position = i});
        }
    }

    public int AddItem(IItem item, int count = 1)//вертає скільки предметів не додало
    {
        var slots = _inventorySlots.Where(x => x.Item == item && x.CanPutCount > 0).ToList();
            foreach(var slot in slots)
            {
                if (slot.CanPutCount >= count)
                {
                    slot.Count += count;
                    Debug.Log($"Added item to existing slot in inventory in slot{slot.Position}");
                    UpdateInventory();
                    return 0;
                }
                else
                {
                    count -= slot.CanPutCount;
                    slot.Count = item.MaxStackCount;
                }
            }
        var freeSlots = _inventorySlots.Where(x => x.Item == null).ToList();
        foreach(var freeSlot in freeSlots)
        {
            freeSlot.Item = item;
            if(item.MaxStackCount >= count)
            {
                Debug.Log($"Added item to empty slot in inventory in slot{freeSlot.Position}");
                freeSlot.Count += count;
                UpdateInventory();
                return 0;
            }
            else
            {
                count -= freeSlot.CanPutCount;
                freeSlot.Count = item.MaxStackCount;
            }
        }
        UpdateInventory();
        return count;
    }

    private void UpdateInventory()
    {
        var _newItem = InventorySlots[_currentItem].Item;
        if(_currentItemManager != null)
        {
            if (!EqualItems(_currentItemManager.ManagerItem, _newItem))
            {
                Debug.Log("End invoked!");
                _currentItemManager.End();
                _currentItemManager = InventorySlots[_currentItem].Item == null ? new HandsItemManager() : InventorySlots[_currentItem].Item.GetItemManager(gameObject);
            }
        }
        else
            _currentItemManager = InventorySlots[_currentItem].Item == null ? new HandsItemManager() : InventorySlots[_currentItem].Item.GetItemManager(gameObject);
        if (InventoryUpdated != null)
            InventoryUpdated.Invoke(this,new InventoryUpdatedArgs(null));
    }

    public int CountOfElements(IItem item)
    {
        int count = 0;
        _inventorySlots.Where(x =>x.Item != null && x.Item.Id == item.Id && x.Item.Type == item.Type).ToList().ForEach(x => count += x.Count);
        return count;
    }

    public int RemoveItem(IItem item, int count = 1)
    {
        var slots = _inventorySlots.Where(x => x.Item != null && x.Item.Id == item.Id && x.Item.Type == item.Type).ToList();
        foreach(var slot in slots)
        {
            if(slot.Count > count)
            {
                slot.Count -= count;
                return 0;
            }
            else
            {
                count -= slot.Count;
                slot.Clear();
            }
        }
        UpdateInventory();
        return count;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            SwapItem();
        if (_currentItemManager != null)
            _currentItemManager.Update(Time.deltaTime);
    }

    public void LockUsage()
    {
        throw new NotImplementedException();
    }

    public void UnlockUasage()
    {
        throw new NotImplementedException();
    }

    public void SetMultiplier()
    {
        
    }

    public void SwapItem()
    {
        _currentItem++;
        if (_currentItem > _quickInventoryItems - 1)
            _currentItem = 0;
        UpdateInventory();
    }

    public bool SetCurrentItem(IItem item)
    {
        return true;
    }

    public bool DropItem(InventorySlot slot)
    {
        Debug.Log($"Dropped item from slot at position {slot.Position}");
        slot.Clear();
        UpdateInventory();
        return true;
    }

    public bool SwapSlots(InventorySlot a, InventorySlot b)
    {
        int posA = a.Position;
        a.Position = b.Position;
        b.Position = posA;
        InventorySlots.Sort((a,b) => a.Position.CompareTo(b.Position));
        Debug.Log($"Swapped items at position {a.Position} and {b.Position}");
        UpdateInventory();
        return true;
    }
    private bool EqualItems(IItem a, IItem b)
    {
        if(a != null && b != null)
            return a.Id == b.Id && a.Type == b.Type;
        return false;
    }
}
