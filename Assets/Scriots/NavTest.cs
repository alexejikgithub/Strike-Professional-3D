using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavTest : MonoBehaviour
{
	[SerializeField] private NavMeshAgent _agent;
    [SerializeField] private Transform _wp;

	private void Start()
	{
		_agent.SetDestination(_wp.position);
	}
}
