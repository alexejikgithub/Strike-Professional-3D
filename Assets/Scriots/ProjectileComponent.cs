using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileComponent : MonoBehaviour,IPoolObject
{
	private ObjectPoolController _pool;



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


	private void OnTriggerEnter(Collider other)
	{

		Debug.Log(other.name);
		EnemySegment segment = other.GetComponent<EnemySegment>();
		if(segment!=null)
		{
			
			segment.TakeDamage();
		}
		RemoveObject();
		
	}


}
