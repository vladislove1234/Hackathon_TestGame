using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFacade : MonoBehaviour, IHealable, IHealth, IHandler,IDamagable
{
    public int Points { get; private set; }

    private IMove _inputMove;
    [SerializeField] private float _maxHealth;
    public GameObject Hand;
    private float _health;
    private GameObject currentItemInHand;
    [SerializeField] GameObject _handItemPrefab;

    public float Health => _health;

    public float MaxHealth => _maxHealth;

    public void AddPoints(int points)
    {
        Points += points;
        if(Points > 100)
        {
            _inputMove.StopMove();
            GameController.Instance.ShowWinScreen();
        }
    }
    public void Heal(float healCount)
    {
        if (_health + healCount > _maxHealth)
            _health = _maxHealth;
        else
            _health += healCount;
        Debug.Log($"Healed by{healCount}");
    }

    // private InventoryManager _inventoryManager;
    void Start()
    {
        _health = MaxHealth;
        _inputMove = GetComponent<IMove>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SetSprite(Sprite sprite)
    {
        currentItemInHand = Instantiate(_handItemPrefab, Hand.transform);
        currentItemInHand.GetComponent<SpriteRenderer>().sprite = sprite;
    }

    public void RemoveSprite()
    {
        if(currentItemInHand != null)
        Destroy(currentItemInHand.gameObject);
    }

    public void ApplyDamage(float damage)
    {
        Debug.Log($"Get damage {damage}");
        _health -= damage;
        if (_health <= 0)
        {
            GameController.Instance.ShowLoseScreen();
            _inputMove.StopMove();
        }

            
    }
}
