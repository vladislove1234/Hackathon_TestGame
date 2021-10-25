using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandsItemManager : IItemManager
{
    public float SpeedMultiplier { get; set; }

    public IItem ManagerItem => null;

    public string ItemText => "";

    public void End()
    {
        
    }

    public void Update(float deltaTime)
    {
        if (Input.GetMouseButtonDown(0))
            Debug.Log("Hands Item Manager");
    }
}
