using Architecture.DI;

namespace Architecture.Contexts
{
    public class SceneContext : Context
    {
        public static SceneContext Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            else
                Instance = this;
        }

        protected override IDIContainer CreateLocalContainer()
        {
            var rootContainer = ProjectContext.Instance.Container;

            return new DIContainer(rootContainer);
        }

        private void Start()
        {
            Initialize();
        }
    }
}
