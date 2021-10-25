using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsDataBase : MonoBehaviour
{
    private static ItemsDataBase _instance;
    public static ItemsDataBase Instance
    {
        get =>  _instance;
    }
    private void Awake()
    {
        _instance = this;
    }
    [Header("Weapon Items")]
    public List<WeaponItem> WeaponItems;
    [Header("Heal Items")]
    public List<HealItem> HealItems;
    [Header("Consumables Items")]
    public List<ConsumableItem> ConsumableItems;
    public IItem GetRadomItem()
    {
        switch ((ItemType)Random.Range(0, 3))
        {
            case ItemType.Weapon:
                var weapon = new WeaponItem(WeaponItems[Random.Range(0, WeaponItems.Count)]);
                weapon.LoadedBullets = Random.Range(0, weapon.MagazineSize);
                return weapon;
                break;
            case ItemType.Heal:
                var heal = new HealItem(HealItems[Random.Range(0, HealItems.Count)]);
                return heal;
                break;
            case ItemType.Consumables:
                var consumable = new ConsumableItem(ConsumableItems[Random.Range(0,ConsumableItems.Count)]);
                return consumable;
                break;
            default:
                throw new System.Exception("Failed to generate item");
        }
    }
}
