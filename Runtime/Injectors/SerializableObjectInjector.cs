using System.Collections.Generic;

namespace Syrinj
{
    public class SerializableObjectInjector
    {
        private static HashSet<InjectableSerializableObject> visited = new HashSet<InjectableSerializableObject>();

        private readonly InjectableSerializableObject injectableSerializableObject;

        public SerializableObjectInjector(InjectableSerializableObject injectableSerializableObject)
        {
            this.injectableSerializableObject = injectableSerializableObject;
        }

        public static void ResetVisited()
        {
            visited = new HashSet<InjectableSerializableObject>();
        }

        public void Inject()
        {
            if (visited.Contains(injectableSerializableObject)) return;

            DependencyContainer.Instance.Inject(injectableSerializableObject);
            MarkAsVisited(injectableSerializableObject);
        }

        private void MarkAsVisited(InjectableSerializableObject injectableSerializableObject)
        {
            visited.Add(injectableSerializableObject);
        }
    }
}