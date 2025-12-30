using System.Collections.Generic;
using UnityEngine;

namespace Features.PoolObject
{
    public class PoolMono<T> where T : MonoBehaviour
    {
        public T prefab { get; }
        public bool autoExpand { get; set; } = true;
        public Transform container { get; }
        
        private List<T> pool;

        public PoolMono(T prefab, int count)
        {
            this.prefab = prefab;
            this.container = null;

            this.CreatePool(count);
        }

        public PoolMono(T prefab, int count, Transform container)
        {
            this.prefab = prefab;
            this.container = container;
            
            this.CreatePool(count);
        }
        
        private void CreatePool(int count)
        {
            this.pool = new List<T>();
            
            for  (int i = 0; i < count; i++)
            {
                this.CreateObject();
            }
        }
        
        private T CreateObject(bool isActiveByDefault = false)
        {
            var createdObject = Object.Instantiate(this.prefab, this.container);
            createdObject.gameObject.SetActive(isActiveByDefault);
            this.pool.Add(createdObject);
            return createdObject;
        }
        
        public bool HasFreeElement(out T element)
        {
            pool.RemoveAll(obj => !obj);
            foreach (var poolObject in this.pool)
            {
                if (!poolObject.gameObject.activeInHierarchy)
                {
                    element = poolObject;
                    return true;
                }
            }

            element = null;
            return false;
        }
        
        public T GetFreeElement()
        {
            if (this.HasFreeElement(out var element))
            {
                return element;
            }

            if (this.autoExpand)
            {
                return this.CreateObject();
            }

            throw new System.Exception($"No free elements in pool of type {typeof(T)}");
        }
    }
}