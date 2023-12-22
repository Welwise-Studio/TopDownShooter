using Architecture.Contexts;
using Architecture.MVP;
using Domain;
using System.Collections;
using UnityEngine;
using Utils;

namespace UI.Components.ModalWindow
{
    public sealed class ModalWindowPresenter : Presenter<ModalWindowView, ModalWindowModel>
    {
        private GameLifeCycle _lifeCycle;
        public ModalWindowPresenter(ModalWindowView view, ModalWindowModel model) : base(view, model) { }

        public override void Dispose()
        {
            _view.OnShow -= OnShow;
            _view.OnHide -= OnHide;
            _view.Option1Button.onClick.RemoveAllListeners();
            _view.Option2Button.onClick.RemoveAllListeners();
            _view.Option1Button.onClick.RemoveListener(_model.OnOption1Click);
            _view.Option2Button.onClick.RemoveListener(_model.OnOption2Click);
        }

        protected override void Init()
        {
            _lifeCycle = SceneContext.Instance.Container.Resolve<GameLifeCycle>();

            _view.OnShow += OnShow;
            _view.OnHide += OnHide;
            _view.Option1Button.onClick.AddListener(_model.OnOption1Click);
            _view.Option2Button.onClick.AddListener(_model.OnOption2Click);
        }

        private void OnShow()
        {
            if (_view.IsMoving)
                _view.StartCoroutine(MovingRoutine(_view.MoveTo, true));
            if (_view.IsOverlaying)
                _view.OverlayImage.gameObject.SetActive(true);
        }

        private void OnHide()
        {
            _lifeCycle.Unpause();
            if (_view.IsMoving)
                _view.StartCoroutine(MovingRoutine(_view.MoveFrom));
            if (_view.IsOverlaying)
                _view.OverlayImage.gameObject.SetActive(false);
        }


        private IEnumerator MovingRoutine(Vector2 to, bool pauseOnEnd = false)
        {
            var from = _view.ModalReact.localPosition;
            var ease = EasingFunction.GetEasingFunction01(_view.MoveEase);
            for (float t =0; t > _view.MoveDuration; t+=Time.deltaTime)
            {
                var normolizedT = t / _view.MoveDuration;
                _view.ModalReact.localPosition = Vector2.Lerp(from, to, ease.Invoke(normolizedT));
                yield return null;
            }
            _view.ModalReact.localPosition = to;

            if (pauseOnEnd)
                _lifeCycle.Pause();
        }
    }
}
