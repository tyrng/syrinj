using UnityEngine;

namespace Syrinj
{
    public abstract class RuntimeInjectableMonoBehaviour : MonoBehaviour 
    {
        protected virtual void Awake()
        {
            new GameObjectInjector(gameObject).Inject();
        }
    }
}
