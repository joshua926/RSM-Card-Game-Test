using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace GeneralPurpose
{
    public class TransformAnimatorMB : MonoBehaviour
    {
#if UNITY_EDITOR
        [SerializeField] string editorName;
#endif
        [Tooltip("Must not be a child of the transform being animated.")]
        public Transform destination;
        [Tooltip("An event to raise upon completion of this animation. It is OK if it is missing.")]
        public EventSO onAnimationCompleted_optional;
        [Min(0)] public float duration = 1f;
        public AnimationCurve positionCurve;
        public AnimationCurve rotationCurve;
        public AnimationCurve scaleCurve;
        [System.NonSerialized] public TRS source;
        Coroutine currentAnimation;
        float startTime;

        void OnValidate()
        {
            if (destination && destination.IsChildOf(transform))
            {
                destination = null;
                Debug.LogWarning("The destination must not be a child at any level of the animated transform.");
            }
        }

        public void StartAnimation(Transform destination)
        {
            this.destination = destination;
            StartAnimation();
        }

        /// <summary>
        /// Be sure to set the destination before calling this method.
        /// </summary>
        public void StartAnimation()
        {
            AssertDependencies();
            duration = Mathf.Max(duration, 0);
            source = new TRS(transform);
            startTime = Time.realtimeSinceStartup;
            currentAnimation = StartCoroutine(Animate());
        }

        public void StopAnimation(bool shouldRaiseAnimationCompletedEvent = true)
        {
            StopCoroutine(currentAnimation);
            if (shouldRaiseAnimationCompletedEvent)
            {
                onAnimationCompleted_optional?.RaiseEvent();
            }
        }

        IEnumerator Animate()
        {
            float percentOfDuration = 0;
            while (percentOfDuration <= 1f)
            {
                percentOfDuration = (Time.realtimeSinceStartup - startTime) / duration;
                InterpolateLocalScale(percentOfDuration);
                InterpolateRotation(percentOfDuration);
                InterpolatePosition(percentOfDuration);
                yield return null;
            }
            onAnimationCompleted_optional?.RaiseEvent();
        }

        void InterpolateLocalScale(float percentOfDuration)
        {
            float t = scaleCurve.Evaluate(percentOfDuration);
            transform.localScale = Vector3.Lerp(source.scale, destination.localScale, t);
        }

        void InterpolateRotation(float percentOfDuration)
        {
            float t = rotationCurve.Evaluate(percentOfDuration);
            transform.rotation = Quaternion.Slerp(source.rotation, destination.rotation, t);
        }

        void InterpolatePosition(float percentOfDuration)
        {
            float t = positionCurve.Evaluate(percentOfDuration);
            transform.position = Vector3.Lerp(source.translation, destination.position, t);
        }

        void AssertDependencies()
        {
            Assert.IsNotNull(transform);
        }
    }
}