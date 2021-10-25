using Pathfinding;
using UnityEngine;

public class BotAI : MonoBehaviour, IDamagable, IHealth
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _activateDistance;
    [SerializeField] private float _speed;
    [SerializeField] private float _nextWaypointDistance;
    [SerializeField] private float _damage;
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _health;
    [SerializeField] private double _maxTimer;
    [SerializeField] private double _damageDistance;
    private double _timer;

    private FollowingPlayerMode _followingPlayerMode;

    public float Health => _health;

    public float MaxHealth => _maxHealth;

    private void Start()
    {
        _followingPlayerMode = new FollowingPlayerMode(GetComponent<Rigidbody2D>(), _target, _speed, _nextWaypointDistance, _activateDistance);
        _timer = _maxTimer;
    }

    private void FixedUpdate()
    {
        _followingPlayerMode.PathFollow();
        transform.rotation = Quaternion.Lerp(transform.rotation, LookTo(transform.position, _target.position), 10 * Time.deltaTime);
        _timer -= Time.deltaTime;
        if (Vector3.Distance(_target.position,transform.position) < _damageDistance && _timer <= 0)
        {
            _target.GetComponent<IDamagable>().ApplyDamage(_damage);
            _timer = _maxTimer;
        }
    }
    private Quaternion LookTo(Vector3 current, Vector3 target)
    {
        Vector2 positionOnScreen = Camera.main.WorldToViewportPoint(current);
        Vector2 mouseOnScreen = (Vector2)Camera.main.ScreenToViewportPoint(target);
        float angle = Mathf.Atan2(positionOnScreen.y - mouseOnScreen.y, positionOnScreen.x - mouseOnScreen.x) * Mathf.Rad2Deg + 180;
        return Quaternion.Euler(new Vector3(0f, 0f, angle));
    }
    private void OnCollisionEnter2D(Collision2D collision)
	{
        print("Collider");
        if (collision.gameObject.GetComponent<PlayerFacade>() != null && _timer <= 0)
        {
            _target.GetComponent<IDamagable>().ApplyDamage(_damage);
            _timer = _maxTimer;
        }
	}

    public void ApplyDamage(float damage)
    {
        Debug.Log($"Get damage from player {damage}");
        _health -= damage;
        if (_health <= 0)
        {
            var p = _target.gameObject.GetComponent<PlayerFacade>();
            p.AddPoints(10);
            Destroy(gameObject);
        }
    }
}
	
