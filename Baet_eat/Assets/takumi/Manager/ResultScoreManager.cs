using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultScoreManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //�����ł��̋Ȃ̕ۑ��󋵂�����
        ScoreStaus.SetDessertScore(ScoreStaus.nowMusi, ScoreStaus.nowDifficulty,(int)InGameStatus.GetScore());



        SaveData.SaveFoundation();


    }
}
