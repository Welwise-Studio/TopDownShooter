using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuzzleFlash : MonoBehaviour
{
    [SerializeField] private GameObject flashHolder;
    [SerializeField] private Sprite[] _flashSprites;
    [SerializeField] private SpriteRenderer[] _spriteRenderers;
    [SerializeField] private float _flashTime;
    private void Start()
    {
        Deactivate();
    }
    public void Activate()
    {
        flashHolder.SetActive(true);

        int flashSpriteIndex = Random.Range(0, _flashSprites.Length);
        for(int i = 0; i < _spriteRenderers.Length; i++)
        {
            _spriteRenderers[i].sprite = _flashSprites[flashSpriteIndex];
        }

        Invoke("Deactivate", _flashTime);
    }
    private void Deactivate()
    {
        flashHolder.SetActive(false);
    }
}
