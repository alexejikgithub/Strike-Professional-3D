using System;
using UnityEngine;

namespace Scripts
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private RagdollComponent _ragdolll;
        [SerializeField] private EnemySegment[] _segments;

        [Header("Health")] [SerializeField] private int _maxHealth;
        [SerializeField] private ObjectPoolController _HPBarPool;


        private HealthBarComponent _healthBar;
        private int _currentHealth;
        private bool _isDead;

        public bool IsDead => _isDead;

        public Action OnDeath;

        private void Awake()
        {
            _currentHealth = _maxHealth;
            _isDead = false;

            foreach (var segment in _segments) segment.OnTakeDamage += TakeDamage;
            SetHealthBar();
        }

        private void SetHealthBar()
        {
            _healthBar = _HPBarPool.GetPooledGameObject().GetComponent<HealthBarComponent>();
            _healthBar.SetTarget(transform);
            _healthBar.SetPool(_HPBarPool);
            _healthBar.RestoreFillAmount();
        }

        private void RemoveHealthBar()
        {
            _healthBar.RemoveObject();
        }

        private void TakeDamage(int damageValue)
        {
            _currentHealth -= damageValue;
            _healthBar.SetFillAmount((float) _currentHealth / _maxHealth);
            if (_currentHealth <= 0) Die();
        }

        private void Die()
        {
            _isDead = true;
            _ragdolll.ActivateRagdoll();
            OnDeath?.Invoke();
        }

        private void OnDestroy()
        {
            foreach (var segment in _segments) segment.OnTakeDamage -= TakeDamage;
        }
    }
}