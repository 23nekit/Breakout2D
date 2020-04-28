using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	public GameObject PlatformObject;
	public Text ScoreObject;
	public Text LengthObject;
	public Text TimeObject;
	public GameObject PauseMenuUI;
	public GameObject GameOverUI;
	public AudioClip GameOverMusic;
	public AudioSource GameMusic;
	public static bool IsGameStarted;
	public static bool IsGameOver = false;
	public static float Score;
	public static bool IsGamePased = false;
	
	private float TimeAfterStart;

	public void Update()
	{
		if (IsGameStarted)
		{
			ChangeAndViewHUD();
		}
		if (Input.GetKeyDown(KeyCode.Escape)) 
		{
			if (IsGamePased)
			{
				ResumeGame();
			}
			else
			{
				PauseGame();
			}
		}
		if (IsGameOver)
		{
			GameOverActivate();
		}
	}
	private void ChangeAndViewHUD()
	{
		ScoreObject.text = "Score :" + Score.ToString();
		LengthObject.text = "Length :" + PlatformObject.GetComponent<SpriteRenderer>().size.x.ToString();
		TimeAfterStart += 1 * Time.deltaTime;
		TimeObject.text = "Time :" + ((int)TimeAfterStart).ToString();
	}
	private void GameOverActivate()
	{
		Time.timeScale = 0f;
		GameOverUI.SetActive(true);
		GameMusic.clip = GameOverMusic;
		GameMusic.loop = false;
		GameMusic.Play();
		IsGamePased = true;
		IsGameOver = false;

	}
	private void PauseGame()
	{
		Time.timeScale = 0f;
		PauseMenuUI.SetActive(true);
		IsGamePased = true;
	}
	public void ResumeGame()
	{
		Time.timeScale = 1f;
		PauseMenuUI.SetActive(false);
		IsGamePased = false;
	}

	public void RestartGame()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		Time.timeScale = 1f;
		PauseMenuUI.SetActive(false);
		GameOverUI.SetActive(false);
		IsGamePased = false;
		IsGameStarted = false;
		TimeAfterStart = 0;
		Score = 0;
	}
}
