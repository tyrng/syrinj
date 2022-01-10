using System.Collections.Generic;

namespace Syrinj
{
    public class ObjectInjector
    {
        private static HashSet<InjectableObject> visited = new HashSet<InjectableObject>();

        private readonly InjectableObject injectableObject;

        public ObjectInjector(InjectableObject injectableObject)
        {
            this.injectableObject = injectableObject;
        }

        public static void ResetVisited()
        {
            visited = new HashSet<InjectableObject>();
        }

        public void Inject()
        {
            if (visited.Contains(injectableObject)) return;

            DependencyContainer.Instance.Inject(injectableObject);
            MarkAsVisited(injectableObject);
        }

        private void MarkAsVisited(InjectableObject injectableObject)
        {
            visited.Add(injectableObject);
        }
    }
}