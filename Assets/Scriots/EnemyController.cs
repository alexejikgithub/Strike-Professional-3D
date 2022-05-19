using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

	[SerializeField] private int _maxHealth;

	private int _currentHealth;
	private bool _isDead;

	public bool IsDead => _isDead;

	public Action OnDeath;

	private void Awake()
	{
		_currentHealth = _maxHealth;
		_isDead = false;
	}


	private void TakeDamage(int damageValue)
	{
		_currentHealth -= damageValue;

		if(_currentHealth<= 0)
		{
			Die();
		}
	}

	private void Die()
	{
		_isDead = true;
		OnDeath?.Invoke();

	}

}
