using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public int score;
    public int[] scoreGoals;
    public Image scoreBar;

    public GameObject mainMenu;
	public GameObject gameplay;
    public GameObject gameOver;
    public GameObject levelCompleted;
	public TextMeshProUGUI scoreText;

	private void Awake()
	{
		GameManager.gameManager.menuUIManager = this;
	}

	private void Start()
	{
		mainMenu.SetActive(true);
	}

	public void StartGame()
	{
		mainMenu.SetActive(false);
		gameplay.SetActive(true);
		GameManager.gameManager.Play();
	}

	public void Continue()
	{
		GameManager.gameManager.started = false;
		GameManager.gameManager.gameOver = false;
		GameManager.gameManager.RestartScene();
	}
    public void AddScore(int amount)
    {
        score += amount;
        scoreText.text = score.ToString();
        if (scoreBar !=null)
        {
            scoreBar.fillAmount = (float)score / (float)scoreGoals[1];
        }
    }
}
