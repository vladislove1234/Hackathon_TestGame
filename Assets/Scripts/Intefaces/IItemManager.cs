using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItemManager
{
    void Update(float deltaTime);
    void End();
    float SpeedMultiplier { get; set; }
    string ItemText { get; }
    IItem ManagerItem { get; }
}
