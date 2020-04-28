using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
	public AudioClip DeathBrick;
	public AudioClip WallOrPlatformColision;
	public AudioSource BallSource;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Death")
		{
			GameManager.IsGameOver = true;
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		PlaySoundOnTagCollisionOrNot(collision, "Brick", DeathBrick);
		PlaySoundOnTagCollisionOrNot(collision, "WallSound", WallOrPlatformColision);
	}
	private void PlaySoundOnTagCollisionOrNot(Collision2D Collision, string Tag, AudioClip clip)
	{
		if (Collision.gameObject.tag == Tag)
		{
			BallSource.clip = clip;
			BallSource.Play();
		}
	}
}
