using Scripts.Input;
using Scripts.Observer;
using UnityEngine;

namespace Scripts
{
    public class FireController : MonoBehaviour, IObserver<bool>
    {
        [SerializeField] private float _projectileSpeed;
        [SerializeField] private InteractableCanvasObject _input;
        [SerializeField] private Transform _shootPositionl;
        [SerializeField] private ObjectPoolController _pool;


        private bool _isGamplayOn;


        private void Awake()
        {
            _input.OnPointerDownEvent += OnTap;
        }

        private void OnDestroy()
        {
            _input.OnPointerDownEvent -= OnTap;
        }

        public void UpdateObservableData(bool gameplayStatus)
        {
            _isGamplayOn = gameplayStatus;
        }


        private void OnTap(Vector3 position)
        {
            if (_isGamplayOn) ShootProjectile();
        }

        private void ShootProjectile()
        {
            var ray = Camera.main.ScreenPointToRay(UnityEngine.Input.mousePosition);
            RaycastHit hitInfo;
            Physics.Raycast(ray, out hitInfo);
            var projectile = _pool.GetPooledGameObject().GetComponent<ProjectileComponent>();
            if (projectile != null)
            {
                projectile.SetPool(_pool);
                projectile.transform.position = _shootPositionl.position;
                projectile.transform.rotation = Quaternion.LookRotation(hitInfo.point);
                projectile.GetComponent<Rigidbody>().velocity =
                    (hitInfo.point - _shootPositionl.position).normalized * _projectileSpeed;
            }
        }
    }
}