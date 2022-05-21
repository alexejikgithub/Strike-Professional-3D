using System;
using UnityEngine;

namespace Scripts
{
    public class EnemySegment : MonoBehaviour
    {
        [SerializeField] private int _damageValue;

        public Action<int> OnTakeDamage;

        public void TakeDamage()
        {
            OnTakeDamage?.Invoke(_damageValue);
        }
    }
}