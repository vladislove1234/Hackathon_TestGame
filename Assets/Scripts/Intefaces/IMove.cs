using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMove 
{
    void Move();
    void StopMove();
    void ResumeMove();
    void SetMultiplier(float multiplier);
    void RemoveMultiplier(float multiplier);
}
