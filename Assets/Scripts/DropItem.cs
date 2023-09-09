using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    public enum ItemType { SmallFirstAidPack, MediumFirstAidPack, BigFirstAidPack };
    [SerializeField] private ItemType _itemType = ItemType.MediumFirstAidPack;
	[field: SerializeField] public float _dropChance { get; private set; }= 0f;
    public void Take()
    {
        float health = 0f;

        if (_itemType == ItemType.SmallFirstAidPack)
        {
            health = 1f;
        }
        else if (_itemType == ItemType.MediumFirstAidPack)
        {
            health = 2f;
        }
        else
        {
            health = 3f;
        }

        FindObjectOfType<Player>().AddHealth(health);
    }
}