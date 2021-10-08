using Syrinj.Attributes;
using Syrinj.Injection;
using UnityEditor;
using UnityEngine;

namespace Syrinj.Resolvers
{
    public class FindResourceOfTypeResolver : IResolver
    {
        public object Resolve(Injectable injectable)
        {
            //FindObjectOfTypeAttribute
            var attribute = (FindResourceOfTypeAttribute)injectable.Attribute;
            if (attribute.ComponentType == null)
            {
                var resources = Resources.FindObjectsOfTypeAll(injectable.Type);
                return resources?[0];
            }
            else
            {
                var resources = Resources.FindObjectsOfTypeAll(attribute.ComponentType);
                return resources?[0];
            }
        }
        // public static T[] GetAllInstances<T>() where T : ScriptableObject
        // {
        //     string[]
        //         guids = AssetDatabase.FindAssets("t:" +
        //                                          typeof(T)
        //                                              .Name); //FindAssets uses tags check documentation for more info
        //     T[] a = new T[guids.Length];
        //     for (int i = 0; i < guids.Length; i++) //probably could get optimized 
        //     {
        //         string path = AssetDatabase.GUIDToAssetPath(guids[i]);
        //         a[i] = AssetDatabase.LoadAssetAtPath<T>(path);
        //     }
        //
        //     return a;
        // }
    }

}