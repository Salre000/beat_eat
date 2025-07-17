using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AchievementStatus
{

    public static Achievements achievements;

    public static List<int> achivementMaxCount = new List<int>();

    public static int achievementNumber = -1;


    public static List<int> achievementNumberList = new List<int>();

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

        //初めてゲームを起動した
        Achievement(AchievementTypeEnum.AchievementType._StartGame);
    }


    public static void Achievement(AchievementTypeEnum.AchievementType type)
    {
        int Index = (int)type;

        if (achievements.GetAChiveMentStatus(Index)) return;

        achievements.AddAChiveMentCount(Index);

        if (achievements.GetAChiveMentCount(Index) < achivementMaxCount[Index]) return;

        achievements.SetAChiveMentStatus(Index);

        //ゲームの途中だったらリストに保存　そうじゃなかったら今すぐに描画
        if (LineUtility.gameManager == null)
        {
            achievementNumber = Index;
            SceneManager.LoadScene("AchievementScene", LoadSceneMode.Additive);
        }
        else
        {
            achievementNumberList.Add(Index);
        }
        //アチーブメントのデータを保存
        SaveData.SaveAchiveMent();

    }
    public static void AchieventSystemSet(AchievementTypeEnum.AchievementType type)
    {
        int Index = (int)type;

        if (achievements.GetAChiveMentStatus(Index)) return;

        achievements.AddAChiveMentCount(Index);

        if (achievements.GetAChiveMentCount(Index) < achivementMaxCount[Index]) return;

        achievements.SetAChiveMentStatus(Index);

        achievementNumberList.Add(Index);
        //アチーブメントのデータを保存
        SaveData.SaveAchiveMent();

    }

    public static List<int> GetAchievementNumber() { return achievementNumberList; }
    public static void ResetAchievementNumber() { achievementNumberList.Clear(); }




}