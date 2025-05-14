using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JudgmentImageManager : MonoBehaviour
{
    [SerializeField, Header("DC,D,Y,G,MÇÃèáî‘")] GameObject[] JudgmentObjects;

    private GameObject nowJudgmentObject;

    private readonly float UP_SIZE = 0.15f;

    private float time =0;
    private readonly float MAX_TIME = 3.0f;

    public void Awake()
    {
        JudgmentImageUtility.judgmentImageManager = this;
        for(int i=0;i< JudgmentObjects.Length; i++) 
        {
            JudgmentObjects[i].transform.localScale = new Vector3(UP_SIZE, UP_SIZE, 0);

            JudgmentObjects[i].SetActive(false);


        }
    }
    public void FixedUpdate()
    {
        time += Time.deltaTime;

        if (MAX_TIME < time) 
        {
            time = 0;
            nowJudgmentObject = null;
        }

        if (nowJudgmentObject == null) return;

        if (nowJudgmentObject.transform.localScale.x >= 1) return;

        nowJudgmentObject.transform.localScale += new Vector3(UP_SIZE, UP_SIZE, 0);



    }

    public void SetNowJudgmentObject(int index)
    {
        for (int i = 0; i < JudgmentObjects.Length; i++)
        {
            JudgmentObjects[i].SetActive(false);
        }
        nowJudgmentObject = JudgmentObjects[index];
        nowJudgmentObject.transform.localScale = new Vector3(UP_SIZE, UP_SIZE, 0);


        nowJudgmentObject.SetActive(true);
    }

}
