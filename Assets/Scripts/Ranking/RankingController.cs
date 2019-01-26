using System;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RankingController : MonoBehaviour
{
    private bool gameFinished, savedScore;
    private Ranking ranking;
    private RankingData rankingList;
    private LoadSaveRanking loadSaveRanking;
    public GameObject RankingPanel;
    public Text InputFieldText;
    public Text Score;

    private void Awake()
    {
        Time.timeScale = 1;
        RankingPanel.SetActive(false);
        loadSaveRanking = GetComponent<LoadSaveRanking>();
        rankingList = loadSaveRanking.LoadFile();
        gameFinished = false;
        savedScore = false;
        ranking = new Ranking();
    }

    void Update()
    {
        if (!gameFinished)
        {
            ranking.Time += Time.deltaTime;
        }
    }

    public void FinishGame()
    {
        if (!gameFinished)
        {
            Time.timeScale = 0;
            gameFinished = true;
            Score.text = "Your score: " + ranking.Time.ToString("0.00");
            RankingPanel.SetActive(true);
        }
    }

    /// <summary>
    /// Should be call from the Submit button in the RankingPanel
    /// </summary>
    public void SaveRanking()
    {
        if (!savedScore)
        {
            savedScore = true;
            ranking.Date = DateTime.Now;
            ranking.PlayerName = InputFieldText.text;
            rankingList.rankings.Add(ranking);
            if (rankingList.rankings.Count > GameConfiguration.MAX_RANKING_RECORDS)
            {
                rankingList.rankings.OrderByDescending(o => o.Time).ThenBy(o => o.Date);
                //Remove the ranking with less time
                rankingList.rankings.RemoveAt(0);
            }
            loadSaveRanking.SaveFile(rankingList);
            SceneManager.LoadSceneAsync(GameConfiguration.MAINMENUSCENE);
        }

    }
}
