using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

public class AchievementsManager : MonoBehaviour
{
    private AchievementsAll _achievements;

    private AchievementsBase select;

    private List<int> _activeCount;

    private List<bool> _activeFlags;
    [SerializeField] GameObject achievementPrefab;
    [SerializeField] Canvas achievementCanvas;
    [SerializeField] Material sepiaMaterial;
    private GameObject objectRoot;
    private List<GameObject> achievementObjects;


    private TextMeshProUGUI name;

    private TextMeshProUGUI explanation;
    private TextMeshProUGUI Condition;
    private Image image;

    [SerializeField] private int targetID = 0;



    public void Start()
    {
        _achievements = Resources.Load<AchievementsAll>("Achievements/AchievementsAll");
        achievementObjects = new List<GameObject>(_achievements.achievements.Count);
        _activeFlags = new List<bool>(_achievements.achievements.Count);
        _activeCount = new List<int>(_achievements.achievements.Count);
        objectRoot = achievementCanvas.transform.GetChild(2).transform.GetChild(0).transform.GetChild(0).gameObject;

        image= achievementCanvas.transform.GetChild(3).transform.GetChild(0).GetComponent<Image>();

        if (!AchievementStatus.achievements.GetAChiveMentStatus(0))
            image.material = sepiaMaterial;
        
        name = achievementCanvas.transform.GetChild(3).transform.GetChild(1).transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        explanation = achievementCanvas.transform.GetChild(3).transform.GetChild(4).transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        Condition = achievementCanvas.transform.GetChild(3).transform.GetChild(6).transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        Condition.text = _achievements.achievements[0].ConditionExplanation;

        for (int i = 0; i < _achievements.achievements.Count; i++)
        {
            GameObject achievement = Instantiate(achievementPrefab, objectRoot.transform);

            Vector3 Addpos = Vector3.zero;

            Addpos.y = -i * 120;

            achievement.transform.localPosition = Addpos;

            //アチーブメントの中身を記述していく
            //画像
            achievement.transform.GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite =
                _achievements.achievements[i].AchievementsImage;
            //名前
            achievement.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text =
                _achievements.achievements[i].AchievementsName;
            //状況
            achievement.transform.GetChild(2).transform.GetChild(0).gameObject.
                SetActive(AchievementStatus.achievements.GetAChiveMentStatus(i));

            if (!AchievementStatus.achievements.GetAChiveMentStatus(i))
                achievement.transform.GetChild(0).GetComponent<UnityEngine.UI.Image>().material = sepiaMaterial;


            //これ必須
            int number = i;

            achievement.GetComponent<Button>().onClick.AddListener(() => { targetID = number; });
            achievementObjects.Add(achievement);
        }

    }


    private void FixedUpdate()
    {
        if (!achievementCanvas.gameObject.activeSelf) return;

        GetActiveData(targetID);
    }
    public void AddActiveCount(int ID)
    {
        _activeCount[ID]++;

        if (_activeCount[ID] < _achievements.achievements[ID].AchievementsMAXCount) return;

        _activeFlags[ID] = true;
    }

    void ShowAchievement()
    {
        if (select == null) return;
        name.text = select.AchievementsName;

        explanation.text = select.AchievementsExplanation;
        Condition.text = select.ConditionExplanation;
    }

    //実績名依存の番号取得
    public int GetID(string name)
    {
        for (int i = 0; i < _achievements.achievements.Count; i++)
        {
            if (_achievements.achievements[i].name != name) continue;
            return i;
        }

        return -1;
    }

    public void ChengeActive()
    {
        achievementCanvas.gameObject.SetActive(!achievementCanvas.gameObject.activeSelf);
    }

    private void GetActiveData(int ID)
    {
        name.text = !_achievements.achievements[ID].HiddenAchievement ?
            _achievements.achievements[ID].AchievementsName : "???";
        explanation.text = !_achievements.achievements[ID].HiddenAchievement ?
            _achievements.achievements[ID].AchievementsExplanation : "???";
        Condition.text = !_achievements.achievements[ID].HiddenAchievement ?
            _achievements.achievements[ID].ConditionExplanation : "???";

        image.sprite =_achievements.achievements[ID].AchievementsImage;


        if (!AchievementStatus.achievements.GetAChiveMentStatus(ID))
            image.material = sepiaMaterial;
        else image.material = null;


    }




}

[Serializable]
public class Achievements
{

    private List<bool> achiveMentesStatus = new List<bool>();
    private List<int> achiveMentesCount = new List<int>();

    public List<bool> GetAChiveMentStatus() { return achiveMentesStatus; }

    public void AddAchiveMentStatus(bool flag) { achiveMentesStatus.Add(flag); }

    public bool GetAChiveMentStatus(int index) { return achiveMentesStatus[index]; }
    public void SetAChiveMentStatus(int index) { achiveMentesStatus[index] = true; }
    public int GetAChiveMentCount(int index) { return achiveMentesCount[index]; }
    public void AddAChiveMentCount(int index) { achiveMentesCount[index]++; }
    public void AddAChiveMentCount() { achiveMentesCount.Add(0); }


}