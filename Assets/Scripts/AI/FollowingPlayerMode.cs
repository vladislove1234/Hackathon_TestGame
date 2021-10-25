using Pathfinding;
using System;
using UnityEngine;

public class FollowingPlayerMode : RigidbodyMove
{
    protected Rigidbody2D _rigidbody2D;
    protected Transform _transform;
    protected float _speedMultiplier;
    private Path _path;
    private Seeker _seeker;
    private int _currentWaypoint;
    private Transform _target;
    private float _activateDistance;
    private float _speed;
    private float _nextWaypointDistance;
    private Vector2 _direction;

    public FollowingPlayerMode(Rigidbody2D rigidbody2D, Transform target, float speed, float nextWaypointDistance, float activeDistance)
        : base(rigidbody2D)
    {
        _rigidbody2D = rigidbody2D;
        _transform = rigidbody2D.transform;
        _target = target;
        _nextWaypointDistance = nextWaypointDistance;
        _speed = speed;
        _seeker = rigidbody2D.GetComponent<Seeker>();
        _activateDistance = activeDistance;
        _speedMultiplier = 1;
        _currentWaypoint = 0;
    }

    public void PathFollow()
    {
        UpdatePath();

        if (TargetInDistance())
        {    
            if (_path == null)
            {
                return;
            }

            if (_currentWaypoint >= _path.vectorPath.Count)
            {
                return;
            }

            Move();

            float distance = Vector2.Distance(_rigidbody2D.position, _path.vectorPath[_currentWaypoint]);
            if (distance < _nextWaypointDistance)
            {
                _currentWaypoint++;
            }
        }
    }

    private void UpdatePath()
    {
        if (TargetInDistance() && _seeker.IsDone())
        {
            _seeker.StartPath(_rigidbody2D.position, _target.position, OnPathComplete);
        }
    }

    private void OnPathComplete(Path newPath)
    {
        if (!newPath.error)
        {
            _path = newPath;
            _currentWaypoint = 0;
        }
    }

    private bool TargetInDistance()
    {
        return Vector2.Distance(_rigidbody2D.position, _target.transform.position) < _activateDistance;
    }

    public override void Move()
    {
        _direction = ((Vector2)_path.vectorPath[_currentWaypoint] - _rigidbody2D.position).normalized;
        _rigidbody2D.velocity = _direction * _speed;
        //_rigidbody2D.AddForce(_direction * _speed * Time.deltaTime);
       
    }

    public void RemoveMultiplier(float multiplier)
    {
        if (multiplier == 0)
            throw new Exception("Multiplier is 0");
        _speedMultiplier /= multiplier;
    }

    public override void ResumeMove() { }

    public void SetMultiplier(float multiplier)
    {
        if (multiplier == 0)
            throw new Exception("Multiplier is 0");
        _speedMultiplier *= multiplier;
    }

    public override void StopMove() { }
}
    


    

