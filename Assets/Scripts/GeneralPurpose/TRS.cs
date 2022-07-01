using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GeneralPurpose
{
    public struct TRS
    {
        public Vector3 translation;
        public Quaternion rotation;
        public Vector3 scale;

        public TRS(Transform transform)
        {
            translation = transform.position;
            rotation = transform.rotation;
            scale = transform.localScale;
        }

        public void SetTransform(Transform transform)
        {
            transform.position = translation;
            transform.rotation = rotation;
            transform.localScale = scale;
        }
    }
}