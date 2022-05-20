using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySegment : MonoBehaviour
{
	[SerializeField] int _damageValue;

	public Action<int> OnTakeDamage; 

	public void TakeDamage()
	{
		OnTakeDamage?.Invoke(_damageValue);
	}
}
