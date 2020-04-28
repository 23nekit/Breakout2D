using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
	public float Speed = 0.001f;

	private Vector3 PlusVector;

	void Start()
    {
		PlusVector = new Vector3(0, -Speed);
	}
	
    void Update()
    {
		if (GameManager.IsGameStarted && !GameManager.IsGamePased && !BonusManager.IsGameSlowDown)
		{
			transform.position += PlusVector;
		}
		if(GameManager.IsGameStarted && !GameManager.IsGamePased && BonusManager.IsGameSlowDown)
		{
			transform.position += PlusVector/2;
		}
		if (transform.position.y < -10)
		{
			transform.position = Vector3.zero;
		}
	}
}
