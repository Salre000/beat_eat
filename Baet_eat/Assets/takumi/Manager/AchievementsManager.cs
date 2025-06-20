using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AchievementsManager : MonoBehaviour
{
    private AchievementsAll _achievements;

    private AchievementsBase select;

    private List<int> _activeCount;

    private List<bool> _activeFlags;
    [SerializeField] GameObject achievementPrefab;
    [SerializeField]Canvas achievementCanvas;
    private GameObject objectRoot;
    private List<GameObject> achievementObjects;



    private TextMeshProUGUI name;

    private TextMeshProUGUI explanation;
    private TextMeshProUGUI Condition;

    [SerializeField]private int targetID = 0;

    public void Awake()
    {
        _achievements = Resources.Load<AchievementsAll>("Achievements/AchievementsAll");
        achievementObjects = new List<GameObject>(_achievements.achievements.Count);
        _activeFlags = new List<bool>(_achievements.achievements.Count);
        _activeCount = new List<int>(_achievements.achievements.Count);
        objectRoot= achievementCanvas.transform.GetChild(2).transform.GetChild(0).transform.GetChild(0).gameObject;
        for (int i = 0; i < _achievements.achievements.Count; i++)
        {
            GameObject achievement = Instantiate(achievementPrefab, objectRoot.transform);

            Vector3 Addpos = Vector3.zero;

            Addpos.y = -i * 120;

            achievement.transform.localPosition = Addpos;

            achievement.GetComponent<Button>().onClick.AddListener(() =>{});

            achievementObjects.Add(achievement);
        }

        name= achievementCanvas.transform.GetChild(3).transform.GetChild(1).transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        explanation= achievementCanvas.transform.GetChild(3).transform.GetChild(4).transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        Condition= achievementCanvas.transform.GetChild(3).transform.GetChild(6).transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        Condition.text= _achievements.achievements[0].ConditionExplanation;
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

    //ŽÀÑ–¼ˆË‘¶‚Ì”Ô†Žæ“¾
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
        name.text = _achievements.achievements[ID].AchievementsName;
        explanation.text = _achievements.achievements[ID].AchievementsExplanation;
        Condition.text = _achievements.achievements[ID].ConditionExplanation;
    }

}
