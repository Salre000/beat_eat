using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "CreateAchievementsAll", menuName = "アチーブメントを纏めるオブジェクトを生成")]

public class AchievementsAll : ScriptableObject
{
    [SerializeField]public List<AchievementsBase> achievements;
    
}