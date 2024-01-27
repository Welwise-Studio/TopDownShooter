using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialShow : MonoBehaviour
{
    private void Start()
    {
        Time.timeScale = .000000003f;
    }

    private void Update()
    {
        if (Input.anyKey || Input.touchCount > 0)
        {
            Time.timeScale = 1;

            gameObject.SetActive(false);
        }
    }

}
