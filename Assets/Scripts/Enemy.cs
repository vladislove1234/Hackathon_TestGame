using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable
{
	public float Health { get; set; }

    public void ApplyDamage(float damage)
    {
		Health -= damage;
    }

	private void Start()
	{
		Health = 100;
	}
	private void Update()
	{
		if (Health <= 0)
		{
			Destroy(gameObject);
		}
	}
}
