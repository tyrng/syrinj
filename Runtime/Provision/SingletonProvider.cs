using System;

namespace Syrinj.Provision
{
    public class SingletonProvider : IProvider
    {
        public System.Type Type { get; set; }
        public string Tag { get; set; }
        
        public bool IsLazy { get; set; }

        private object instance;
        private bool isInjected;

        public SingletonProvider(Type type, string tag, bool isLazy)
        {
            this.Type = type;
            this.Tag = tag;
            this.IsLazy = isLazy;
            if (!isLazy) instance = Activator.CreateInstance(Type);
        }

        public object Get()
        {
            if (!IsLazy)
            {
                if (isInjected) return instance;
                isInjected = true;
                DependencyContainer.Instance.Inject(instance);
                return instance;
            }
            
            if (instance != null) return instance;
            isInjected = true;
            instance = Activator.CreateInstance(Type);
            DependencyContainer.Instance.Inject(instance);
            return instance;
        }
    }
}

