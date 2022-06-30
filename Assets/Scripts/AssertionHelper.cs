using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public static class AssertionHelper
{
    public static void AssertUnityObject(Object dependency)
    {
        Assert.IsTrue(dependency, $"{dependency.GetType()} {dependency.name} is null or has not been assigned in the inspector.");
    }
}
