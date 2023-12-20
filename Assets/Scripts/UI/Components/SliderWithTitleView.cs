using Architecture.MVP;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Components
{
    public sealed class SliderWithTitleView : View
    { 
        [field: SerializeField]
        public Slider Slider { get; private set; }

        [SerializeField]
        private TMP_Text _title;
    }
}
