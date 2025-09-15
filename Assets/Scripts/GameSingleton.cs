using UnityEngine;

public enum Gameplay
{
    SinglePlayer,
    MultiPlayer,
    Undefined
}


public class GameSingleton : MonoBehaviour
{
    public static GameSingleton Instance { get; private set; }
    private Gameplay currentGameplay = Gameplay.Undefined;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SetGameplayMode(string mode)
    {
        if (mode == "SinglePlayer")
            currentGameplay = Gameplay.SinglePlayer;
        else if (mode == "MultiPlayer")
            currentGameplay = Gameplay.MultiPlayer;
        else
            currentGameplay = Gameplay.Undefined;
    }

    public Gameplay GetCurrentGameplayMode()
    {
        return currentGameplay;
    }
}
