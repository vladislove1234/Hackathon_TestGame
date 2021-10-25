using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIQuickInventoryCell : InventoryCell
{
    [SerializeField] private Text _text;
    [SerializeField] private Image _image;
    private PlayerInventoryPresenter _parent;
    private GameObject _dragCell;
    private Outline _outline;

    public bool IsActive;
    public override void OnBeginDrag(PointerEventData eventData)
    {
        if (Slot.Item != null && _parent.AllInventoryEnabled)
        {
            _image.sprite = null;
            _text.text = "";
            _dragCell = Instantiate(_parent.InvetoryCellPrefab, _parent.transform);
            _dragCell.GetComponent<UIInventoryCell>().Render(_parent, Slot);
        }
    }

    public override void OnDrag(PointerEventData eventData)
    {

        if (_dragCell != null)
            _dragCell.transform.position = Input.mousePosition;

    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        if (_dragCell == null)
            return;
        if (_dragCell.transform.position.x >
            _parent.GetComponent<RectTransform>().sizeDelta.x / 2 + _parent.transform.position.x)
        {
            _parent.Inventory.DropItem(Slot);
            Destroy(_dragCell.gameObject);
            return;
        }
        InventorySlot _closestSlot = null;
        float _closestDist = float.MaxValue;
        foreach (var child in _parent.UICells)
        {
            Debug.Log($"{child.transform.position} Position:{child.Slot == null} +" +
                $"Distance: {Vector3.Distance(_dragCell.transform.position, child.transform.position)}");
            if (Vector3.Distance(_dragCell.transform.position, child.transform.position) < _closestDist)
            {
                _closestDist = Vector3.Distance(_dragCell.transform.position, child.transform.position);
                _closestSlot = child.Slot;
            }
        }
        Debug.Log($"Closest :{_closestSlot.Position}");
        if (_closestSlot != null)
            _parent.Inventory.SwapSlots(Slot, _closestSlot);
        Destroy(_dragCell.gameObject);
        if (Slot.Item != null)
        {
            _text.text = Slot.Item.Name;
            _image.sprite = Slot.Item.InventorySprite;
        }
        _dragCell = null;
    }

    public override void Render(PlayerInventoryPresenter parent, InventorySlot slot)
    {
        _outline = GetComponent<Outline>();
        _parent = parent;
        Slot = slot;
        if (slot.Item != null)
        {
            _text.text = slot.Item.Name;
            _image.sprite = slot.Item.InventorySprite;
        }
            SetActive(_parent.Inventory.CurrentSlot == Slot.Position);
        
    }

    private void SetActive(bool state)
    {
        if (_outline != null)
            _outline.enabled = state;
    }
 
    
}
