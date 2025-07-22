using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JudgmentImageManager : MonoBehaviour
{
    [SerializeField, Header("DC,D,Y,G,MÇÃèáî‘")] GameObject[] JudgmentObjects;
    [SerializeField, Header("fast lestÇÃèáî‘")] GameObject[] JudgmentSpeedObjects;

    private GameObject nowJudgmentObject;

    private Vector2 ImagePos = Vector2.zero;

    public readonly float UP_SIZE = 0.25f;

    private float time = 0;
    private readonly float MAX_TIME = 3.0f;

    private readonly float MAXCOUNT = 50;
    
    private List<List<GameObject>> judgmentPool = new List<List<GameObject>>();
    private List<List<GameObject>> judgmentSpeedPool = new List<List<GameObject>>();


    public void Awake()
    {
        Judgment.parent = gameObject;
        JudgmentImageUtility.judgmentImageManager = this;
        Transform parent = JudgmentObjects[0].transform.parent;

        for (int i = 0; i < JudgmentObjects.Length; i++)
        {
            JudgmentObjects[i].transform.localScale = new Vector3(UP_SIZE, UP_SIZE, 0);

            JudgmentObjects[i].SetActive(false);

            judgmentPool.Add(new List<GameObject>());

            for (int j = 0; j < MAXCOUNT; j++) judgmentPool[i].Add(GameObject.Instantiate(JudgmentObjects[i], parent));
        }

        judgmentSpeedPool.Add(new List<GameObject>());
        judgmentSpeedPool.Add(new List<GameObject>());
        JudgmentSpeedObjects[0].SetActive(false);
        JudgmentSpeedObjects[1].SetActive(false);
        for (int i = 0; i < MAXCOUNT; i++) 
        {

            judgmentSpeedPool[0].Add(GameObject.Instantiate(JudgmentSpeedObjects[1], parent));
            judgmentSpeedPool[1].Add(GameObject.Instantiate(JudgmentSpeedObjects[0], parent));

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
    private GameObject GetJudgmentSpeedImage(int index)
    {
        List<GameObject> gameObjects = judgmentSpeedPool[index];
        for (int i = 0; i < gameObjects.Count; i++)
        {

            if (gameObjects[i].activeSelf) continue;

            gameObjects[i].SetActive(true);

            return gameObjects[i];

        }
        return null;

    }
    public void SetImagePos(Vector2 pos) { ImagePos = pos; }
    public void SetNowJudgmentObject(int index, int index2)
    {
        nowJudgmentObject = GetJudgmentImage(index);
        nowJudgmentObject.transform.localScale = new Vector3(UP_SIZE, UP_SIZE, 0);
        time = 0;
        nowJudgmentObject.SetActive(true);

        if (OptionStatus.GetNotesTouchPos())
        {
            nowJudgmentObject.transform.localPosition = new Vector3(Screen.width / 2, Screen.height / 2,0) + new Vector3(-Screen.width / 2, OptionStatus.GetNotesTouchOffset() * 30 + 30 - Screen.height / 2, 0);
        }
        else
        {

            nowJudgmentObject.transform.localPosition = ImagePos + new Vector2(-Screen.width / 2, OptionStatus.GetNotesTouchOffset() * 30 + 100 - Screen.height / 2);
        }

        if (index <= 0) return;

        GameObject speed= GetJudgmentSpeedImage(index2);

        speed.transform.parent = nowJudgmentObject.transform;

        speed.transform.localPosition = new Vector3 (0, -50, 0);
        speed.transform.localScale = new Vector3(UP_SIZE*1.5f, UP_SIZE* 1.5f, 0);

        nowJudgmentObject.GetComponent<Judgment>().SetEndAction(() => { speed.transform.parent = gameObject.transform; });


    }

}
