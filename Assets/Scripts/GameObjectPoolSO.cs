using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameObjectPoolSO : ScriptableObject
{
    [SerializeField] GameObject prefab;
    public int growSize = 5;
    Queue<GameObject> pool = new Queue<GameObject>();

    private void Awake()
    {
        GrowPool();
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
