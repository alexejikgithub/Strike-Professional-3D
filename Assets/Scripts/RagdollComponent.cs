using UnityEngine;

namespace Scripts
{
    public class RagdollComponent : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        private Rigidbody[] _rigidBodies;


        private void Start()
        {
            _rigidBodies = GetComponentsInChildren<Rigidbody>();
            DeavrivateRagdoll();
        }

        private void DeavrivateRagdoll()
        {
            foreach (var rb in _rigidBodies) rb.isKinematic = true;
            _animator.enabled = true;
        }

        [ContextMenu("Activate")]
        public void ActivateRagdoll()
        {
            foreach (var rb in _rigidBodies) rb.isKinematic = false;
            _animator.enabled = false;
        }
    }
}