using UnityEngine;

namespace Syrinj
{
    public class RuntimeInjectableMonoBehaviour : MonoBehaviour 
    {
        protected virtual void Awake()
        {
            new GameObjectInjector(gameObject).Inject();
        }
    }
}
