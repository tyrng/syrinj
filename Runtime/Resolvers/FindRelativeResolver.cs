using Syrinj.Injection;
using UnityEngine;

namespace Syrinj.Resolvers
{
    public class FindRelativeResolver : IResolver
    {
        public object Resolve(Injectable injectable)
        {
            var name = ((FindRelativeAttribute)injectable.Attribute).PathName;

            var parent = ((MonoBehaviour)injectable.Object).transform;
            var transform = parent.FindRelativePath(name);

            if (transform == null) return null;

            return typeof(Transform).IsAssignableFrom(injectable.Type)
                ? transform
                : transform.GetComponent(injectable.Type);
        }
    }
}