using UnityEngine;

namespace Scripts
{
    public class ProjectileComponent : MonoBehaviour, IPoolObject
    {
        private ObjectPoolController _pool;


        public void SetPool(ObjectPoolController pool)
        {
            _pool = pool;
        }

        public void RemoveObject()
        {
            if (_pool != null)
                _pool.ReturnPooledGameObject(gameObject);
            else
                Destroy(gameObject);
        }


        private void OnTriggerEnter(Collider other)
        {
            var segment = other.GetComponent<EnemySegment>();
            if (segment != null) segment.TakeDamage();
            RemoveObject();
        }
    }
}