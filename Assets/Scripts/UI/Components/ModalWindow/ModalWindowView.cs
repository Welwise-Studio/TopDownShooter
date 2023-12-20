using Architecture.MVP;
using UnityEngine;
using UnityEngine.UI;
using Utils;
using Utils.ConditionalField;

namespace UI.Components.ModalWindow
{
    public class ModalWindowView : View
    {
        [field: Header("Style")]
        [field: SerializeField]
        public bool IsOverlaying { get; private set; }

        [field: SerializeField]
        public bool IsMoving { get; private set; }

        [field: SerializeField]
        [field: ConditionalField(nameof(IsMoving))]
        public Vector2 MoveFrom { get; set; }

        [field: SerializeField]
        [field: ConditionalField(nameof(IsMoving))]
        public Vector2 MoveTo { get; set; }

        [field: SerializeField]
        [field: ConditionalField(nameof(IsMoving))]
        public EasingFunction.Ease MoveEase { get; set; }

        [field: SerializeField]
        [field: ConditionalField(nameof(IsMoving))]
        public float MoveDuration { get; set; }

        [field: Header("References")]
        [field: SerializeField]
        public Button Option1Button { get; private set; }
        [field: SerializeField]
        public Button Option2Button { get; private set; }

        [field: SerializeField]
        public Image OverlayImage { get; private set; }

        [field: SerializeField]
        public RectTransform ModalReact { get; private set; }

    }
}
