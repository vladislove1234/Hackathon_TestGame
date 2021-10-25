using System;
using UnityEngine;
public class HealItemManager : IItemManager
{
    public float SpeedMultiplier { get; set; }
    public HealItem Item { get; private set; }

    public string ItemText => Item.Name;
    public IItem ManagerItem => Item;

    private GameObject _owner;
    private bool IsUsing;
    private float _timer;
    Vector3 _lastPos;
    IMove _ownerMove;
    IInventory _ownerInventory;
    IHealable _ownerHealable;
    public HealItemManager(HealItem item, GameObject owner)
    {
        Item = item;
        _owner = owner;
        IsUsing = false;
        _ownerMove = owner.GetComponent<IMove>();
        _ownerInventory = owner.GetComponent<IInventory>();
        _ownerHealable = owner.GetComponent<IHealable>();
        SpeedMultiplier = 1;
        //Debug.Log($"Setted itemManager {item.Name}  {item.TimeToUse}  {item.HealCount}");
    }
    public void Update(float deltaTime)
    {
        if (!IsUsing)
        {
            if (Input.GetMouseButtonDown(0))
            {
                StartUse();
            }

        }
        else
        {
            _timer -= deltaTime;
            if (Item.StopToUse && _owner.transform.position != _lastPos)
                InterruptUse();
            if (_timer <= 0)
                StopUse();
        }
            
    }

    private void StopUse()
    {
        Debug.Log("Stop");
        IsUsing = false;
        _ownerMove.RemoveMultiplier(Item.SpeedMultiplier);
        _ownerInventory.RemoveItem(Item);
        _ownerHealable.Heal(Item.HealCount);
    }

    private void InterruptUse()
    {
        Debug.Log("Interrupt");
        IsUsing = false;
        _ownerMove.RemoveMultiplier(Item.SpeedMultiplier);
    }

    private void StartUse()
    {
        Debug.Log("Started");
        IsUsing = true;
        _timer = Item.TimeToUse * SpeedMultiplier;
        _ownerMove.SetMultiplier(Item.SpeedMultiplier);
    }

    public void End()
    {
        if(IsUsing)
            InterruptUse();
    }
}
