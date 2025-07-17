using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JudgmentImageManager : MonoBehaviour
{
    [SerializeField, Header("DC,D,Y,G,MÇÃèáî‘")] GameObject[] JudgmentObjects;

    private GameObject nowJudgmentObject;

    private Vector2 ImagePos = Vector2.zero;

    private readonly float UP_SIZE = 0.25f;

    private float time = 0;
    private readonly float MAX_TIME = 3.0f;

    private readonly float MAXCOUNT = 50;
    private List<List<GameObject>> judgmentPool = new List<List<GameObject>>();



    public void Awake()
    {
        JudgmentImageUtility.judgmentImageManager = this;
        Transform parent = JudgmentObjects[0].transform.parent;

        for (int i = 0; i < JudgmentObjects.Length; i++)
        {
            JudgmentObjects[i].transform.localScale = new Vector3(UP_SIZE, UP_SIZE, 0);

            JudgmentObjects[i].SetActive(false);

            judgmentPool.Add(new List<GameObject>());

            for (int j = 0; j < MAXCOUNT; j++) judgmentPool[i].Add(GameObject.Instantiate(JudgmentObjects[i], parent));
        }





    }
    public void FixedUpdate()
    {
        if (nowJudgmentObject == null) return;

        time += Time.deltaTime;

        if (MAX_TIME < time)
        {
            time = 0;
            nowJudgmentObject.SetActive(false);
            nowJudgmentObject = null;

            return;
        }


        if (nowJudgmentObject.transform.localScale.x >= 1) return;

        nowJudgmentObject.transform.localScale += new Vector3(UP_SIZE, UP_SIZE, 0);



    }


    private GameObject GetJudgmentImage(int index)
    {
        List<GameObject> gameObjects = judgmentPool[index];

        for (int i = 0; i < gameObjects.Count; i++)
        {

            if (gameObjects[i].activeSelf) continue;

            return gameObjects[i];

        }
        return null;

    }
    public void SetImagePos(Vector2 pos) { ImagePos = pos; }
    public void SetNowJudgmentObject(int index)
    {
        nowJudgmentObject = GetJudgmentImage(index);
        nowJudgmentObject.transform.localScale = new Vector3(UP_SIZE, UP_SIZE, 0);
        time = 0;
        nowJudgmentObject.SetActive(true);

        if (OptionStatus.GetNotesTouchPos())
        {
            nowJudgmentObject.transform.localPosition = Vector3.zero + new Vector3(-Screen.width / 2, OptionStatus.GetNotesTouchOffset() * 30 + 30 - Screen.height / 2, 0);
        }
        else
        {

            nowJudgmentObject.transform.localPosition = ImagePos + new Vector2(-Screen.width / 2, OptionStatus.GetNotesTouchOffset() * 30 + 100 - Screen.height / 2);
        }
    }

}
