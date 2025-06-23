using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementStatus
{

    public static Achievements achievements;

    public static List<int> achivementMaxCount = new List<int>();


    public static void Initialize()
    {
        LoadData.LoadAchiveMent(() =>
        {

            achievements = new Achievements();

            for (int i = 0; i < (int)AchievementTypeEnum.AchievementType.MAX; i++)
            {
                achievements.AddAchiveMentStatus(false);
                achievements.AddAChiveMentCount();
            }

        });

        AchievementsAll _achievements = Resources.Load<AchievementsAll>("Achievements/AchievementsAll");


        for (int i = 0; i < _achievements.achievements.Count; i++)
        {
            achivementMaxCount.Add(_achievements.achievements[i].AchievementsMAXCount);

        }

        //���߂ăQ�[�����N������
        Achievement(AchievementTypeEnum.AchievementType._StartGame);
    }


    public static void Achievement(AchievementTypeEnum.AchievementType type)
    {
        int Index = (int)type;

        if (achievements.GetAChiveMentStatus(Index)) return;

        achievements.AddAChiveMentCount(Index);

        if (achievements.GetAChiveMentCount(Index) < achivementMaxCount[Index]) return;

        achievements.SetAChiveMentStatus(Index);

        //�A�`�[�u�����g�̃f�[�^��ۑ�
        SaveData.SaveAchiveMent();

    }






}