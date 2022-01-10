using System;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Syrinj
{
    public static class ObjectExtensions
    {
        public static Object Clone(Object original)
            => InjectObject(Object.Instantiate(original));

        public static T Clone<T>(T original) where T : Object
            => (T)Clone((Object)original);

        public static Object Clone(Object original, Transform parent)
            => InjectObject(Object.Instantiate(original, parent));

        public static T Clone<T>(T original, Transform parent) where T : Object
            => (T)Clone((Object)original, parent);

        public static Object Clone(
            Object original,
            Transform parent,
            bool instantiateInWorldSpace)
            => InjectObject(Object.Instantiate(original, parent, instantiateInWorldSpace));

        public static T Clone<T>(
            T original,
            Transform parent,
            bool instantiateInWorldSpace) where T : Object
            => (T)Clone((Object)original, parent, instantiateInWorldSpace);

        public static Object Clone(Object original, Vector3 position, Quaternion rotation)
            => InjectObject(Object.Instantiate(original, position, rotation));

        public static T Clone<T>(T original, Vector3 position, Quaternion rotation) where T : Object
            => (T)Clone((Object)original, position, rotation);

        public static Object Clone(
            Object original,
            Vector3 position,
            Quaternion rotation,
            Transform parent)
            => InjectObject(Object.Instantiate(original, position, rotation, parent));

        public static T Clone<T>(
            T original,
            Vector3 position,
            Quaternion rotation,
            Transform parent) where T : Object
            => (T)Clone((Object)original, position, rotation, parent);

        private static Object InjectObject(Object obj)
        {
            switch (obj)
            {
                case GameObject gameObject:
                    new GameObjectInjector(gameObject, injectChildren: true).Inject();
                    break;
                case MonoBehaviour monoBehaviour:
                    new GameObjectInjector(monoBehaviour.gameObject, injectChildren: true).Inject();
                    break;
                case ScriptableObject scriptableObject:
                    new ScriptableObjectInjector(scriptableObject).Inject();
                    break;
            }

            return obj;
        }
        public static T[] FindMonoBehavioursOfType<T>(bool includeInactive = false) where T : MonoBehaviour
        {
            return includeInactive
                ? Resources.FindObjectsOfTypeAll<T>().Where(mono =>
                        !String.IsNullOrWhiteSpace(mono.gameObject.scene.name) && mono.gameObject.scene.rootCount != 0)
                    .ToArray()
                : Object.FindObjectsOfType<T>();
        }

        public static MonoBehaviour[] FindMonoBehavioursOfType(Type type, bool includeInactive = false)
        {
            if (!typeof(MonoBehaviour).IsAssignableFrom(type)) return Array.Empty<MonoBehaviour>();
            return includeInactive
                ? Resources.FindObjectsOfTypeAll(type).Select(obj => (MonoBehaviour)obj).Where(mono =>
                    !String.IsNullOrWhiteSpace(mono.gameObject.scene.name) &&
                    mono.gameObject.scene.rootCount != 0).ToArray()
                : Object.FindObjectsOfType(type).Select(obj => (MonoBehaviour)obj).ToArray();
        }

        public static T[] FindMonoBehavioursOfType<T>(string sceneName, bool includeInactive = false)
            where T : MonoBehaviour
        {
            return includeInactive
                ? Resources.FindObjectsOfTypeAll<T>().Where(obj => obj.gameObject.scene.name == sceneName).ToArray()
                : FindMonoBehavioursOfType<T>().Where(obj => obj.gameObject.scene.name == sceneName).ToArray();
        }

        public static MonoBehaviour[] FindMonoBehavioursOfType(Type type, string sceneName, bool includeInactive = false)
        {
            return FindMonoBehavioursOfType(type, includeInactive)
                .Where(mono => mono.gameObject.scene.name == sceneName).ToArray();
        }

        public static T FindMonoBehaviourOfType<T>(bool includeInactive = false) where T : MonoBehaviour
        {
            return includeInactive
                ? FindMonoBehavioursOfType<T>(true).FirstOrDefault()
                : Object.FindObjectOfType<T>();
        }

        public static MonoBehaviour FindMonoBehaviourOfType(Type type, bool includeInactive = false)
        {
            if (typeof(MonoBehaviour).IsAssignableFrom(type))
            {
                return includeInactive
                    ? FindMonoBehavioursOfType(type, true).FirstOrDefault()
                    : (MonoBehaviour) Object.FindObjectOfType(type);
            }
            return null;
        }

        public static T FindMonoBehaviourOfType<T>(string sceneName, bool includeInactive = false) where T : MonoBehaviour
        {
            return FindMonoBehavioursOfType<T>(sceneName, includeInactive).FirstOrDefault();
        }

        public static MonoBehaviour FindMonoBehaviourOfType(Type type, string sceneName, bool includeInactive = false)
        {
            return FindMonoBehavioursOfType(type, sceneName, includeInactive).FirstOrDefault();
        }
    }
}