using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultScoreManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //‚±‚±‚Å‚»‚Ì‹È‚Ì•Û‘¶ó‹µ‚ğ“ü‚ê‚é
        ScoreStatus.SetDessertScore(ScoreStatus.nowMusic, ScoreStatus.nowDifficulty,(int)InGameStatus.GetScore());

        ScoreStatus.SetDessertClearRanks(ScoreStatus.nowMusic, ScoreStatus.nowDifficulty, InGameStatus.GetScoreClearRank((int)InGameStatus.GetScore()));


        SaveData.SaveFoundation();


    }
}
