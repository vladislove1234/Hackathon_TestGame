using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputMove : MonoBehaviour, IMove
{
    private float _xAxis;
    private float _yAxis;
    public bool IsMoving { get; private set; }
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _speedOfLook;
    private Animator _animator;
    public float SpeedMultiplier { get; private set; }
    private Rigidbody2D _rigidbody2D;

    // Start is called before the first frame update
    public void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        SpeedMultiplier = 1;
        IsMoving = true; 
    }
    public void Update()
    {
        if (IsMoving) {
        Move();
        LookAtMouse();

        }
    }
    public void Move()
    {
        _xAxis = Input.GetAxis("Horizontal");
        _yAxis = Input.GetAxis("Vertical");
        var vectorOfMove = new Vector2(_xAxis, _yAxis);
            _animator.SetBool("Is_moving", _xAxis != 0 || _yAxis != 0);

        _rigidbody2D.velocity = vectorOfMove * _moveSpeed * SpeedMultiplier;
    }
    private void LookAtMouse()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, LookTo(transform.position, Input.mousePosition), _speedOfLook * Time.deltaTime );
    }

    private Quaternion LookTo(Vector3 current,Vector3 target)
    {
        Vector2 positionOnScreen = Camera.main.WorldToViewportPoint(current);
        Vector2 mouseOnScreen = (Vector2)Camera.main.ScreenToViewportPoint(target);
        float angle = Mathf.Atan2(positionOnScreen.y - mouseOnScreen.y, positionOnScreen.x - mouseOnScreen.x) * Mathf.Rad2Deg + 180;
        return Quaternion.Euler(new Vector3(0f, 0f, angle));
    }
    private float getAngle(Vector2 current, Vector2 target)
    {
        return Mathf.Atan2(current.y - target.y, current.x - target.x) * Mathf.Rad2Deg - 90;
    }
    public void StopMove()
    {
        IsMoving = false;
    }

    public void ResumeMove()
    {
        IsMoving = true;
    }

    public void SetMultiplier(float multiplier)
    {
        if (multiplier != 0)
            SpeedMultiplier *= multiplier;
    }

    public void RemoveMultiplier(float multiplier)
    {
        if (multiplier != 0)
            SpeedMultiplier /= multiplier;
    }
}
