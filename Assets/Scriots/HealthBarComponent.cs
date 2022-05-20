using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarComponent : MonoBehaviour
{
    
    [SerializeField] private Vector3 _offset;

	private Transform _target;

	private void LateUpdate()
	{
		transform.position = Camera.main.WorldToScreenPoint(_target.position + _offset);
	}

	public void SetTarget(Transform target)
	{
		_target = target;
	}
}
