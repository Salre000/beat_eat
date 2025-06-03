using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "CreateAchievements", menuName = "アチーブメントを生成")]

public class AchievementsBase : ScriptableObject 
{
    [SerializeField, Header("実績の列挙体名")] public string AchievementsEnumName;
    [SerializeField, Header("実績の名前")] public string AchievementsName;
    [SerializeField, Header("実績の説明")] public string AchievementsExplanation;
    [SerializeField, Header("実績の条件の説明")] public string ConditionExplanation;
    [SerializeField, Header("条件を満たす必要のある回数")] public int AchievementsMAXCount;
    [SerializeField, Header("この実績の画像")] public Sprite AchievementsImage;




}