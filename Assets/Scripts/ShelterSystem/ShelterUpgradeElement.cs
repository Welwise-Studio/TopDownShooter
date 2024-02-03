using System;
using System.Collections;
using UnityEngine;

namespace ShelterSystem
{
    public class ShelterUpgradeElement : MonoBehaviour
    {
        [Serializable]
        public struct ModelForLevel
        {
            public int Level;
            public GameObject Model;
        }


        [Header("VFX")]
        [SerializeField]
        private ParticleSystem _poofParticle;

        [SerializeField]
        private float _showDuration = .34f;

        [SerializeField]
        private float _showScale = 1;

        [SerializeField] 
        private float _hideScale = 0;

        [SerializeField]
        private ModelForLevel[] _modelsForLevels;

        private GameObject _lastModel;

        private void Awake()
        {
            _lastModel = GetModel(1);
        }

        public void UpdateLevel(int level)
        {
            bool check = false;
            foreach (var item in _modelsForLevels)
            {
                if (item.Level == (level))
                {
                    check = true;
                    break;
                }
            }

            if (!check)
                return;


            var newModel = GetModel(level);

            if (_lastModel != null)
                StartCoroutine(ChangeObjectsRoutine(_lastModel, newModel));
            else
                ShowObject(newModel);

            _lastModel = newModel;
        }

        private GameObject GetModel(int level)
        {
            foreach (var item in _modelsForLevels)
            {
                if (item.Level == level)
                    return item.Model;
            }
            return null;
        }

        private IEnumerator ChangeObjectsRoutine(GameObject one, GameObject two)
        {
            Debug.Log(one.name + ", " + two.name);
            HideObject(one);
            yield return new WaitForSeconds(_showDuration+.01f);
            ShowObject(two);
        }

        private void HideObject(GameObject obj)
        {
            _poofParticle.Play();
            StartCoroutine(ScaleObject(_lastModel.transform, _showScale, _hideScale,
                    () => _lastModel.SetActive(true),
                    () => _lastModel.SetActive(false)));
        }

        private void ShowObject(GameObject obj)
        {
            _poofParticle.Play();
            StartCoroutine(ScaleObject(_lastModel.transform, _hideScale, _showScale,
                    () => _lastModel.SetActive(false),
                    () => _lastModel.SetActive(true)));
        }

        private IEnumerator ScaleObject(Transform obj, float from, float to, Action startAction = null, Action endAction = null)
        {
            startAction?.Invoke();
            var fromVector = from * Vector3.one;
            var toVector = to * Vector3.one;

            obj.localScale = fromVector;
            for (float t = 0; t <= _showDuration; t += Time.deltaTime)
            {
                obj.localScale = Vector3.Lerp(fromVector, toVector, EaseInOutBack(t / _showDuration));
                yield return null;
            }
            obj.localScale = toVector;
            endAction?.Invoke();
        }

        private float EaseInOutBack(float x)
        {
            const float c1 = 1.70158f;
            const float c2 = c1 * 1.525f;

            return x < 0.5f
                ? (Mathf.Pow(2 * x, 2) * ((c2 + 1) * 2 * x - c2)) / 2f
                : (Mathf.Pow(2 * x - 2, 2) * ((c2 + 1) * (x * 2 - 2) + c2) + 2f) / 2f;
        }
    }
}
