using System;
using UnityEngine;

namespace Syrinj
{
    [Serializable]
    public abstract class InjectableSerializableObject : ISerializationCallbackReceiver
    {
        private void Construct()
        {
            if (SceneInjector.IsSceneInjected)
            {
                Inject();
            }
            SceneInjector.SceneInjected += Inject;
            SceneInjector.SceneDestroyed += Destruct;
        }

        private void Inject()
        {
            new SerializableObjectInjector(this).Inject();
        } 

        public void OnBeforeSerialize() { } 

        public void OnAfterDeserialize()
        {
            Construct();
        }

        private void Destruct()
        {
            SceneInjector.SceneInjected -= Inject;
            SceneInjector.SceneDestroyed -= Destruct;
        }

        ~InjectableSerializableObject()
        {
            Destruct();
        }
    }
}