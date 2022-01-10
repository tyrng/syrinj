using UnityEngine;

namespace Syrinj
{
    public abstract class MonoClonable: MonoBehaviour
    {
        public static Object Clone(Object original)
            => ObjectExtensions.Clone(original);
        
        public static T Clone<T>(T original) where T : Object
            => ObjectExtensions.Clone(original);

        public static Object Clone(Object original, Transform parent)
            => ObjectExtensions.Clone(original, parent);

        public static T Clone<T>(T original, Transform parent) where T : Object
            => ObjectExtensions.Clone(original, parent);

        public static Object Clone(
            Object original,
            Transform parent,
            bool instantiateInWorldSpace)
            => ObjectExtensions.Clone(original, parent, instantiateInWorldSpace);

        public static T Clone<T>(
            T original,
            Transform parent,
            bool instantiateInWorldSpace) where T : Object
            => ObjectExtensions.Clone(original, parent, instantiateInWorldSpace);

        public static Object Clone(Object original, Vector3 position, Quaternion rotation)
            => ObjectExtensions.Clone(original, position, rotation);

        public static T Clone<T>(T original, Vector3 position, Quaternion rotation) where T : Object
            => ObjectExtensions.Clone(original, position, rotation);

        public static Object Clone(
            Object original,
            Vector3 position,
            Quaternion rotation,
            Transform parent)
            => ObjectExtensions.Clone(original, position, rotation, parent);

        public static T Clone<T>(
            T original,
            Vector3 position,
            Quaternion rotation,
            Transform parent) where T : Object
            => ObjectExtensions.Clone(original, position, rotation, parent);
    }
}