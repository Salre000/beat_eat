using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AchievementsManager
{
    private static AchievementsAll _achievements;

    private static List<int> _activeCount;

    private static List<bool> _activeFlags;

    public static void Initialize()
    {
        _achievements = Resources.Load<AchievementsAll>("AchievementsAll");
        _activeFlags= new List<bool>(_achievements.achievements.Count);
        _activeCount = new List<int>(_achievements.achievements.Count);



    }

    public static void AddActiveCount(int ID) 
    {
        _activeCount[ID]++;

        if (_activeCount[ID] < _achievements.achievements[ID].AchievementsMAXCount) return;

        _activeFlags[ID] = true;
    }

    //ŽÀÑ–¼ˆË‘¶‚Ì”Ô†Žæ“¾
    public static int GetID(string name) 
    {
        for(int i=0;i< _achievements.achievements.Count; i++) 
        {
            if (_achievements.achievements[i].name != name) continue;
            return i;
        }

        return -1;



    }

}
