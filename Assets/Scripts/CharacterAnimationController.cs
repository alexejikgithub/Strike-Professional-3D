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
        private static readonly int IsRunning = Animator.StringToHash("IsRunning");
        private static readonly int Jumping = Animator.StringToHash("Jumping");
        private static readonly int Landed = Animator.StringToHash("Landed");
        

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
            _animator.SetBool(IsRunning, _agent.velocity.magnitude > 0.5f);
        }

        private void JumpStart()
        {
            _animator.SetTrigger(Jumping);
        }

        private void JumpEnd()
        {
            _animator.SetTrigger(Landed);
        }

        private void OnDestroy()
        {
            _linkMover.OnLinkStart -= JumpStart;
            _linkMover.OnLinkEnd -= JumpEnd;
        }
    }
}