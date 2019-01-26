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
    private RankingData ranking;

    private void Awake()
    {
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
        Font arialFont = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
        //Sort by higher score and first registered
        ranking.rankings.OrderBy(o => o.Time).ThenByDescending(o => o.Date);
        foreach (var rank in ranking.rankings)
        {
            CreateText(rank.PlayerName, arialFont);
            CreateText(rank.Time.ToString(), arialFont);
        }
    }

    private void CreateText(string text, Font arialFont)
    {
        GameObject newText = new GameObject();
        newText.transform.SetParent(RankingGrid.transform);

        Text myText = newText.AddComponent<Text>();
        myText.font = arialFont;
        myText.material = arialFont.material;
        myText.text = text;
        myText.alignment = TextAnchor.MiddleCenter;
    }
}
