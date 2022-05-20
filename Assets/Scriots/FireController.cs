using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour
{

	[SerializeField] private InteractableCanvasObject _input;
	[SerializeField] private float _projectileSpeed;
	[SerializeField] private Transform _shootPositionl;
	[SerializeField] private ObjectPoolController _pool;

	private void Awake()
	{
		_input.OnPointerDownEvent += OnTap;
	}


	private void OnTap(Vector3 position)
	{
		ShootProjectile();
	}

	private void ShootProjectile()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hitInfo;
		Physics.Raycast(ray, out hitInfo);
		ProjectileComponent projectile = _pool.GetPooledGameObject().GetComponent<ProjectileComponent>();
		if(projectile!=null)
		{
			projectile.SetPool(_pool);
			projectile.transform.position = _shootPositionl.position;
			projectile.transform.rotation = Quaternion.LookRotation(hitInfo.point);
			projectile.GetComponent<Rigidbody>().velocity = (hitInfo.point - _shootPositionl.position).normalized * _projectileSpeed;
		}

	}

}
