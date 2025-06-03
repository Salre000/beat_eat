using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "CreateAchievements", menuName = "�A�`�[�u�����g�𐶐�")]

public class AchievementsBase : ScriptableObject 
{
    [SerializeField, Header("���т̗񋓑̖�")] public string AchievementsEnumName;
    [SerializeField, Header("���т̖��O")] public string AchievementsName;
    [SerializeField, Header("���т̐���")] public string AchievementsExplanation;
    [SerializeField, Header("���т̏����̐���")] public string ConditionExplanation;
    [SerializeField, Header("�����𖞂����K�v�̂����")] public int AchievementsMAXCount;
    [SerializeField, Header("���̎��т̉摜")] public Sprite AchievementsImage;




}