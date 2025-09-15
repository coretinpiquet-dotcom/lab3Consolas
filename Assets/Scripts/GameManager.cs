using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private SinglePlayerManager singlePlayerManager;
    [SerializeField] private MultiPlayerManager multiPlayerManager;
    Gameplay currentMode = Gameplay.Undefined;

    private void Awake()
    {
        currentMode = GameSingleton.Instance.GetCurrentGameplayMode();
        if (singlePlayerManager)
            singlePlayerManager.DesactivateScoreText();
        if (multiPlayerManager)
            multiPlayerManager.DesactivateScoreTexts();
    }

    private void Start()
    {
        switch (currentMode)
        {
            case Gameplay.SinglePlayer:
                singlePlayerManager.SinglePlayerStart();
                break;
            case Gameplay.MultiPlayer:
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