using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollComponent : MonoBehaviour
{
	[SerializeField] private Animator _animator;
	private Rigidbody[] _rigidBodies;
	


	private void Start()
	{
		_rigidBodies = GetComponentsInChildren<Rigidbody>();
		DeavrivateRagdoll();
	}

	public void DeavrivateRagdoll()
	{
		foreach(Rigidbody rb in _rigidBodies)
		{
			rb.isKinematic = true;
		}
		_animator.enabled = true;
	}

	[ContextMenu("Activate")]
	public void ActivateRagdoll()
	{
		foreach (Rigidbody rb in _rigidBodies)
		{
			rb.isKinematic = false;
		}
		_animator.enabled = false;
	}
}
