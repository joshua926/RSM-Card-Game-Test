using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuntimeSetSO<T> : ScriptableObject
{
    protected List<T> items = new List<T>();
    public int Count => items.Count;
    public T this[int index]
    {
        get => items[index];
        set => items[index] = value;
    } 

    public void Add(T item)
    {
        if (!items.Contains(item))
        {
            items.Add(item);
        }
    }

    public bool Remove(T item)
    {
        return items.Remove(item);
    }

    public void RemoveAt(int index)
    {
        items.RemoveAt(index);
    }

    public void Shuffle()
    {
        for (int i = 0; i < items.Count; i++)
        {
            int r = Random.Range(0, items.Count);
            var temp = items[r];
            items[r] = items[i];
            items[i] = temp;
        }
    }
}