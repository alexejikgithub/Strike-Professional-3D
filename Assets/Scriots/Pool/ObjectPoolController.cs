using System.Collections.Generic;
using UnityEngine;


public class ObjectPoolController : MonoBehaviour
{
	[SerializeField] private List<GameObject> _pooledObjects;
	[SerializeField] private GameObject _objectToPool;
	[SerializeField] private int _amountToPool;

	private GameObject _currentElement;


	private void Start()
	{
		_pooledObjects = new List<GameObject>();

		for (int i = 0; i < _amountToPool; i++)
		{
			_currentElement = Instantiate(_objectToPool, transform.parent);
			_currentElement.SetActive(false);
			_pooledObjects.Add(_currentElement);
			_currentElement.transform.parent = this.transform;
		}
	}


	public GameObject GetPooledGameObject()
	{
		if (_pooledObjects.Count > 0)
		{
			_currentElement = _pooledObjects[0];
			_currentElement.SetActive(true);
			_pooledObjects.RemoveAt(0);
			return _currentElement;
		}
		else
		{
			_currentElement = Instantiate(_objectToPool, transform.parent);
			return _currentElement;
		}

	}

	public void ReturnPooledGameObject(GameObject item)
	{
		item.SetActive(false);
		_pooledObjects.Add(item);
	}


}
