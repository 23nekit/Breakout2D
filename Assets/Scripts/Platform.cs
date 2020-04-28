using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Platform : MonoBehaviour
{
	public float Speed = 5;
	public float DefaultMaxXClamp = 6.3f;
	public float DefaultMinXClamp = -6.3f;
	public float DefaultPlatformWidthInPixels = 4;
	public float ColisionAngle = 200;
	public BallsManager BallsManagerObject;
	public BonusManager BonusManagerObject;
	[HideInInspector] public SpriteRenderer ThisSpriteRender;
	
	private float PlatformShift;

	private void Start()
	{
		ThisSpriteRender = GetComponent<SpriteRenderer>();
	}

	private void Update()
	{
		if (!GameManager.IsGamePased)
		{
			PlatformShift = (DefaultPlatformWidthInPixels - ThisSpriteRender.size.x) / 2;
			if (Input.GetKey(KeyCode.LeftArrow))
			{
				transform.position = new Vector3(Mathf.Clamp(transform.position.x + (-Speed), DefaultMinXClamp - PlatformShift, DefaultMaxXClamp + PlatformShift), transform.position.y);
			}
			if (Input.GetKey(KeyCode.RightArrow))
			{
				transform.position = new Vector3(Mathf.Clamp(transform.position.x + Speed, DefaultMinXClamp - PlatformShift, DefaultMaxXClamp + PlatformShift), transform.position.y);
			}
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.transform.tag == "Ball")
		{
			Vector3 PlatformCenter = transform.position;
			Vector3 HitPoint = collision.contacts[0].point;
			Rigidbody2D BallRigidBody = collision.gameObject.GetComponent<Rigidbody2D>();
			float Difference = PlatformCenter.x - HitPoint.x;
			BallRigidBody.velocity = Vector2.zero;
			if (HitPoint.x < PlatformCenter.x)
			{
				BallRigidBody.AddForce(new Vector2(-Mathf.Abs(Difference * ColisionAngle), BallsManagerObject.BallSpeed));
			}
			else
			{
				BallRigidBody.AddForce(new Vector2(Mathf.Abs(Difference * ColisionAngle), BallsManagerObject.BallSpeed));
			}
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		BonusManagerObject.ActivateBonus(collision);
	}
}
