using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointComponent : MonoBehaviour
{
	[SerializeField] Color _gizmosColor;

	
	private void OnDrawGizmos()
	{
		Gizmos.color = _gizmosColor;

		Gizmos.DrawSphere(transform.position, 0.5f);
	}
}
