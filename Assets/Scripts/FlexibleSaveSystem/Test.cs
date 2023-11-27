using TMPro;
using UnityEngine;

namespace FlexibleSaveSystem
{
    public class Test : MonoBehaviour
    {
        [SaveData]
        public int Value1;

        [SerializeField] private TextMeshProUGUI _valueHolder;

        private void Awake()
        {
            SaveSystem.OnReady += () =>
            {
                SaveSystem.Load();
            };
        }

        private void Update()
        {
            _valueHolder.text = Value1.ToString();
        }

        public void Save()
        {
            SaveSystem.Save();
        }
    }
}
