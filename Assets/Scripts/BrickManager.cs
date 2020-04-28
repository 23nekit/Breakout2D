using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BrickManager : MonoBehaviour
{
	public BricksContent ContentPrefab;
	public Vector3 StartPosition;
	public const float YForNewSpawn_Const = -2.7f;
	public ParticleSystem DeathEffect;
	public Vector2 RandomBonusRange;
	public int MaximumBricks =36;
	public BonusManager BonusManagerObject;

	private int StandartBrickChance = 10;
	private GameObject NewContent;
	private bool IsCreateNewContent = false;
	
	private void Start()
	{
		CreateContentAndAddOrNotBonus();
	}
	private void CreateContentAndAddOrNotBonus()
	{
		NewContent = Instantiate(ContentPrefab.gameObject);
		AddOrNotBonusBricks();
	}
	public void AddOrNotBonusBricks()
	{
		int CountOfBonuceBricks = GenerateRandomNumber((int)RandomBonusRange.x, (int)RandomBonusRange.y);
		List<Brick> NewContentList = NewContent.GetComponentsInChildren<Brick>().ToList();
		for (int i = 0; i < CountOfBonuceBricks - StandartBrickChance; i++)
		{
			ChangeBrickToBonusAndEditIt(NewContentList);
		}
	}
	private int GenerateRandomNumber(int min, int max)
	{
		Random NewRandom = new Random();
		return Random.Range(min, max);
	}
	private void ChangeBrickToBonusAndEditIt(List<Brick> ContentList)
	{
		int NewBonuceBrick = GenerateRandomNumber(0, MaximumBricks + 1);
		int RandomBrick = GenerateRandomNumber(0, BonusManagerObject.BonusList.Length);
		Destroy(ContentList[NewBonuceBrick]);
		BonusBrick NewContentBonusBrick = ContentList[NewBonuceBrick].gameObject.AddComponent<BonusBrick>();
		EditBonusBrick(NewContentBonusBrick, RandomBrick);
	}
	private void EditBonusBrick(BonusBrick NewBonusBrick, int IdOfNewBrick)
	{
		NewBonusBrick.DestroyEffect = DeathEffect;
		NewBonusBrick.BonusObject = BonusManagerObject.BonusList[IdOfNewBrick].BonusObject;
		NewBonusBrick.Newsprite = BonusManagerObject.BonusList[IdOfNewBrick].BonusBrickSprite;
		NewBonusBrick.ChangeBrickColorAndAddBonusToBrick();
	}

	private void Update()
	{
		CreateNewContentOrNot();
	}
	private void CreateNewContentOrNot()
	{
		if (IsCreateNewContent)
		{
			IsCreateNewContent = false;
			NewContent = Instantiate(ContentPrefab.gameObject, StartPosition, Quaternion.identity);
			AddOrNotBonusBricks();
		}
		if (IsNewContentPositionWellForSpawnNew())
		{
			IsCreateNewContent = true;
		}
	}
	private bool IsNewContentPositionWellForSpawnNew()
	{
		return NewContent.transform.position.y < YForNewSpawn_Const;
	}
}
