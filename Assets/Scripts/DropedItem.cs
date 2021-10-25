using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropedItem : MonoBehaviour
{
    public IItem item;
    private int count;
    private void Start()
    {
        item = ItemsDataBase.Instance.GetRadomItem();
        count = item.MaxStackCount;//Random.Range(1, item.MaxStackCount);
        var spriteRenderer = GetComponent<SpriteRenderer>();
        Vector2 size = spriteRenderer.size;
        spriteRenderer.sprite = item.DropSprite;
        spriteRenderer.size = size;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var c = collision.gameObject.GetComponent<IInventory>();
        if (c != null)
        {
            count = c.AddItem(item,count);
            if (count == 0)
                Destroy(this.gameObject);
            else if (count < 0)
                throw new System.Exception("wrong value from inventory");
        }
    }
}
