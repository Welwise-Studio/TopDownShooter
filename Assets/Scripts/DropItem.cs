using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour
{
	[field: SerializeField] public AudioClip catchUpSoundEffect { get; private set; }
	[field: SerializeField] [Tooltip("Chance of drop")] public float dropChance { get; private set; }
	[field: SerializeField] [Tooltip("Amount of healthpoint to add")] public int health { get; private set; }
	[field: SerializeField] [Tooltip("Number of a gun from GunController list")] public int gun { get; private set; }
	[field: SerializeField] public int defaultCoinAmount { get; private set; }
}