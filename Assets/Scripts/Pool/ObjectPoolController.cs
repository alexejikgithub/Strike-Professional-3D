using System.Collections.Generic;
using UnityEngine;


public class ObjectPoolController : MonoBehaviour
{
    [SerializeField] private List<GameObject> _pooledObjects;
    [SerializeField] private GameObject _objectToPool;


    private GameObject _currentElement;


    private void Start()
    {
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
            _currentElement = Instantiate(_objectToPool, transform);
            return _currentElement;
        }
    }

    public void ReturnPooledGameObject(GameObject item)
    {
        item.SetActive(false);
        _pooledObjects.Add(item);
    }
}