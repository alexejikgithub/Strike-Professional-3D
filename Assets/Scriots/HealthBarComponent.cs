using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarComponent : MonoBehaviour, IPoolObject
{
    
    [SerializeField] private Vector3 _offset;
	[SerializeField] private Image _bar;

	private Transform _target;
	private ObjectPoolController _pool;

	private void LateUpdate()
	{
		transform.position = Camera.main.WorldToScreenPoint(_target.position + _offset);
		transform.localScale = Vector3.one/Vector3.Distance(_target.position, Camera.main.transform.position)*10;
	}

	public void SetTarget(Transform target)
	{
		_target = target;
	}

	public void SetFillAmount(float amount)
	{
		_bar.fillAmount = amount;
	}

	public void SetPool(ObjectPoolController pool)
	{
		_pool = pool;
	}


	public void RemoveObject()
	{
		if (_pool != null)
		{
			_pool.ReturnPooledGameObject(this.gameObject);
		}
		else
		{
			Destroy(this.gameObject);
		}
	}
}
