using UnityEngine;

namespace Syrinj
{
    public class SceneRootInjector: SceneInjector
    {
        protected override MonoBehaviour[] GetAllBehavioursInScene()
        {
            return gameObject.GetComponentsInChildren<MonoBehaviour>(includeInactive: true);
        }
    }
}