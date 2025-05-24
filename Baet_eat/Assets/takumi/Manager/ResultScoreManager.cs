using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResultScoreManager : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI MusicName;
    [SerializeField] TextMeshProUGUI DifficultyName;
    // Start is called before the first frame update
    void Start()
    {
        //�y�Ȃ̖��O������
        MusicName.text = Resources.Load<MusicDataBase>(SaveData.MusicDataName).musicData[ScoreStatus.nowMusic].name;

        DifficultyName.text= ScoreStatus.nowDifficulty.ToString();

        //�����ł��̋Ȃ̕ۑ��󋵂�����
        ScoreStatus.SetDessertScore(ScoreStatus.nowMusic, ScoreStatus.nowDifficulty,(int)InGameStatus.GetScore());

        ScoreStatus.SetDessertClearRanks(ScoreStatus.nowMusic, ScoreStatus.nowDifficulty, InGameStatus.GetScoreClearRank((int)InGameStatus.GetScore()));


        SaveData.SaveFoundation();


    }
}
