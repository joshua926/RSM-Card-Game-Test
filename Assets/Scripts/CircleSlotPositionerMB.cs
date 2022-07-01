using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class CircleSlotPositionerMB : MonoBehaviour
{
    public float radius = 2000f;
    public float itemArcLengthDegrees = 5.3f;
    [Tooltip("Counter clockwise. 0 degrees is 3 o'clock. 90 degrees is 12 o'clock.")]
    public float arcCenterDegrees = 90f;
    public bool alignChildrenRotation;
    [Range(0, 1)] public float rotationAlignmentStrength = 1f;
    public EventRaiser raiseEventsOnCircleChanged;
    public int SlotCount => transform.childCount;

    void OnValidate()
    {
        SetChildrenTransformsOnCircle(shouldRaiseEventOnCompletion: false);
    }

    void Start()
    {
        SetChildrenTransformsOnCircle();
    }

    public Transform GetSlot(int index)
    {
        return transform.GetChild(index);
    }

    public void AddSlot()
    {
        var child = new GameObject("Circle Slot");
        child.transform.SetParent(transform);
        child.transform.SetAsLastSibling();
        SetChildrenTransformsOnCircle();
    }

    public void RemoveSlotAt(int index)
    {
        var child = transform.GetChild(index);
        child.SetParent(null);
        Destroy(child);
        SetChildrenTransformsOnCircle();
    }

    public void SetChildrenTransformsOnCircle(bool shouldRaiseEventOnCompletion = true)
    {
        int itemCount = transform.childCount;
        float arcLengthDegrees = itemArcLengthDegrees * (itemCount - 1);
        float startAngle = arcCenterDegrees - (arcLengthDegrees / 2);
        float endAngle = arcCenterDegrees + (arcLengthDegrees / 2);
        for (int i = 0; i < itemCount; i++)
        {
            float percent = (float)i / (itemCount - 1);
            float degrees = Mathf.Lerp(startAngle, endAngle, percent);
            float radians = degrees * Mathf.Deg2Rad;
            Vector3 direction = new Vector3(
                Mathf.Cos(radians),
                Mathf.Sin(radians),
                0);
            var child = transform.GetChild(i);
            child.localPosition = direction * radius;
            if (alignChildrenRotation)
            {
                child.localRotation = Quaternion.Euler(0, 0, (degrees - arcCenterDegrees) * rotationAlignmentStrength);
            }
        }
        if (shouldRaiseEventOnCompletion)
        {
            raiseEventsOnCircleChanged?.RaiseEvents();
        }
    }
}
