using Architecture.MVP;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.WeaponShow
{
    public class WeaponShowView : View
    {
        [field: SerializeField]
        public Image Icon { get; private set; }
        [field: SerializeField]
        public RectTransform IconRect { get; private set; }

        [field: SerializeField]
        public TMP_Text ReloadingLabel { get; private set; }
        [field: SerializeField]
        public RectTransform ReloadingLabelRect { get; private set; }
    }
}
