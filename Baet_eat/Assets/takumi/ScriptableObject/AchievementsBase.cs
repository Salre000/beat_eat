using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementsBase : ScriptableObject 
{
    [SerializeField, Header("実績の名前")] private string AchievementsName;
    [SerializeField, Header("実績の説明")] private string AchievementsExplanation;
    [SerializeField, Header("実績の条件の説明")] private string ConditionExplanation;
    [SerializeField, Header("条件を満たす必要のある回数")] private int AchievementsMAXCount;
    [SerializeField, Header("実績が解放されているか")] private　bool AchievementsFlag;
    [SerializeField, Header("この実績に割り振られたID")] private int AchievementsID;
    [SerializeField, Header("この実績の画像")] private Sprite AchievementsImage;




}