using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class MainCharacterController : MonoBehaviour
{

	[SerializeField] private WayPointComponent[] _wayPoints;

	private NavMeshAgent _agent;

	private int _wayPointIndex = 0;

	private void Awake()
	{
		_agent = GetComponent<NavMeshAgent>();
	}

	private void Start()
	{
		SetWaypoint(_wayPoints[_wayPointIndex]);
	}

	private void Update()
	{
		if (!_agent.pathPending && _agent.remainingDistance < 0.5f && _wayPoints[_wayPointIndex].IsWPClear)
		{
			SetNextWayPoint();
		}
	}


	private void SetWaypoint(WayPointComponent waypoint)
	{
		_agent.SetDestination(waypoint.transform.position);
	}

	private void SetNextWayPoint()
	{
		if (_wayPointIndex < _wayPoints.Length - 1)
		{

			_wayPointIndex++;
			SetWaypoint(_wayPoints[_wayPointIndex]);
		}
	}



	private void OnDrawGizmos()
	{
		for (int i = 0; i < _wayPoints.Length - 1; i++)
		{
			DrawArrow(_wayPoints[i].transform.position, _wayPoints[i + 1].transform.position);
		}

	}

	private void DrawArrow(Vector3 start, Vector3 finish)
	{
		Gizmos.color = Color.red;
		Gizmos.DrawLine(start, finish);
		Vector3 direction = (finish - start).normalized;
		Vector3 right = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 + 20f, 0) * new Vector3(0, 0, 1);
		Vector3 left = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 - 20f, 0) * new Vector3(0, 0, 1);
		Gizmos.DrawRay(finish, right * 1f);
		Gizmos.DrawRay(finish, left * 1f);
	}
}
