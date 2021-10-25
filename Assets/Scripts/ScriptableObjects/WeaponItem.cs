using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "new Weapon", menuName = "Items/Weapon")]
public class WeaponItem : ScriptableObject, IItem
{
    public WeaponItem(WeaponItem item)
    {
        _id = item.Id;
        _name = item.Name;
        _description = item.Description;
        _inventorySprite = item.InventorySprite;
        _dropSprite = item.DropSprite;
        _useSprite = item.UseSprite;

        _bullets = item.Bullets;
        _isAuto = item.IsAuto;
        _shootingDelay = item._shootingDelay;
        _magazineSize = item.MagazineSize;
        _damage = item.Damage;
        _reloadingTime = item.ReloadingTime;
        LoadedBullets = item.LoadedBullets;
        _weaponType = item.WeaponType;
        _reloadSound = item.ReloadSound;
        _shootSound = item.ShootSound;
    }
    public int Id => _id;
    public Sprite InventorySprite => _inventorySprite;
    public Sprite DropSprite => _dropSprite;
    public Sprite UseSprite => _useSprite;
    public ItemType Type => ItemType.Weapon;
    public string Name => _name;
    public string Description => _description;
    public int MaxStackCount => 1;
    public AudioClip ReloadSound => _reloadSound;
    public AudioClip ShootSound => _shootSound;

    [SerializeField] private int _id;
    [SerializeField] private string _name;
    [SerializeField] [TextArea(1, 3)] private string _description;
    [SerializeField] private Sprite _inventorySprite;
    [SerializeField] private Sprite _dropSprite;
    [SerializeField] private Sprite _useSprite;
    [Header("Weapon settings")]
    [SerializeField] private List<ConsumableItem> _bullets;
    [SerializeField] private bool _isAuto;
    [SerializeField] private float _shootingDelay;
    [SerializeField] private int _magazineSize;
    [SerializeField] private float _damage;
    [SerializeField] private float _reloadingTime;
    [SerializeField] private WeaponType _weaponType;
    [SerializeField] AudioClip _reloadSound;
    [SerializeField] AudioClip _shootSound;

    public float ReloadingTime => _reloadingTime;
    public List<ConsumableItem> Bullets => _bullets;
    public bool IsAuto => _isAuto;
    public float ShootingDelay => _shootingDelay;
    public int MagazineSize => _magazineSize;
    public float Damage =>_damage;
    public WeaponType WeaponType => _weaponType;

    [HideInInspector]public int LoadedBullets;
    public IItemManager GetItemManager(GameObject owner)
    {
        return new WeaponItemManager(this, owner);
    }
}
public enum WeaponType
{
    Pistol,
    Weapon
}