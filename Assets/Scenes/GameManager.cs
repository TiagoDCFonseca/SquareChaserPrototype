using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameManager gameManager;

    public Button restartButton;
    public Button homeButton;

    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI bestScoreText;

    private int score = 0;
    private int bestScore;
    

    private bool isGameOver = false;

    // Start is called before the first frame update
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    void Start()
    {
        StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateBestScoreText();

    }

    public void GameOver()
    {
        if (!isGameOver)
        {
            Debug.Log("Enter Game over method!");
            isGameOver = true;
            gameOverText.gameObject.SetActive(true);
            restartButton.gameObject.SetActive(true);
            homeButton.gameObject.SetActive(true);

            //Save Best Score
            MainManager.Instance.HandleGameOver(score);
            
            UpdateScoreText();
            UpdateBestScoreText();
        }
        
    }

    public void RestartGame()
    {
        Debug.Log("RestartGame method Called ");
        SceneManager.LoadScene("Game Scene");
        isGameOver = false;
        SpawnManager.Instance.ResetSpawnState();
       
    }

    public void BackToMenu()
    {
        Debug.Log("BackToMenu method called");
        //MainManager.Instance.ResetBestScore();
        SceneManager.LoadScene("Title Screen");

    }

    public void IncreaseScore()
    {
        score += 5;
        UpdateScoreText();
    }

    private void UpdateScoreText ()
    {
        if(scoreText != null)
        {
            scoreText.text = "Score: " + score.ToString();
        }
    }

    private void UpdateBestScoreText()
    {
        if (bestScoreText != null)
        {
            bestScoreText.text = "Best Score: " + MainManager.Instance.GetBestScore();
        }
    }

    public bool IsGameOver()
    {
        return isGameOver;
    }

    public void StartGame()
    {
        Debug.Log("StartGame method called");
        score = 0;
        restartButton.gameObject.SetActive(false);
        homeButton.gameObject.SetActive(false);
        gameOverText.gameObject.SetActive(false);
        //MainManager.Instance.ResetBestScore();
        MainManager.Instance.UpdateBestScoreText();
        
    }
}
