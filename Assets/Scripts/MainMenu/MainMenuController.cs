using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public GameObject Buttons;
    public GameObject Credits;
    public GameObject RankingPanel;
    public GameObject DifficultySelector;
    public GameObject RankingLoadSave;
    public GameObject RankingGrid;
    public Font font;
    private RankingData ranking;

    private void Awake()
    {
        //Load all the ranking
        ranking = RankingLoadSave.GetComponent<LoadSaveRanking>().LoadFile();
        Credits.SetActive(false);
        RankingPanel.SetActive(false);
        DifficultySelector.SetActive(false);
        LoadRankingMenu();
    }

    private void LoadGame()
    {
        PlayerPrefs.Save();
        SceneManager.LoadSceneAsync(GameConfiguration.GAMESCENE);
    }

    public void OnePlayer()
    {
        PlayerPrefs.SetInt(GameConfiguration.PLAYERS, 1);
        ShowDifficultySelector();
    }

    public void ThreePlayer()
    {
        PlayerPrefs.SetInt(GameConfiguration.PLAYERS, 3);
        ShowDifficultySelector();
    }

    public void SetDifficulty(int difficulty)
    {
        PlayerPrefs.SetInt(GameConfiguration.DIFFICULTY, difficulty);
        LoadGame();
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ShowCredits()
    {
        Buttons.SetActive(false);
        Credits.SetActive(true);
    }

    public void ShowButtons()
    {
        DifficultySelector.SetActive(false);
        RankingPanel.SetActive(false);
        Credits.SetActive(false);
        Buttons.SetActive(true);
    }

    public void ShowRanking()
    {
        Buttons.SetActive(false);
        RankingPanel.SetActive(true);
    }

    public void ShowDifficultySelector()
    {
        Buttons.SetActive(false);
        DifficultySelector.SetActive(true);
    }

    private void LoadRankingMenu()
    {
        //Sort by higher score and first registered
        ranking.rankings = ranking.rankings.OrderByDescending(o => o.Time).ThenByDescending(o => o.Date).ToList();
        foreach (var rank in ranking.rankings)
        {
            CreateText(rank.PlayerName);
            CreateText(rank.Time.ToString("0.00"));
        }
    }

    private void CreateText(string text)
    {
        GameObject newText = new GameObject();
        newText.transform.SetParent(RankingGrid.transform);

        Text myText = newText.AddComponent<Text>();
        myText.font = font;
        myText.fontSize = 18;
        myText.material = font.material;
        myText.text = text;
        myText.alignment = TextAnchor.MiddleCenter;
    }
}
