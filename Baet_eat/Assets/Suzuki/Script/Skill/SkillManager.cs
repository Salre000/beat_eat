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
    [SerializeField] private int _selectedSkillID = -1;
    public const int SKILLLIST_CAPACITY = 5;
    /// <summary>
    /// (0 = クリティカル判定3割) :
    /// (1 = 体力250以下で500回復) :
    /// (2 = オート操作):
    /// </summary>
    public static List<bool> isSkillActiveFlags = new(SKILLLIST_CAPACITY);
    [SerializeField, Header("スキルとなるもの全て")]
    private List<GameObject> _skillCards = new(SKILLLIST_CAPACITY);
    [SerializeField] GameObject content;

    public readonly CriticalJudgmentExpands criticalJudgmentExpands = new CriticalJudgmentExpands();
    public readonly HeelHp heelHp = new HeelHp();
    public readonly Auto auto = new Auto();
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
        //前回のゲームで選択していたスキルを再選択
        content.transform.localPosition += new Vector3(0, 142 * -OptionStatus.GetSkillIndex(), 0);



        for (int i = 0; i < SKILLLIST_CAPACITY; i++)
            isSkillActiveFlags.Add(false);
        criticalJudgmentExpands.Initialize();
        heelHp.Initialize();
        auto.Initialize();

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
    // どのスキルが選らばれているかセット
    public void SetSelectedSkillID(int selectSkillID) { _selectedSkillID = selectSkillID; }
    // スキルのアクティブと非アクティブをセットする
    public void SetIsSkillActiveFlags(int i, bool flag = false) { isSkillActiveFlags[i] = flag; }

    //現在選択中のスキルの説明を返す
    public string GetDescription()
    {
        string description = "";
        if (_selectedSkillID == 0) description = criticalJudgmentExpands.GetDescription();
        if (_selectedSkillID == 1) description = heelHp.GetDescription();
        if (_selectedSkillID == 2) description = auto.GetDescription();
        return description;
    }
}
