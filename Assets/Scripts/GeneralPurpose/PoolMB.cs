using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GeneralPurpose
{
    public class PoolMB : MonoBehaviour
    {
        [SerializeField] GameObject prefab;
        public int growSize = 5;
        List<GameObject> pool = new List<GameObject>();

        private void Awake()
        {
            GrowPool();
        }

        private void OnDestroy()
        {
            for (int i = pool.Count - 1; i >= 0; i--)
            {
                var go = Dequeue();
                if (go)
                {
                    Destroy(go);
                }
            }
        }

        public GameObject Get(Transform parent = null)
        {
            if (pool.Count <= 0)
            {
                GrowPool();
            }
            var go = Dequeue();
            go.transform.SetParent(parent);
            go.SetActive(true);
            return go;
        }

        public void Return(GameObject gameObject)
        {
            gameObject.SetActive(false);
            gameObject.transform.SetParent(transform);
            pool.Add(gameObject);
        }

        public void GrowPool()
        {
            for (int i = 0; i < growSize; i++)
            {
                Return(Instantiate(prefab, transform));
            }
        }

        GameObject Dequeue()
        {
            int i = pool.Count - 1;
            var go = pool[i];
            pool[i] = null;
            pool.RemoveAt(i);
            return go;
        }
    }
}