using Architecture.DI;
using UnityEngine;

namespace Architecture.Contexts
{
    public sealed class ProjectContext : Context
    {
        public static ProjectContext Instance { get; private set; }

        [SerializeField] private bool _initOnAwake = false; 
        private bool _isInit;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            if (_initOnAwake)
                Init();
        }

        public void Init()
        {
            if (_isInit)
                return;

            Instance = this;

            DontDestroyOnLoad(gameObject);
            Initialize();
            _isInit = true;
        }

        protected override IDIContainer CreateLocalContainer()
        {
            return new DIContainer();
        }
    }
}