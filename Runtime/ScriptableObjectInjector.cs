using System.Collections.Generic;
using UnityEngine;

namespace Syrinj
{
    public class ScriptableObjectInjector
    {
        private static HashSet<ScriptableObject> visited = new HashSet<ScriptableObject>(); 
        
        private readonly ScriptableObject scriptableObject;

        public ScriptableObjectInjector(ScriptableObject scriptableObject)
        {
            this.scriptableObject = scriptableObject;
        }

        public static void ResetVisited()
        {
            visited = new HashSet<ScriptableObject>();
        }

        public void Inject()
        {
            if (visited.Contains(scriptableObject)) return;

            DependencyContainer.Instance.Inject(scriptableObject);
            MarkAsVisited(scriptableObject);
        }

        private void MarkAsVisited(ScriptableObject scriptableObject)
        {
            visited.Add(scriptableObject);
        }
    }
}