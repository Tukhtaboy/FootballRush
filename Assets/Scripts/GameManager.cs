using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public int score = 0;
    public int level = 1;
    public float timeLeft = 30f;
    private int highScore;

    public TMP_Text scoreText;
    public TMP_Text timerText;
    public TMP_Text levelText;
    public TMP_Text messageText;
    public TMP_Text highScoreText;

    public DefenderAI defenderAI;
   
    public AudioSource audioSource;
    public AudioClip goalSound;
    public AudioClip caughtSound;
    public AudioClip winSound;
    public AudioSource backgroundMusic;

    public Transform player;
    public Transform ball;
    public Transform defender;

    public GameObject defender2;
    public GameObject defender3;
    public GameObject restartButton;
    private Vector3 playerStartPos;
    private Vector3 ballStartPos;
    private Vector3 defenderStartPos;

    private Vector3 defender2StartPos;
    private Vector3 defender3StartPos;

    void Start()
{
    score = 0;
    level = 1;
    timeLeft = 30f;

    Time.timeScale = 1f;
    restartButton.SetActive(false);

    playerStartPos = player.position;
    ballStartPos = ball.position;
    defenderStartPos = defender.position;
    if (defender2 != null)
    {
        defender2StartPos = defender2.transform.position;
    }

    if (defender3 != null)
    {
        defender3StartPos = defender3.transform.position;
    }

    highScore = PlayerPrefs.GetInt("HighScore", 0);
    highScoreText.text = "High Score: " + highScore;

    if (messageText != null)
    {
        messageText.text = "";
    }

    UpdateUI();
}

    void Update()
{
    // ESC pauses and resumes the game
    if (Input.GetKeyDown(KeyCode.Escape))
    {
        if (Time.timeScale == 1f)
        {
            Time.timeScale = 0f;
            
            if (backgroundMusic != null)
                backgroundMusic.Pause();

            messageText.text = "PAUSED";
        }
        else
        {
            Time.timeScale = 1f;
            
            if (backgroundMusic != null)
              backgroundMusic.UnPause();
            
            messageText.text = "";
        }
    }

    if (Time.timeScale == 0f)
        return;

    timeLeft -= Time.deltaTime;
    UpdateTimerText();

    if (timeLeft <= 0)
    {
        PlayerCaught();
    }
}
    public void AddScore()
    {
        score++;

        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();
        }

            level++;
            timeLeft = 30f;

        if (audioSource != null && goalSound != null)
        {
            audioSource.PlayOneShot(goalSound);
        }

        if (level > 3)
        {
           messageText.text = "You Win!";

           if (audioSource != null && winSound != null)
           {
               audioSource.PlayOneShot(winSound);
           }
           if (backgroundMusic != null)
           {
               backgroundMusic.Stop();
           } 
           
           ResetPositions();

           restartButton.SetActive(true);
           Time.timeScale = 0f;
           return;
    }

        if (defenderAI != null)
        {
            defenderAI.speed += 0.5f;
        }

        if (level >= 2 && defender2 != null)
        {
            defender2.SetActive(true);
        }

        if (level >= 3 && defender3 != null)
        {
            defender3.SetActive(true);
        }

        ResetPositions();
        UpdateUI();
    }

    public void PlayerCaught()
    {
        if (audioSource != null && caughtSound != null)
        {
            audioSource.PlayOneShot(caughtSound);
        }
        if (backgroundMusic != null)
        {
            backgroundMusic.Stop();
        }

        messageText.text = "Game Over!";
        ResetPositions();
        restartButton.SetActive(true);
        UpdateUI();
        Time.timeScale = 0f;
        
    }

    void ResetPositions()
   {
       player.position = playerStartPos;
       ball.position = ballStartPos;
       defender.position = defenderStartPos;

       if (defender2 != null)
       {
           defender2.transform.position = defender2StartPos;
       }

       if (defender3 != null)
       {
           defender3.transform.position = defender3StartPos;
       }

       Rigidbody2D ballRb = ball.GetComponent<Rigidbody2D>();

       if (ballRb != null)
       {
           ballRb.linearVelocity = Vector2.zero;
           ballRb.angularVelocity = 0f;
       }
    }
    void UpdateUI()
    {
        scoreText.text = "Score: " + score;
        timerText.text = "Time: " + Mathf.Ceil(timeLeft);
        levelText.text = "Level: " + level;
        highScoreText.text = "High Score: " + highScore;
    }

    void UpdateTimerText()
    {
        timerText.text = "Time: " + Mathf.Ceil(timeLeft);
    }
    public void RestartGame()
   {
    Time.timeScale = 1f;
    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}