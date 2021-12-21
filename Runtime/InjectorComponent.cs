using UnityEngine;

namespace Syrinj
{
    public class InjectorComponent : MonoBehaviour
    {
        public bool ShouldInjectChildren = false;

        void Awake()
        {
            new GameObjectInjector(gameObject, ShouldInjectChildren).Inject();
        }
    }
}
