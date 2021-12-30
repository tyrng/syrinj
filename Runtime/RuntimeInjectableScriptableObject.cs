using UnityEditor;
using UnityEngine;

namespace Syrinj
{
    public abstract class RuntimeInjectableScriptableObject : ScriptableObject 
    {
        protected virtual void Awake()
        {
            new ScriptableObjectInjector(this).Inject();
        }
    }
}