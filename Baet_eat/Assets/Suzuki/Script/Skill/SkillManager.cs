using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillManager : MonoBehaviour
{
    public static SkillManager instance;
    // �X�L�����^�b�v�őI���������𔻒�
    private bool _isSelected = false;
    private RectTransform _closest = null;
    private int _selectedSkillID = -1;
    public const int SKILLLIST_CAPACITY = 5;
    /// <summary>
    /// (0 = �N���e�B�J������3��) :
    /// (1 = �̗�250�ȉ���500��) :
    /// (2 = �I�[�g����):
    /// </summary>
    public static List<bool> isSkillActiveFlags = new(SKILLLIST_CAPACITY);
    [SerializeField, Header("�X�L���ƂȂ���̑S��")]
    private List<GameObject> _skillCards = new(SKILLLIST_CAPACITY);

    public readonly CriticalJudgmentExpands criticalJudgmentExpands = new CriticalJudgmentExpands();
    public readonly HeelHp heelHp = new HeelHp();
    public readonly Aoto aoto = new Aoto();
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);

        Initialize();
    }
    private void Initialize()
    {
        for (int i = 0; i < SKILLLIST_CAPACITY; i++)
            isSkillActiveFlags.Add(false);
        criticalJudgmentExpands.Initialize();
        heelHp.Initialize();
        aoto.Initialize();
    }

    public List<GameObject> GetSkillCards() { return _skillCards; }
    // �^�b�v�Ō��m
    public bool IsSelected() { return _isSelected; }
    // �^�b�v�����������炤
    public void SetSelected(bool isSelected) { _isSelected = isSelected; }
    // �������{�^���𓮂������߂ɕԂ�
    public RectTransform GetClosest() { return _closest; }
    // �Ώۂ��擾
    public void SetClosest(RectTransform closest) { _closest = closest; }
    // �I�΂�Ă���X�L����Ԃ�
    public int GetSelectedSkillID() { return _selectedSkillID; }
    // �ǂ̃X�L�����I��΂�Ă��邩�Z�b�g
    public void SetSelectedSkillID(int selectSkillID) { _selectedSkillID = selectSkillID; }
    // �X�L���̃A�N�e�B�u�Ɣ�A�N�e�B�u���Z�b�g����
    public void SetIsSkillActiveFlags(int i,bool flag=false) { isSkillActiveFlags[i] = flag;}
}
