using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RigidbodyMove : IMove
{
    public Rigidbody2D _rigidbody2D;
    protected Transform _transform;
    protected float _speedMultiplier;
    public RigidbodyMove(Rigidbody2D rigidbody2D)
    {
        _rigidbody2D = rigidbody2D;
        _transform = rigidbody2D.transform;
        _speedMultiplier = 1;
    }
    public abstract void Move();

    public void RemoveMultiplier(float multiplier)
    {
        if (multiplier == 0)
            throw new Exception("Multiplier is 0");
        _speedMultiplier /= multiplier;
    }

    public abstract void ResumeMove();

    public void SetMultiplier(float multiplier)
    {
        if (multiplier == 0)
            throw new Exception("Multiplier is 0");
        _speedMultiplier *= multiplier;
    }

    public abstract void StopMove();
}