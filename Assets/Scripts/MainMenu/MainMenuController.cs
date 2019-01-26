using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public GameObject buttons;
    public GameObject credits;
    public GameObject ranking;
    public GameObject difficultySelector;

    private void Awake()
    {
        credits.SetActive(false);
        ranking.SetActive(false);
        difficultySelector.SetActive(false);
    }

    private void LoadGame()
    {
        PlayerPrefs.Save();
        SceneManager.LoadSceneAsync(GameConfiguration.GAMESCENE);
    }

    public void OnePlayer()
    {
        PlayerPrefs.SetInt("Players", 1);
        ShowDifficultySelector();
    }

    public void ThreePlayer()
    {
        PlayerPrefs.SetInt("Players", 3);
        ShowDifficultySelector();
    }

    public void SetDifficulty(int difficulty)
    {
        PlayerPrefs.SetInt("Difficulty", difficulty);
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

    public void ShowButtons()
    {
        difficultySelector.SetActive(false);
        ranking.SetActive(false);
        credits.SetActive(false);
        buttons.SetActive(true);
    }

    public void ShowRanking()
    {
        buttons.SetActive(false);
        ranking.SetActive(true);
    }

    public void ShowDifficultySelector()
    {
        buttons.SetActive(false);
        difficultySelector.SetActive(true);
    }
}
