using TMPro;
using UnityEngine;

namespace ShelterSystem
{
    public class ShelterLevelLabel : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _lvlHolder;
        [SerializeField]
        private Shelter _shelter;

        private void OnEnable()
        {
            _shelter.OnLevelUp += SetText;
        }

        private void OnDisable()
        {
            _shelter.OnLevelUp -= SetText;
        }

        private void SetText(int lvl) => _lvlHolder.text = lvl.ToString();
    }
}
