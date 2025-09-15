using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject[] mainMenu;
    [SerializeField] private GameObject[] gameplayMenu;
    [SerializeField] private GameObject[] LevelMenu;
    private String currentMenu;

    private void Start()
    {
        ShowMainMenu();
    }

    public void ShowMainMenu()
    {
        currentMenu = "MainMenu";
        foreach (var menu in mainMenu)
            menu.SetActive(true);

        foreach (var menu in gameplayMenu)
            menu.SetActive(false);

        foreach (var menu in LevelMenu)
            menu.SetActive(false);
    }

    public void ShowGameplayMenu()
    {
        currentMenu = "GameplayMenu";
        foreach (var menu in mainMenu)
            menu.SetActive(false);

        foreach (var menu in gameplayMenu)
            menu.SetActive(true);

        foreach (var menu in LevelMenu)
            menu.SetActive(false);
    }

    public void ShowLevelMenu()
    {
        currentMenu = "LevelMenu";
        foreach (var menu in mainMenu)
            menu.SetActive(false);

        foreach (var menu in gameplayMenu)
            menu.SetActive(false);

        foreach (var menu in LevelMenu)
            menu.SetActive(true);
    }

    public void GoPreviousMenu()
    {
        switch (currentMenu)
        {
            case "GameplayMenu":
                ShowMainMenu();
                break;
            case "LevelMenu":
                ShowGameplayMenu();
                break;
            default:
                ShowMainMenu();
                break;
        }
    }

    public void PlayLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Nivel1");

    }

    public void NextScene()
    {
        SceneManager.LoadScene("Nivel2");
    }

    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }

}


