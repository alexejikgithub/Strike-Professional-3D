using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

	
	[SerializeField] private RagdollComponent _ragdolll;
	[SerializeField] private EnemySegment[] _segments;

	[Header("Health")]
	[SerializeField] private int _maxHealth;
	[SerializeField] private Canvas _hUDCanvas;
	[SerializeField] private HealthBarComponent _healthBarPrefab;


	private HealthBarComponent _healthBar;
	private int _currentHealth;
	private bool _isDead;

	public bool IsDead => _isDead;

	public Action OnDeath;

	private void Awake()
	{
		_currentHealth = _maxHealth;
		_isDead = false;

		foreach(EnemySegment segment in _segments)
		{
			segment.OnTakeDamage += TakeDamage;
		}
		SetHealthBar();
	}
	
	private void SetHealthBar()
	{
		_healthBar =Instantiate(_healthBarPrefab);
		_healthBar.transform.parent = _hUDCanvas.transform;
		_healthBar.SetTarget(this.transform);
	}
	
	public void TakeDamage(int damageValue)
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
		_ragdolll.ActivateRagdoll();
		OnDeath?.Invoke();

	}
	private void OnDestroy()
	{
		foreach (EnemySegment segment in _segments)
		{
			segment.OnTakeDamage -= TakeDamage;
		}
	}

}
