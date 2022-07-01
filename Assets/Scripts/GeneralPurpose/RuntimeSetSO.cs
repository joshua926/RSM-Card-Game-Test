using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GeneralPurpose
{
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

        public T GetAndRemoveAt(int index)
        {
            T item = items[index];
            items.RemoveAt(index);
            return item;
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

#if UNITY_EDITOR
        // Unfortunately ScriptableObjects do not necessarily get reset (even their Properties)
        // when switching to and from play mode in the editor so this should handle that.    
        private void OnEnable()
        {
            UnityEditor.EditorApplication.playModeStateChanged += ResetAfterPlay;
        }

        void ResetAfterPlay(UnityEditor.PlayModeStateChange state)
        {
            if (this)
            {
                items.Clear();
            }
        }
#endif
    }
}