using System;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Syrinj
{
    public static class ObjectExtensions
    {
        public static T[] FindMonoBehavioursOfType<T>() where T : MonoBehaviour
        {
            return Object.FindObjectsOfType<T>();
        }
        public static T[] FindMonoBehavioursOfType<T>(string sceneName) where T : MonoBehaviour
        {
            return Object.FindObjectsOfType<T>().Where(obj =>
                string.Compare(obj.gameObject.scene.name, sceneName, StringComparison.Ordinal) == 0).ToArray();
        }

        public static T FindMonoBehaviourOfType<T>() where T : MonoBehaviour
        {
            return Object.FindObjectOfType<T>();
        }

        public static T FindMonoBehaviourOfType<T>(string sceneName) where T : MonoBehaviour
        {
            return FindMonoBehavioursOfType<T>(sceneName).FirstOrDefault();
        }
    }
}