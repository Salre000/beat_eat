using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillManager : MonoBehaviour
{
    public static SkillManager instance;
    // スキルをタップで選択したかを判定
    private bool _isSelected = false;
    private RectTransform _closest = null;
    private int _selectedSkillID = -1;
    public const int SKILLLIST_CAPACITY = 5;
    /// <summary>
    /// (0 = クリティカル判定3割) :
    /// (1 = 体力250以下で500回復) :
    /// </summary>
    public static List<bool> isSkillActiveFlagList = new(SKILLLIST_CAPACITY);
    [SerializeField, Header("スキルとなるもの全て")]
    private List<GameObject> _skillCards = new(SKILLLIST_CAPACITY);

    public readonly CriticalJudgmentExpands criticalJudgmentExpands = new CriticalJudgmentExpands();
    public readonly HeelHp heelHp = new HeelHp();

    private void Awake()
    {
        instance = this;
        Initialize();
    }
    private void Initialize()
    {
        for (int i = 0; i < SKILLLIST_CAPACITY; i++)
            isSkillActiveFlagList.Add(false);
        criticalJudgmentExpands.Initialize();
        heelHp.Initialize();
    }

    public List<GameObject> GetSkillCards() { return _skillCards; }
    // タップで検知
    public bool IsSelected() { return _isSelected; }
    // タップしたかをもらう
    public void SetSelected(bool isSelected) { _isSelected = isSelected; }
    // 押したボタンを動かすために返す
    public RectTransform GetClosest() { return _closest; }
    // 対象を取得
    public void SetClosest(RectTransform closest) { _closest = closest; }
    // 選ばれているスキルを返す
    public int GetSelectedSkillID() { return _selectedSkillID; }
    public void SetSelectedSkillID(int selectSkillID) { _selectedSkillID = selectSkillID; }
}
