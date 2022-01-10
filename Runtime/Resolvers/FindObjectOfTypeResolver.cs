using Syrinj.Injection;

namespace Syrinj.Resolvers
{
    public class FindObjectOfTypeResolver : IResolver
    {
        public object Resolve(Injectable injectable)
        {
            var attribute = (FindObjectOfTypeAttribute)injectable.Attribute;
            if (attribute.ComponentType == null)
            {
                return ObjectExtensions.FindMonoBehaviourOfType(injectable.Type, includeInactive: true);
            }
            else
            {
                return ObjectExtensions.FindMonoBehaviourOfType(attribute.ComponentType, includeInactive: true);
            }
        }
    }
}