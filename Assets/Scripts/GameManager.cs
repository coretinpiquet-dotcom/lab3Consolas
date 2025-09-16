using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private SinglePlayerManager singlePlayerManager;
    [SerializeField] private MultiPlayerManager multiPlayerManager;
    [SerializeField] private GameObject enemySpawner;
    Gameplay currentMode = Gameplay.Undefined;

    private void Awake()
    {
        currentMode = GameSingleton.Instance.GetCurrentGameplayMode();
        Debug.Log("Current gameplay mode: " + currentMode);
        if (singlePlayerManager)
            singlePlayerManager.DesactivateScoreText();
        if (multiPlayerManager)
            multiPlayerManager.DesactivateScoreTexts();
    }

    private void Start()
    {
        Awake();
        Debug.Log("Starting game in mode: " + currentMode);
        switch (currentMode)
        {
            case Gameplay.SinglePlayer:
                singlePlayerManager.SinglePlayerStart();
                if (enemySpawner)
                    enemySpawner.SetActive(true);
                break;
            case Gameplay.MultiPlayer:
                if (enemySpawner)
                    enemySpawner.SetActive(false);
                multiPlayerManager.MultiPlayerStart();
                break;
            default:
                Debug.Log("No gameplay mode selected.");
                break;
        }
    }

    private void Update()
    {
        switch (currentMode)
        {
            case Gameplay.SinglePlayer:
                singlePlayerManager.SinglePlayerUpdate();
                break;
            case Gameplay.MultiPlayer:
                multiPlayerManager.MultiPlayerUpdate();
                break;
            default:
                Debug.Log("No gameplay mode selected.");
                break;
        }
    }

    private void FixedUpdate()
    {
        switch (currentMode)
        {
            case Gameplay.SinglePlayer:
                singlePlayerManager.SinglePlayerFixedUpdate();
                break;
            case Gameplay.MultiPlayer:
                // multiPlayerManager.MultiPlayerFixedUpdate();
                break;
            default:
                Debug.Log("No gameplay mode selected.");
                break;
        }
    }
}