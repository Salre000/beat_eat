using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementsManager : MonoBehaviour
{
    private AchievementsAll _achievements;

    private List<int> _activeCount;

    private List<bool> _activeFlags;
    [SerializeField] GameObject achievementPrefab;
    [SerializeField]Canvas achievementCanvas;
    private GameObject objectRoot;
    private List<GameObject> achievementObjects;
    public void Awake()
    {
        _achievements = Resources.Load<AchievementsAll>("Achievements/AchievementsAll");
        achievementObjects = new List<GameObject>(_achievements.achievements.Count);
        _activeFlags = new List<bool>(_achievements.achievements.Count);
        _activeCount = new List<int>(_achievements.achievements.Count);
        objectRoot= achievementCanvas.transform.GetChild(2).transform.GetChild(0).gameObject;
        for (int i=0;i< _achievements.achievements.Count; i++) 
        {
            GameObject achievement = Instantiate(achievementPrefab,objectRoot.transform);

            Vector3 Addpos = Vector3.zero;

            Addpos.y = -i * 30;

            achievement.transform.localPosition = Addpos;

            achievementObjects.Add(achievement);
        }

    }
    private void FixedUpdate()
    {
        if (!achievementCanvas.gameObject.activeSelf) return;
        Move(1);
    }
    public void AddActiveCount(int ID)
    {
        _activeCount[ID]++;

        if (_activeCount[ID] < _achievements.achievements[ID].AchievementsMAXCount) return;

        _activeFlags[ID] = true;
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

    private void Move(float moveAdd) 
    {
        for (int i=0;i< achievementObjects.Count;i++) 
        {

            Vector3 pos= achievementObjects[i].transform.localPosition;

            pos.y += moveAdd;
            achievementObjects[i].transform.localPosition = pos;
        }

    }

}
