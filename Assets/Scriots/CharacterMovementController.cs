using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof (NavMeshAgent))]
[RequireComponent(typeof (Animator))]
[RequireComponent(typeof (AgentLinkMover))]
public class CharacterMovementController : MonoBehaviour
{
	
    [SerializeField] private WayPointComponent[] _wayPoints;

	private NavMeshAgent _agent;
	private Animator _animator;
	private AgentLinkMover _linkMover;

	private const string _isRunning = "IsRunning";
	private const string _jumping = "Jumping";
	private const string _landed = "Landed";

	private void Awake()
	{
		_agent = GetComponent<NavMeshAgent>();
		_animator = GetComponent<Animator>();
		_linkMover = GetComponent<AgentLinkMover>();

		_linkMover.OnLinkStart += JumpStart;
		_linkMover.OnLinkEnd += JumpEnd;
	}

	private void Start()
	{
		
	}

	private void Update()
	{
		SetRunningAnimation();
	}

	private void SetRunningAnimation()
	{
		_animator.SetBool(_isRunning, _agent.velocity.magnitude > 0.5f);
	}

	private void JumpStart()
	{
		_animator.SetTrigger(_jumping);
	}
	private void JumpEnd()
	{
		_animator.SetTrigger(_landed);
	}
	private void SetWaypoint(WayPointComponent waypoint)
	{
		_agent.SetDestination(waypoint.transform.position);
	}

	private void OnDrawGizmos()
	{
		for (int i = 0; i < _wayPoints.Length - 1; i++)
		{
			Gizmos.color = Color.red;
			Gizmos.DrawLine(_wayPoints[i].transform.position, _wayPoints[i + 1].transform.position);
			Vector3 direction = (_wayPoints[i + 1].transform.position - _wayPoints[i].transform.position).normalized;
			Vector3 right = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 + 20f, 0) * new Vector3(0, 0, 1);
			Vector3 left = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 - 20f, 0) * new Vector3(0, 0, 1);
			Gizmos.DrawRay(_wayPoints[i + 1].transform.position, right * 1f);
			Gizmos.DrawRay(_wayPoints[i + 1].transform.position, left * 1f);
		}

	}

	private void OnDestroy()
	{
		_linkMover.OnLinkStart -= JumpStart;
		_linkMover.OnLinkEnd -= JumpEnd;
	}


}
