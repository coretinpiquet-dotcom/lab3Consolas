using UnityEngine;
using TMPro;
using Tanks.Complete;

public class MultiPlayerManager : MonoBehaviour
{
    [SerializeField] private SimplePlayer[] players;
    [SerializeField] private Transform[] playerSpawnPoint;
    [SerializeField] private TextMeshProUGUI[] scoreText;
    // [SerializeField] private TextMeshProUGUI[] livesText;
    // [SerializeField] private int livesPerPlayer = 3;

    private bool CheckIfOnlyTwoPlayers()
    {
        return players.Length == 2 && playerSpawnPoint.Length == 2 && scoreText.Length == 2;
    }

    public void MultiPlayerStart()
    {
        if (!CheckIfOnlyTwoPlayers())
        {
            Debug.LogError("Multiplayer mode requires exactly two players, two spawn points, two score texts, and two lives texts.");
            return;
        }
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i] != null && playerSpawnPoint[i] != null)
                players[i] = Instantiate(players[i], playerSpawnPoint[i].position, playerSpawnPoint[i].rotation);
        }
        DesactivateScoreTexts();
    }

    public void DesactivateScoreTexts()
    {
        foreach (var score in scoreText)
            if (score != null)
                score.gameObject.SetActive(false);
    }

    public void MultiPlayerUpdate()
    {
        if (!CheckIfOnlyTwoPlayers())
            return;
        if (players[0].GetLife() <= 0)
        {
            GameWon(1);
            GameLost(0);
        }
        else if (players[1].GetLife() <= 0)
        {
            GameWon(0);
            GameLost(1);
        }
    }

    private void GameWon(int winningPlayerIndex)
    {
        if (!CheckIfOnlyTwoPlayers())
            return;
        if (scoreText[winningPlayerIndex] != null)
        {
            scoreText[winningPlayerIndex].gameObject.SetActive(true);
            scoreText[winningPlayerIndex].text = $"Player {winningPlayerIndex + 1} Wins!";
        }
        Time.timeScale = 0f;
    }

    private void GameLost(int losingPlayerIndex)
    {
        if (!CheckIfOnlyTwoPlayers())
            return;
        if (scoreText[losingPlayerIndex] != null)
        {
            scoreText[losingPlayerIndex].gameObject.SetActive(true);
            scoreText[losingPlayerIndex].text = $"Player {losingPlayerIndex + 1} Loses!";
        }
        Time.timeScale = 0f;
    }
}
