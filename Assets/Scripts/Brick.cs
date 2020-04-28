using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Brick : MonoBehaviour
{
	public ParticleSystem DestroyEffect;
	[HideInInspector] public SpriteRenderer BrickSpriteRenderer;
	[HideInInspector] public float ScoreOnBrake;
	
	private void Awake()
	{
		ScoreOnBrake = 500;
		BrickSpriteRenderer = GetComponent<SpriteRenderer>();
	}
	private void OnCollisionEnter2D(Collision2D collision)
	{
		DestroyBrick();
	}
	protected void DestroyBrick()
	{
		SpawnDestroyEffectAndDestroyItAfterFewSeconds();
		Destroy(gameObject);
		GameManager.Score += ScoreOnBrake;
	}
	private void SpawnDestroyEffectAndDestroyItAfterFewSeconds()
	{
		Vector3 BrickPosition = gameObject.transform.position;
		Vector3 StartPosition = new Vector3(BrickPosition.x, BrickPosition.y, BrickPosition.z - 0.2f);
		GameObject Effect = Instantiate(DestroyEffect.gameObject, StartPosition, Quaternion.identity);
		MainModule EffectMainModule = Effect.GetComponent<ParticleSystem>().main;
		Destroy(Effect, EffectMainModule.startLifetime.constant);
	}
}
