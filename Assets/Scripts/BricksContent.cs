using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BricksContent : MonoBehaviour
{
	public float Speed = 0.001f;
	public float ChangeColorPosition = -2.7f;
	public float GameOverPosition = -8.3f;

	private Vector3 PlusVector;
	private int ChangeColorPhase = 1;
	private List<StandartBrick> childs;

	private void Awake()
	{
		PlusVector = new Vector3(0, -Speed);
	}

	private void Update()
	{
		MoveBricks();
		DestroyOldContentIfIsNeedIt();
		ChangeColorAndScoreOnBricksifTheyNeedIt();
		GameOverOrNot();
	}
	private void MoveBricks()
	{
		if (IsGameStartedAndNotPause())
		{
			if (BonusManager.IsGameSlowDown)
			{
				transform.position += PlusVector / 2;
			}
			else
			{
				transform.position += PlusVector;
			}
		}
	}
	private bool IsGameStartedAndNotPause()
	{
		return GameManager.IsGameStarted && !GameManager.IsGamePased;
	}
	private void DestroyOldContentIfIsNeedIt()
	{
		if (IsContentEmptyAndPositionOnEnd())
		{
			Destroy(gameObject);
		}
	}
	private bool IsContentEmptyAndPositionOnEnd()
	{
		return transform.childCount == 0 && transform.position.y < BrickManager.YForNewSpawn_Const - 0.1f;
	}
	private void ChangeColorAndScoreOnBricksifTheyNeedIt()
	{
		if (IsContentOnChangeColorAndScorePosition(1, ChangeColorPosition))
		{
			ChangeColorAndScore(2, Color.green, 300);
		}
		if (IsContentOnChangeColorAndScorePosition(2, ChangeColorPosition + ChangeColorPosition))
		{
			ChangeColorAndScore(1, Color.white, 100);
		}
	}
	private bool IsContentOnChangeColorAndScorePosition(int ChangeColorPhase, float Position)
	{
		return this.ChangeColorPhase == ChangeColorPhase && transform.position.y < Position;
	}
	private void ChangeColorAndScore(int ColorPhase,Color NewColor, int Score)
	{
		childs = GetComponentsInChildren<StandartBrick>().ToList();
		ChangeColorPhase = ColorPhase;
		for (int i = 0; i < childs.Count; i++)
		{
			childs[i].BrickSpriteRenderer.color = NewColor;
			childs[i].ScoreOnBrake = Score;
		}
	}
	private void GameOverOrNot()
	{
		if (transform.position.y < GameOverPosition && !GameManager.IsGameOver)
		{
			GameManager.IsGameOver = true;
			Destroy(gameObject);
		}
	}
}
