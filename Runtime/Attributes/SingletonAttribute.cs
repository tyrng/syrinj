using Syrinj.Attributes;

namespace Syrinj
{
    public class SingletonAttribute : UnityProviderAttribute
    {
        public readonly SingletonOptions SingletonOptions;
        public SingletonAttribute(SingletonOptions singletonOptions = SingletonOptions.NonLazy)
        {
            SingletonOptions = singletonOptions;
        }
    }
}