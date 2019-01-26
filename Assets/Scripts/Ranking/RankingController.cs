using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class RankingController : MonoBehaviour
{
    public bool GameFinished;
    private Ranking ranking;
    private RankingData rankingList;
    private LoadSaveRanking loadSaveRanking;
    public GameObject RankingPanel;
    public Text InputFieldText;

    private void Awake()
    {
        RankingPanel.SetActive(false);
        loadSaveRanking = GetComponent<LoadSaveRanking>();
        rankingList = loadSaveRanking.LoadFile();
        GameFinished = false;
        ranking = new Ranking();
    }

    void Update()
    {
        if (!GameFinished)
        {
            ranking.Time += Time.deltaTime;
        }
    }

    public void FinishGame()
    {
        GameFinished = true;
        RankingPanel.SetActive(true);
    }

    /// <summary>
    /// Should be call from the Submit button in the RankingPanel
    /// </summary>
    public void SaveRanking()
    {
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
    }
}
