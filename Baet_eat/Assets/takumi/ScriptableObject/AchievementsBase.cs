using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementsBase : ScriptableObject 
{
    [SerializeField, Header("���т̖��O")] private string AchievementsName;
    [SerializeField, Header("���т̐���")] private string AchievementsExplanation;
    [SerializeField, Header("���т̏����̐���")] private string ConditionExplanation;
    [SerializeField, Header("�����𖞂����K�v�̂����")] private int AchievementsMAXCount;
    [SerializeField, Header("���т��������Ă��邩")] private�@bool AchievementsFlag;
    [SerializeField, Header("���̎��тɊ���U��ꂽID")] private int AchievementsID;
    [SerializeField, Header("���̎��т̉摜")] private Sprite AchievementsImage;




}