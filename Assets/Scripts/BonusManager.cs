using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusManager : MonoBehaviour
{
	public Bonus[] BonusList;
	public AudioSource BonusAudio;
	public Ball MainBall;
	public BallsManager BallsManagerObject;
	public Platform GamePlatform;
	public static bool IsGameSlowDown;

	private float GrowTimer = -1;
	private float SlowTimeTimer = -1;
	private float SpeedUpTimer = -1;

	private void Update()
	{
		GrowTimerEffectIfTimerActive();
		SpeedUpTimerEffectIfTimerActive();
		SlowTimerEffectIfTimerActive();
	}
	private void GrowTimerEffectIfTimerActive()
	{
		if (GrowTimer <= 0 && GrowTimer > -1)
		{
			GamePlatform.ThisSpriteRender.size = new Vector2(GamePlatform.ThisSpriteRender.size.x - 4, GamePlatform.ThisSpriteRender.size.y);
			GamePlatform.ThisSpriteRender.GetComponent<BoxCollider2D>().size = new Vector2(GamePlatform.ThisSpriteRender.size.x, GamePlatform.ThisSpriteRender.size.y);
			BonusList[0].BonusObjectView.SetActive(false);
			GrowTimer = -1;
		}
		else if (GrowTimer > 0)
		{
			GrowTimer -= 1 * Time.deltaTime;
			BonusList[0].BonusTextView.text = GrowTimer.ToString();
		}
	}
	private void SpeedUpTimerEffectIfTimerActive()
	{
		if (SpeedUpTimer <= 0 && SpeedUpTimer > -1)
		{
			BallsManagerObject.BallSpeed -= 200;
			BonusList[1].BonusObjectView.SetActive(false);
			SpeedUpTimer = -1;
		}
		else if (SpeedUpTimer > 0)
		{
			SpeedUpTimer -= 1 * Time.deltaTime;
			BonusList[2].BonusTextView.text = SpeedUpTimer.ToString();
		}
	}
	private void SlowTimerEffectIfTimerActive()
	{
		if (SlowTimeTimer <= 0 && SlowTimeTimer > -1)
		{
			Time.timeScale = 1f;
			Time.fixedDeltaTime = Time.timeScale * 0.02f;
			SlowTimeTimer = -1;
			IsGameSlowDown = false;
			BonusList[2].BonusObjectView.SetActive(false);
		}
		else if (SlowTimeTimer > 0)
		{
			SlowTimeTimer -= 1 * Time.deltaTime;
			BonusList[2].BonusTextView.text = SlowTimeTimer.ToString();
		}
	}

	public void ActivateBonus(Collider2D Collision)
	{
		Destroy(Collision.gameObject);
		GrowBonusActivateTimerAudioAndVideoEffectsShowUIIfItIs(Collision);
		SpeedUpBonusActivateTimerAudioAndVideoEffectsShowUIIfItIs(Collision);
		SlowTimeBonusActivateTimerAudioAndVideoEffectsShowUIIfItIs(Collision);
	}
	private void GrowBonusActivateTimerAudioAndVideoEffectsShowUIIfItIs(Collider2D Collision)
	{
		if (Collision.transform.tag == "GrowBonus")
		{
			Destroy(Collision.gameObject);
			if (GrowTimer == -1)
			{
				PlayAudioSpawnParticlesAndShowUI(BonusList[0].BonusAudio, BonusList[0].BonusEffect, GamePlatform.transform.position, BonusList[0].BonusObjectView);
				GamePlatform.ThisSpriteRender.size = new Vector2(GamePlatform.ThisSpriteRender.size.x + 4, GamePlatform.ThisSpriteRender.size.y);
				GamePlatform.ThisSpriteRender.GetComponent<BoxCollider2D>().size = new Vector2(GamePlatform.ThisSpriteRender.size.x, GamePlatform.ThisSpriteRender.size.y);
				GrowTimer = BonusList[0].BonusTime;
			}
			else if (GrowTimer > 0)
			{
				GrowTimer += BonusList[0].BonusTime;
			}
		}
	}
	private void PlayAudioSpawnParticlesAndShowUI(AudioClip BonusClip,ParticleSystem BonusEffect,Vector3 BonusEffectPosition,GameObject BonusObjectUI)
	{
		BonusAudio.clip = BonusClip;
		BonusAudio.Play();
		Instantiate(BonusEffect.gameObject, BonusEffectPosition, Quaternion.identity);
		BonusObjectUI.SetActive(true);
	}
	private void SpeedUpBonusActivateTimerAudioAndVideoEffectsShowUIIfItIs(Collider2D Collision)
	{
		if (Collision.transform.tag == "SpeedUpBonus")
		{
			Destroy(Collision.gameObject);
			if (SpeedUpTimer == -1)
			{
				PlayAudioSpawnParticlesAndShowUI(BonusList[1].BonusAudio, BonusList[1].BonusEffect, MainBall.transform.position, BonusList[1].BonusObjectView);
				BallsManagerObject.BallSpeed += 200;
				SpeedUpTimer = BonusList[1].BonusTime;
			}
			else if (SpeedUpTimer > 0)
			{
				SpeedUpTimer += BonusList[1].BonusTime;
			}
		}
	}
	private void SlowTimeBonusActivateTimerAudioAndVideoEffectsShowUIIfItIs(Collider2D Collision)
	{
		if (Collision.transform.tag == "SlowTimeBonus")
		{
			Destroy(Collision.gameObject);
			if (SlowTimeTimer == -1)
			{
				PlayAudioSpawnParticlesAndShowUI(BonusList[2].BonusAudio, BonusList[2].BonusEffect, Vector3.zero, BonusList[2].BonusObjectView);
				SlowTimeTimer = BonusList[2].BonusTime;
				Time.timeScale = 0.5f;
				Time.fixedDeltaTime = Time.timeScale * 0.02f;
				IsGameSlowDown = true;
			}
			else if (SlowTimeTimer > 0)
			{
				SlowTimeTimer += BonusList[2].BonusTime;
			}
		}
	}
}
