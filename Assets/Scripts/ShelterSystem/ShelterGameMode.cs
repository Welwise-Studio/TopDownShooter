using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ShelterSystem
{
    public class ShelterGameMode : MonoBehaviour
    {
        [SerializeField]
        private WavesController _wavesController;
        private bool _isFirstWave = true;

        [SerializeField]
        private Teleport _teleport;
        [SerializeField]
        private AudioClip _tpAwakeSound;

        [SerializeField]
        private Fence _fence;
        private void OnEnable()
        {
            _wavesController.OnNewWave += RestoreEvent;
            _wavesController.OnNewWave += TeleportCheck;
        }

        private void OnDisable()
        {
            _wavesController.OnNewWave -= RestoreEvent;
            _wavesController.OnNewWave -= TeleportCheck;
        }

        private void Awake()
        {
            _teleport.gameObject.SetActive(false);
        }

        private void RestoreEvent(int wave)
        {
            _fence.Restore();
        }

        private void TeleportCheck(int index)
        {
            if (!_isFirstWave)
            {
                _teleport.gameObject.SetActive(true);
                AudioManager.Instance.PlaySound(_tpAwakeSound, _teleport.transform.position);
                _wavesController.IsDisabled = true;
            }

            _isFirstWave = false;
        }
    }
}
