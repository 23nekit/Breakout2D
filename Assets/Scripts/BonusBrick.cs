using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusBrick : Brick
{
	public GameObject BonusObject;
	public Sprite Newsprite;

	public void ChangeBrickColorAndAddBonusToBrick()
	{
		SpriteRenderer ObjectSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
		ObjectSpriteRenderer.sprite = Newsprite;
		ObjectSpriteRenderer.color = Color.white;
		ScoreOnBrake = 300;
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		DestroyBrick();
		SpawnBonusObject();
	}
	private void SpawnBonusObject()
	{
		Vector3 NewPosition = transform.position;
		Instantiate(BonusObject, NewPosition, Quaternion.identity);
	}
}
