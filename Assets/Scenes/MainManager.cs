using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using System.IO;


#if UNITY_EDITOR
using UnityEditor;
#endif

public class MainManager : MonoBehaviour
{
    public static MainManager Instance { get; private set; }

    private bool isGameOver = false;

    public TextMeshProUGUI bestScoreText;

    private BestScoreData bestScoreData;
    private string bestScorePath;

    // Start is called before the first frame update
    private void Awake()
    {
        Debug.Log("Main Manager Awake");

        Instance = this;
        DontDestroyOnLoad(gameObject);

        bestScorePath = Application.persistentDataPath + "/bestScore.json";
        LoadBestScore();

    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "Title Screen")
        {
            LoadBestScore();
        }
    }

    // Update is called once per frame
    private void Update()
    {
        UpdateBestScoreText();
    }

    public void StartGame()
    {
        Debug.Log("Load Game Scene MainManager");
        SceneManager.LoadScene("Game Scene");

    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void HandleGameOver(int scoreFromGameManager)
    {
        Debug.Log("HandleGameOver method called from MainManager");
        if (scoreFromGameManager > bestScoreData.bestScore)
        {
            bestScoreData.bestScore = scoreFromGameManager;
            SaveBestScore();
            UpdateBestScoreText();
        }
    }
    public void UpdateBestScoreText()
    {
        if (bestScoreText != null)
        {
            bestScoreText.text = "Best Score: " + bestScoreData.bestScore;
        }

    }
    public void ResetBestScore()
    {
        bestScoreData.bestScore = 0;
        SaveBestScore();
    }

    public int GetBestScore()
    {
        return bestScoreData.bestScore;
    }

    public bool GameOver()
    {
        return isGameOver;
    }
    private void SaveBestScore()
    {
        if (bestScorePath != null)
        {
            string jsonData = JsonUtility.ToJson(bestScoreData);
            File.WriteAllText(bestScorePath, jsonData);
        }
        else
        {
            Debug.LogError("bestScorePath is null. Cannot save best score.");
        }
    }
    private void LoadBestScore()
    {
        if (!string.IsNullOrEmpty(bestScorePath) && File.Exists(bestScorePath))
        {
            string json = File.ReadAllText(bestScorePath);
            bestScoreData = JsonUtility.FromJson<BestScoreData>(json);
        }
        else
        {
            Debug.LogError("bestScorePath is null or file does not exist. Cannot load best score.");
        }
    }
}
