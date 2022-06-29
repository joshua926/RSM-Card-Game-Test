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
        Value = startValue;
    }
}
