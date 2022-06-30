using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class IntSO : ScriptableObject
{
    [SerializeField] int startValue;
    public int Value { get; set; }

    void Awake()
    {
        ResetValue();
    }

    public void ResetValue()
    {
        Value = startValue;
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
            ResetValue();
        }
    }
#endif
}
