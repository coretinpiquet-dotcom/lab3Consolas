using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Transform playerSpawnPoint;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private float gameDuration = 300f;

    private void Start()
    {
        if (player != null && playerSpawnPoint != null)
            Instantiate(player, playerSpawnPoint.position, playerSpawnPoint.rotation);
        if (scoreText != null)
            scoreText.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (!AreEnemiesRemaining())
        {
            if (scoreText != null)
            {
                scoreText.gameObject.SetActive(true);
                scoreText.text = "You Win!";
            }
            Time.timeScale = 0f;
        } else if (gameDuration <= 0f)
        {
            if (scoreText != null)
            {
                scoreText.gameObject.SetActive(true);
                scoreText.text = "Time's Up!\nYou Lose!";
            }
            Time.timeScale = 0f;
        }
    }

    private bool AreEnemiesRemaining()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        return enemies.Length > 0;
    }
    
    private void FixedUpdate()
    {
        if (timerText != null)
        {
            gameDuration -= Time.fixedDeltaTime;
            timerText.text = Mathf.Max(0, Mathf.CeilToInt(gameDuration)).ToString() + "s left";
        }
    }
}
