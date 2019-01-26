using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public GameObject buttons;
    public GameObject credits;
    private void LoadGame()
    {
        PlayerPrefs.Save();
        SceneManager.LoadSceneAsync(GameConfiguration.GAMESCENE);
    }

    public void OnePlayer()
    {
        PlayerPrefs.SetInt("Players", 1);
        LoadGame();
    }

    public void ThreePlayer()
    {
        PlayerPrefs.SetInt("Players", 3);
        LoadGame();
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ShowCredits()
    {
        buttons.SetActive(false);
        credits.SetActive(true);
    }

    public void CloseCredits()
    {
        credits.SetActive(false);
        buttons.SetActive(true);
    }
}
