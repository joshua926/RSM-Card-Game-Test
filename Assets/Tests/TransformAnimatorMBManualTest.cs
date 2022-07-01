using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformAnimatorMBManualTest : MonoBehaviour
{
    [SerializeField] float delayTime = 2;
    [SerializeField] TransformAnimatorMB[] animations;

    private void Start()
    {
        StartCoroutine(StartAfterDelay());
    }

    IEnumerator StartAfterDelay()
    {
        yield return new WaitForSeconds(delayTime);
        for (int i = 0; i < animations.Length; i++)
        {
            animations[i].StartAnimation();
            yield return new WaitForSeconds(animations[i].duration);
        }
    }
}
