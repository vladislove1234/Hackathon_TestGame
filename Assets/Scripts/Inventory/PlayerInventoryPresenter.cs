using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryPresenter: MonoBehaviour
{
    public IInventory Inventory { get; private set; }
    [SerializeField] private int _quickInvetorySlots;
    [SerializeField] private Transform _playerPosition;
    [Header("All Inventory Settings")]
    [SerializeField] public GameObject InvetoryCellPrefab;
    [SerializeField] private GameObject _container;
    [SerializeField] public  GameObject InventoryContainer;
    [Header("Quick Invenotory Settings")]
    [SerializeField] public GameObject QuickInvetoryCellPrefab;
    [SerializeField] public GameObject QuickContainer;
    public List<InventoryCell> UICells { get; set; } = new List<InventoryCell>();
    [HideInInspector] public bool AllInventoryEnabled => InventoryContainer.gameObject.active;
    private void Start()
    {
        Inventory = _playerPosition.gameObject.GetComponent<IInventory>();
        Inventory.InventoryUpdated += UpdateInventory;
        Render();
    }
    private void UpdateInventory(object sender, InventoryUpdatedArgs args)
    {
        Render();
    }
    private void Render()
    {
        foreach (var child in UICells)
        {
            Destroy(child.gameObject);
        }
        UICells.Clear();
        foreach (var slot in Inventory.InventorySlots)
        {
            if(slot.Position > _quickInvetorySlots - 1)
            {
                var cell = Instantiate(InvetoryCellPrefab, _container.transform).GetComponent<UIInventoryCell>();
                cell.Render(this,slot);
                UICells.Add(cell);
            }
            else
            {
                var cell = Instantiate(QuickInvetoryCellPrefab,QuickContainer.transform).GetComponent<UIQuickInventoryCell>();   
                cell.Render(this,slot);
                UICells.Add(cell);
            }
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            InventoryContainer.gameObject.SetActive(!InventoryContainer.active);

    }


}
