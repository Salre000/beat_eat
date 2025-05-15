using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillSelect : MonoBehaviour
{
    [SerializeField, Header("SkillSelectButton")] private GameObject _selectButtonObject;
    private Button _selectButton;

    [SerializeField, Header("�X�L���ƂȂ���̑S��")] private List<Button> _skillButtons = new(SkillManager.SKILLLIST_CAPACITY);
    [SerializeField] private RectTransform _skillMask;
    [SerializeField] private RectTransform _content;
    private bool _isSelect = false;

    // �J�����Ƃ��̖ڕW�n�_
    private readonly Vector3 _skillmaskOpenPosition = new Vector3(0, 0, 0);
    private readonly Vector2 _skillmaskOpenSize = new Vector2(160.0f, 630.0f);
    // ����Ƃ��̖ڕW�n�_
    private readonly Vector3 _skillmaskClosePosition = new Vector3(0, -135.0f, 0);
    private readonly Vector2 _skillmaskCloseSize = new Vector2(160, 138);


    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        _selectButton = _selectButtonObject.GetComponent<Button>();
        _selectButton.onClick.AddListener(IsSelect);

    }

    private void LateUpdate()
    {
        ExecuteSelect();
    }

    // �X�L������񉟂��ŃX�L���I����W�J
    private void ExecuteSelect()
    {
        if (_isSelect)
            OpenSkill();
        else
            CloseSkill();
    }
    // �W�J
    private void OpenSkill()
    {
        _skillMask.localPosition = Vector3.Lerp(_skillMask.localPosition, _skillmaskOpenPosition, Time.deltaTime * 10f);
        _skillMask.sizeDelta = Vector3.Lerp(_skillMask.sizeDelta, _skillmaskOpenSize, Time.deltaTime * 10f);
    }
    // �I�яI���
    private void CloseSkill()
    {
        _skillMask.localPosition = Vector3.Lerp(_skillMask.localPosition, _skillmaskClosePosition, Time.deltaTime * 10f);
        _skillMask.sizeDelta = Vector3.Lerp(_skillMask.sizeDelta, _skillmaskCloseSize, Time.deltaTime * 10f);
    }

    private void IsSelect() { _isSelect = !_isSelect; }
}
