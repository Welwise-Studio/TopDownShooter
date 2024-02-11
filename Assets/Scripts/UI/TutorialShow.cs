using UnityEngine;

public class TutorialShow : MonoBehaviour
{
    public static bool IsShown;

    private bool _currentShown;
    [SerializeField] private GameObject _gameObject;

    private void Start()
    {
        _gameObject.SetActive(false);
        if (!IsShown)
        {
            _gameObject.SetActive(true);
            Time.timeScale = .000000003f;
            IsShown = true;
            _currentShown = true;
        }
    }

    private void Update()
    {
        if (IsShown && !_currentShown)
            return;

        if (Input.anyKey || Input.touchCount > 0)
        {
            Time.timeScale = 1;

            _gameObject.SetActive(false);
        }
    }

}
