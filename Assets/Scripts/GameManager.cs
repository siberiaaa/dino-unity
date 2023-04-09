using UnityEngine;
using TMPro; //for the text mesh
using UnityEngine.UI; //for legacy ones
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }


    public float initalGameSpeed = 5f;
    public float gameSpeedIncrease = 0.1f;
    public float gameSpeed { get; private set; }

    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI hiscoreText;
    public Button retryButton;
    public Button resetHighscore;

    private float score;
    private Player player;
    private Spawner spawner;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }

    private void OnDestroy()
    {
        if(Instance == this)
        { 
            Instance = null;
        }
    }


    private void Start()
    {
        player = FindObjectOfType<Player>();
        spawner = FindObjectOfType<Spawner>();
      
        NewGame();
    }

    public void NewGame()
    {
        Obstacle[] obstacles = FindObjectsOfType<Obstacle>();

        foreach(Obstacle obstacle in obstacles)
        {
            Destroy(obstacle.gameObject);
        }

        gameSpeed = initalGameSpeed;
        score = 0f;
        enabled = true;

        player.gameObject.SetActive(true);
        spawner.gameObject.SetActive(true);

        gameOverText.gameObject.SetActive(false);
        retryButton.gameObject.SetActive(false);
        resetHighscore.gameObject.SetActive(false);

        UpdateHighScore();
    }

    public void GameOver()
    {
        gameSpeed = 0f;
        enabled = false;

        //player.enabled = false; This doesn't work idrk why

        player.gameObject.SetActive(false);
        spawner.gameObject.SetActive(false);

        gameOverText.gameObject.SetActive(true);
        retryButton.gameObject.SetActive(true);
        resetHighscore.gameObject.SetActive(true);

        UpdateHighScore();
    }

    private void Update()
    {
        gameSpeed += gameSpeedIncrease * Time.deltaTime;

        score += gameSpeed * Time.deltaTime;

        scoreText.text = Mathf.FloorToInt(score).ToString("D5");
    }

    private void UpdateHighScore()
    {
        //player prefs for store data
        float hiscore = PlayerPrefs.GetFloat("hiscore", 0);

        if (score > hiscore)
        {
            hiscore = score;
            PlayerPrefs.SetFloat("hiscore", hiscore);
        }

        hiscoreText.text = Mathf.FloorToInt(hiscore).ToString("D5");
    }

    public void ResetHighscore()
    {
        PlayerPrefs.DeleteKey("hiscore");
        hiscoreText.text = "00000";
    }
}
