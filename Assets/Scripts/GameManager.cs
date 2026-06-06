using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public int score = 0;
    public int level = 1;
    public float timeLeft = 30f;

    public TMP_Text scoreText;
    public TMP_Text timerText;
    public TMP_Text levelText;
    public TMP_Text messageText;

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

    void Start()
    {
        Time.timeScale = 1f;
        restartButton.SetActive(false);
        playerStartPos = player.position;
        ballStartPos = ball.position;
        defenderStartPos = defender.position;

        if (messageText != null)
        {
            messageText.text = "";
        }

        UpdateUI();
    }

    void Update()
    {
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
        restartButton.SetActive(true);
        Time.timeScale = 0f;
        
    }

    void ResetPositions()
    {
        player.position = playerStartPos;
        ball.position = ballStartPos;
        defender.position = defenderStartPos;
    }

    void UpdateUI()
    {
        scoreText.text = "Score: " + score;
        timerText.text = "Time: " + Mathf.Ceil(timeLeft);
        levelText.text = "Level: " + level;
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