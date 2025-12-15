using Features.Hero.HeroStats.HeroHP;
using UnityEngine;

namespace Features.AbstractMinion
{
    public class CanvasMinionSystem : MonoBehaviour
    {
        private GameObject _enemyPrefab;
        
        public void Init(GameObject prefab, Health health)
        {
            _enemyPrefab =  prefab;
            transform.localRotation = Camera.main.transform.rotation;
            
            health.OnDeath += () => Destroy(gameObject);
        }
        
        private void Update()
        {
            transform.position = _enemyPrefab.transform.position;
        }
    }
}