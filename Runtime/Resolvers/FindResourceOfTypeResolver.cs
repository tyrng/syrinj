using Syrinj.Attributes;
using Syrinj.Injection;
using UnityEngine;

namespace Syrinj.Resolvers
{
    public class FindResourceOfTypeResolver : IResolver
    {
        public object Resolve(Injectable injectable)
        {
            var attribute = (FindResourceOfTypeAttribute)injectable.Attribute;
            if (attribute.ComponentType == null)
            {
                var resources =  Resources.LoadAll("", injectable.Type);
                return resources?[0];
            }
            else
            {
                var resources =  Resources.LoadAll("", attribute.ComponentType);
                return resources?[0];
            }
        }
    }
}