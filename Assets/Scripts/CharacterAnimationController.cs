using UnityEngine;
using UnityEngine.AI;

namespace Scripts
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(AgentLinkMover))]
    public class CharacterAnimationController : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        private NavMeshAgent _agent;
        private AgentLinkMover _linkMover;

        private const string _isRunning = "IsRunning";
        private const string _jumping = "Jumping";
        private const string _landed = "Landed";

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
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

        private void OnDestroy()
        {
            _linkMover.OnLinkStart -= JumpStart;
            _linkMover.OnLinkEnd -= JumpEnd;
        }
    }
}