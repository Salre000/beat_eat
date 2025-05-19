using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{

    public void Awake()
    {
        //initialize‚Ìˆø”‚Í‹È‚Ì”
        ScoreStaus.Initialize(Resources.Load<MusicDataBase>(SaveData.MusicDataName).musicData.Count);

        int i = 0;
    }


    public void FixedUpdate()
    {
        int i = 0;
    }





}
