using UnityEngine;
using FlexibleSaveSystem.Savers;

namespace FlexibleSaveSystem.Installers
{
    public class SaveSystemInstaller : MonoBehaviour, IInstaller
    {
        [SerializeField] private bool _useYG;
        [SerializeField] private MonoBehaviour[] _instances;

        private void Start()
        {
            if (_useYG)
                SaveSystem.AddSaver(new YGPluginSaver());
            else
                SaveSystem.AddSaver(new PlayerPrefsSaver());

            foreach (var instance in _instances)
            {
                SaveSystem.InjectInstance(instance);
            }
            SaveSystem.OnReady += SaveSystem.Load;
            SaveSystem.Install(this);
        }

        private void OnApplicationQuit()
        {
            SaveSystem.Save();
        }
    }
}