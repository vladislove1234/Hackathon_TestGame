using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponItemManager : IItemManager
{
    private GameObject _owner;
    private IInventory _ownerInventory;
    private Animator _animator;
    private AudioSource _audioSource;
    public WeaponItemManager(WeaponItem item, GameObject owner)
    {
        _owner = owner;
        Item = item;
        _ownerInventory = _owner.GetComponent<IInventory>();
        if (_ownerInventory == null)
            throw new System.Exception("Failed to get inventory of player");
        _timer = 0;
        _animator = _owner.GetComponent<Animator>();
        SetAnim(true);
        _audioSource = _owner.GetComponent<AudioSource>();
        UpdateText();
    }
    public IItem ManagerItem => Item;
    public WeaponItem Item { get; private set; }
    public float SpeedMultiplier { get; set; }
    public int CountOfBullets => GetCountOfBullets();

    public string ItemText => _itemText;

    private float _timer;
    private string _itemText;
    private void MakeSound(AudioClip clip)
    {
        if(_audioSource != null)
        {
            _audioSource.clip = clip;
            _audioSource.Play();
        }
    }
    private int GetCountOfBullets()
    {
        int count = 0;
        Item.Bullets.ForEach(x => count += _ownerInventory.CountOfElements(x));
        return count;
    }
    private void SetAnim(bool state)
    {
        switch (Item.WeaponType)
        {
            case WeaponType.Pistol:
                _animator.SetBool("Hand_pistol", state);
                break;
            case WeaponType.Weapon:
                _animator.SetBool("Hand_weapon", state);
                break;
        }
        if(state)
        _owner.GetComponent<IHandler>().SetSprite(Item.UseSprite);
        else
            _owner.GetComponent<IHandler>().RemoveSprite();
    }
    public void Update(float deltaTime)
    {
        
        if (((Input.GetMouseButton(0) && Item.IsAuto) || (Input.GetMouseButtonDown(0) && !Item.IsAuto)) && Item.LoadedBullets > 0 && _timer <= 0)
        {
            MakeSound(Item.ShootSound);
            var hits = Physics2D.RaycastAll(_owner.transform.position, _owner.transform.right, 1000) ;
            Debug.Log("Shoot");
            _timer = Item.ShootingDelay;
            Item.LoadedBullets--;
            foreach (var hit in hits)
            {
                Debug.Log(hit.transform.gameObject.name);
                if (hit.collider != null && hit.collider.gameObject != _owner)
                {
                    var enemy = hit.collider.GetComponent<IDamagable>();
                    if(enemy != null)
                    {
                        enemy.ApplyDamage(Item.Damage);
                        Debug.Log("hited");
                        
                        break;
                    }
                
                }
            }
            UpdateText();
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            if(Reload())
            {
                MakeSound(Item.ReloadSound);
                _timer = Item.ReloadingTime;
                UpdateText();
                //play anim;
            }
        }
		else
        {
            if(_timer > 0)
            _timer -= deltaTime;
        }
            
    }

    private bool Reload()
    {
        if (Item.LoadedBullets == Item.MagazineSize)
            return false;
        
        if(CountOfBullets > 0)
        {
            foreach(var bullet in Item.Bullets)
            {
                int count = _ownerInventory.RemoveItem(bullet, Item.MagazineSize - Item.LoadedBullets);
                if(count != Item.MagazineSize - Item.LoadedBullets)
                {
                    Item.LoadedBullets = Item.MagazineSize - count;
                    return true;
                }
            }
        }
        return false;
    }
    private void UpdateText()
    {
        _itemText = $"{Item.Name}  {Item.LoadedBullets}/{CountOfBullets}";
    }

    public void End()
    {
        SetAnim(false);
    }
}
