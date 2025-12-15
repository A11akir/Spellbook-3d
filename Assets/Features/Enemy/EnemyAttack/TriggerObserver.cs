using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Features.Enemy.EnemyAttack
{
    public class TriggerObserver : MonoBehaviour
    {
        public event Action<Collider> TriggerEnter;
        public event Action<Collider> TriggerExit;

        public bool IsDictanceCanAttack;
        
        private void OnTriggerEnter(Collider other)
        {
            TriggerEnter?.Invoke(other);
            IsDictanceCanAttack = true;
        }

        private void OnTriggerExit(Collider other)
        {
            TriggerExit?.Invoke(other);
            IsDictanceCanAttack = false;
        }
    }
}