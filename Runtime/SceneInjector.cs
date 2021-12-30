using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Syrinj
{
    public class SceneInjector : MonoBehaviour
    {
        public static SceneInjector Instance;
        
        void Awake()
        {
            Instance = this;
            DependencyContainer.Instance.Reset();
            InjectScene();
        }

        void Start()
        {
            SceneManager.sceneLoaded += SceneManager_sceneLoaded;
        }

        void OnDestroy()
        {
            SceneManager.sceneLoaded -= SceneManager_sceneLoaded;
        }

        void SceneManager_sceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.name == gameObject.scene.name) InjectScene();
        }

        public void InjectScene()
        {
            var behaviours = GetAllBehavioursInScene();
            InjectBehaviours(behaviours);
        }

        protected virtual MonoBehaviour[] GetAllBehavioursInScene()
        {
            return ObjectExtensions.FindMonoBehavioursOfType<MonoBehaviour>(gameObject.scene.name);
        }

        private void InjectBehaviours(MonoBehaviour[] behaviours)
        {
            DependencyContainer.Instance.Inject(behaviours);
        }
    }
}
