using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallsManager : MonoBehaviour
{
	public Ball MainBall;
	public Platform PlatformObject;
	public float BallSpeed;
	
	private Vector3 PlatformPosition;

	private void Start()
	{
		InitBall();
	}
	public void InitBall()
	{
		PlatformPosition = PlatformObject.transform.position;
		Vector3 StartingPosition = new Vector3(PlatformPosition.x, PlatformPosition.y + .5f);
		MainBall.transform.position = StartingPosition;
	}

	private void Update()
	{
		if (!GameManager.IsGameStarted) 
		{
			PlatformPosition = PlatformObject.transform.position;
			MainBall.transform.position = new Vector3(PlatformPosition.x, PlatformPosition.y + .5f);
			StartGameIfPressedSpace();
		}
	}
	private void StartGameIfPressedSpace()
	{
		if (Input.GetKeyUp(KeyCode.Space))
		{
			Rigidbody2D InitialBallRigidbody = MainBall.GetComponent<Rigidbody2D>();
			InitialBallRigidbody.isKinematic = false;
			InitialBallRigidbody.AddForce(new Vector2(0, BallSpeed));
			GameManager.IsGameStarted = true;
		}
	}
}
