using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideByTime : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(Hide), 4);
    }

    private void Hide() => gameObject.SetActive(false);
}
