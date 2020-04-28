using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct Bonus
{
	public AudioClip BonusAudio;
	public ParticleSystem BonusEffect;
	public GameObject BonusObjectView;
	public Text BonusTextView;
	public Sprite BonusBrickSprite;
	public GameObject BonusObject;
	public int BonusTime;
}
