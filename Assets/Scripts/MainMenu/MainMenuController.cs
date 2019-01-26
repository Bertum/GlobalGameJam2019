using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
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

    }   //TODO show the credits
}
