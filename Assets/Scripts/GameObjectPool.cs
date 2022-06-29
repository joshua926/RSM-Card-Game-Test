using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameObjectPool : ScriptableObject
{
    [SerializeField] GameObject prefab;
    public int growSize = 5;
    Queue<GameObject> pool;

    private void Awake()
    {
        pool = new Queue<GameObject>(growSize);
        Debug.Log("SO awake");
    }

    private void OnDisable()
    {
        while(pool != null && pool.Count > 0)
        {
            Destroy(pool.Dequeue());
        }
    }

    public GameObject Get()
    {                        
        if (pool.Count <= 0)
        {
            GrowPool();
        }
        var go = pool.Dequeue();        
        go.SetActive(true);
        return go;
    }

    public void Return(GameObject gameObject)
    {        
        gameObject.SetActive(false);        
        pool.Enqueue(gameObject);
    }

    public void GrowPool()
    {        
        for (int i = 0; i < growSize; i++)
        {
            Return(Instantiate(prefab));
        }
    }
}
