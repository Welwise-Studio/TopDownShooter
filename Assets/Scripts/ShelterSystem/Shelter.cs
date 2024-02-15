using System;
using UnityEngine;
using HealthSystem;
using YG;


namespace ShelterSystem
{
    public class Shelter : LivingEntity
    {
        [Serializable]
        public struct Stats
        {
            public float MaxHealth;
            public float RegenerationPerSecond;
        }

        public event Action<int> OnLevelUp;
#if UNITY_EDITOR
        public int Level { get => _level; private set => _level = value; }
        [SerializeField]
        private int _level = 1;

        public int MaxLevel { get => _maxLevel; private set => _maxLevel = value; }
        [SerializeField]
        private int _maxLevel = 10;
#else
        public int Level { get; private set; } = 1;
        public int MaxLevel { get; private set; } = 10;
#endif


        [SerializeField]
        private Stats[] _statsPerLevel;

        [SerializeField]
        private HPRegeneration _regeneration;

        [SerializeField]
        private ShelterUpgradeElement[] _elements;
        [SerializeField]
        private DieModal _dieModal;
        [SerializeField]
        private Fence _fence;
        [SerializeField]
        private AudioClip _upgradeFx;


        protected override void Start()
        {
            base.Start();

            if (CombinedSDK.IsInitilized)
                GetLevelData();
        }

        private void OnEnable()
        {
            CombinedSDK.OnCombinedSDKInitilizedEvent += GetLevelData;
        }

        private void OnDisable()
        {   
            CombinedSDK.OnCombinedSDKInitilizedEvent -= GetLevelData;
        }

        private void GetLevelData()
        {
            Level = CombinedSDK.AllSavesCombinedSDK.ShelterLevel;
            UpdateElements();
            UpdateStats();
            AddHealth(1);
            _fence.AddHealth(startingHealth);
            OnLevelUp?.Invoke(Level);
        }


        public void Upgrade()
        {
            if (Level == MaxLevel)
                return;

            AudioManager.Instance.PlaySound(_upgradeFx, transform.position);

            Level++;
            UpdateElements();
            UpdateStats();
            _fence.AddHealth(startingHealth);
            CombinedSDK.AllSavesCombinedSDK.ShelterLevel = Level;
            CombinedSDK.SaveProgressData();
            OnLevelUp?.Invoke(Level);
        }

        private void UpdateStats()
        {
            if (Level > _statsPerLevel.Length)
                return;

            var currentStats = _statsPerLevel[Level - 1];
            if (_regeneration != null)
                _regeneration.RegenerationPerSecond = currentStats.RegenerationPerSecond;
            Debug.Log(startingHealth);
            startingHealth = currentStats.MaxHealth;
            Debug.Log(startingHealth);
        }

        private void UpdateElements()
        {
            foreach (var element in _elements)
            {
                element.UpdateLevel(Level);
            }
        }

        private Stats[] GenerateLevelStatsExp(float expfHealth, float expfRegeneration,
            float startHealth, float startRegeneration, float endHealth, float endRegeneration)
        {
            Stats[] stats = new Stats[MaxLevel];

            float expHealth = 2.3f + expfHealth;
            float expRegeneration = 2.3f + expfRegeneration;

            for (int i = 0; i < MaxLevel; i++)
            {
                stats[i] = new Stats();
                var newHealth = startHealth * (float)Math.Pow(expHealth, i * Mathf.Log(endHealth / startHealth) / (MaxLevel - 1));
                var newRegen = startRegeneration * (float)Mathf.Pow(expRegeneration, i * Mathf.Log(endRegeneration / startRegeneration) / (MaxLevel - 1));
                stats[i].MaxHealth = Mathf.Clamp(Mathf.CeilToInt(newHealth), startHealth, endHealth);
                stats[i].RegenerationPerSecond = Mathf.Clamp(Mathf.CeilToInt(newRegen), startRegeneration, endRegeneration);
            }

            return stats;
        }

        public override void Die()
        {
            _dieModal.Show();
        }
    }
}
