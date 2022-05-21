using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointComponent : MonoBehaviour
{
	[SerializeField] Color _wPColor;
	[SerializeField] private EnemyController[] _enemies;


	private bool _isWPClear;
	public bool IsWPClear => _isWPClear;

	private void Awake()
	{

		foreach (EnemyController enemy in _enemies)
		{
			enemy.OnDeath += CheckIfCleared;
		}

		CheckIfCleared();
	}

	private void Start()
	{
		
	}


	private void OnDrawGizmos()
	{
		Gizmos.color = _wPColor;
		Gizmos.DrawSphere(transform.position, 0.5f);
	}

	private void CheckIfCleared()
	{
		foreach(EnemyController enemy in _enemies)
		{
			if (!enemy.IsDead)
			{
				_isWPClear = false;
				return;
			}
		}
		_isWPClear = true;		

	}

	private void OnDestroy()
	{
		foreach (EnemyController enemy in _enemies)
		{
			enemy.OnDeath -= CheckIfCleared;
		}
	}
}
