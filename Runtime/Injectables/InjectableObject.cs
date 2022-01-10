namespace Syrinj
{
    public class InjectableObject
    {
        protected InjectableObject()
        {
            Construct();
        }
        
        private void Construct()
        {
            if (SceneInjector.IsSceneInjected)
            {
                Inject();
            }
            SceneInjector.SceneInjected += Inject;
        }

        private void Inject()
        {
            new ObjectInjector(this).Inject();
        }

        ~InjectableObject()
        {
            SceneInjector.SceneInjected -= Inject;
        }
    }
}