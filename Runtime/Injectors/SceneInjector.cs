using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Syrinj
{
    public class SceneInjector : MonoBehaviour
    {
        public static event Action SceneInjected;
        public static event Action SceneDestroyed;
        public static bool IsSceneInjected { get; private set; }
        private void Awake()
        {
            DependencyContainer.Instance.Reset();
            InjectScene();
        }

        private void Start()
        {
            SceneManager.sceneLoaded += SceneManager_sceneLoaded;
        }

        private void OnDestroy()
        {
            SceneManager.sceneLoaded -= SceneManager_sceneLoaded;
            SceneDestroyed?.Invoke();
            SceneInjected = null;
            IsSceneInjected = false;
        }

        private void SceneManager_sceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene == gameObject.scene) InjectScene();
        }

        private void InjectScene()
        {
            InjectBehaviours(GetAllBehavioursInScene());
            InjectScriptableObjects(GetAllScriptableObjectsInResource());
            SceneInjected?.Invoke();
            IsSceneInjected = true;
        }

        protected virtual MonoBehaviour[] GetAllBehavioursInScene()
        {
            return ObjectExtensions.FindMonoBehavioursOfType<MonoBehaviour>(gameObject.scene.name, includeInactive: true);
        }

        private void InjectBehaviours(MonoBehaviour[] behaviours)
        {
            DependencyContainer.Instance.Inject(behaviours);
        }


        private ScriptableObject[] GetAllScriptableObjectsInResource()
        {
            return Resources.LoadAll<ScriptableObject>("");
        }

        private void InjectScriptableObjects(ScriptableObject[] scriptableObjects)
        {
            DependencyContainer.Instance.Inject(scriptableObjects);
        }
    }
}
