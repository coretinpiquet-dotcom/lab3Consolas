using UnityEngine;
using TMPro;

public class SinglePlayerManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Transform playerSpawnPoint;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private float gameDuration = 300f;

    public void SinglePlayerStart()
    {
        if (player != null && playerSpawnPoint != null)
            player = Instantiate(player, playerSpawnPoint.position, playerSpawnPoint.rotation);
        DesactivateScoreText();
    }

    public void DesactivateScoreText()
    {
        if (scoreText != null)
            scoreText.gameObject.SetActive(false);
    }

    public void SinglePlayerUpdate()
    {
        if (isGameWon())
        {
            GameWon();
        }
        else if (isGameLost())
        {
            GameLost();
        }
    }

    private void GameWon()
    {
        if (scoreText != null)
        {
            scoreText.gameObject.SetActive(true);
            scoreText.text = "You Win!";
        }
        Time.timeScale = 0f;
    }

    private void GameLost()
    {
        if (scoreText != null)
        {
            scoreText.gameObject.SetActive(true);
            scoreText.text = "Time's Up!\nYou Lose!";
        }
        Time.timeScale = 0f;
    }

    private bool isGameWon()
    {
        return !AreEnemiesRemaining();
    }

    private bool isGameLost()
    {
        return gameDuration <= 0f || (player != null && player.GetComponent<SimplePlayer>().GetLife() <= 0);
    }

    private bool AreEnemiesRemaining()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        return enemies.Length > 0;
    }

    public void SinglePlayerFixedUpdate()
    {
        if (timerText != null)
        {
            if (gameDuration > 0f)
            {
                gameDuration -= Time.fixedDeltaTime;
                int minutes = Mathf.FloorToInt(gameDuration / 60f);
                int seconds = Mathf.FloorToInt(gameDuration % 60f);
                timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            }
            else
            {
                timerText.text = "00:00";
            }
        }
    }
}
