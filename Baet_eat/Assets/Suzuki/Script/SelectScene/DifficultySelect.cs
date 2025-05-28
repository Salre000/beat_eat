using NoteEditor.Utility;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DifficultySelect : MonoBehaviour
{
    // ��Փx�̍ő吔
    private const int _DIFFICULTY_MAX = 5;
    [SerializeField, Header("�e���Փx�I�u�W�F�N�g")]
    private List<GameObject> _diffiObject = new(_DIFFICULTY_MAX);
    private List<Button> _diffiButton = new(_DIFFICULTY_MAX);
    private List<GameObject> _diffiColor = new(_DIFFICULTY_MAX);
    private List<TextMeshProUGUI> _diffiText = new(_DIFFICULTY_MAX);
    private int _diffiNumber = 0;

    // ��I�����J���[
    private Color32 _grayColor = new Color32(50, 50, 50, 255);
    // �I�����J���[
    private Color32 _whiteColor = new Color32(255, 255, 255, 255);

    private void Start()
    {
        Initialize();
    }

    // ToDo:�Z���N�g��ʂɗ��邽�эŌ�ɑI��ł�����Փx�ɖ߂�(�\��)
    private void Initialize()
    {
        _diffiNumber = MusicManager.instance.GetDifficultyNumber();
        int forNum = 0;
        foreach (GameObject gameobject in _diffiObject)
        {
            _diffiButton.Add(gameobject.GetComponent<Button>());
            _diffiColor.Add(gameobject.transform.GetChild(0).gameObject);
            _diffiText.Add(gameobject.transform.GetChild(1).GetComponent<TextMeshProUGUI>());
            if (forNum == 0)
            {
                _diffiText[forNum].color = _whiteColor;
                _diffiColor[forNum].SetActive(true);
            }
            else
            {
                _diffiText[forNum].color = _grayColor;
                _diffiColor[forNum].SetActive(false);
            }

            switch (forNum)
            {
                case 0:
                    _diffiButton[forNum].onClick.AddListener(OnDrink);
                    break;
                case 1:
                    _diffiButton[forNum].onClick.AddListener(OnHorsDoeuvre);
                    break;
                case 2:
                    _diffiButton[forNum].onClick.AddListener(OnSOUP);
                    break;
                case 3:
                    _diffiButton[forNum].onClick.AddListener(OnMainDish);
                    break;
                case 4:
                    _diffiButton[forNum].onClick.AddListener(OnDessert);
                    break;
            }

            forNum++;
        }
        // �K�x�[�W�R���N�V�����s��
        _diffiObject = null;

        MusicManager.instance.SetChangeDifficulty();
    }


    // �{�^���������ꂽ�Ƃ�
    private void OnDrink()
    {
        _diffiNumber = 0;
        OnButtons();
    }
    private void OnHorsDoeuvre()
    {
        _diffiNumber = 1;
        OnButtons();
    }
    private void OnSOUP()
    {
        _diffiNumber = 2;
        OnButtons();
    }
    private void OnMainDish()
    {
        _diffiNumber = 3;
        OnButtons();
    }
    private void OnDessert()
    {
        _diffiNumber = 4;
        OnButtons();
    }
    // �S��Փx�{�^�����ʃA�N�V����
    private void OnButtons()
    {
        if (_diffiNumber == MusicManager.instance.GetDifficultyNumber()) 
            return;

        MusicManager.instance.SetDifficultyNumber(_diffiNumber);

        ScoreUtility.ChengeDifficulty();

        // �I������Ă�����͔̂w�i�J���[�̕\���ƃe�L�X�g�J���[�̕ύX��������
        for (int i = 0; i < _DIFFICULTY_MAX; i++)
        {
            if (i == _diffiNumber)
            {
                _diffiColor[i].SetActive(true);
                _diffiText[i].color = _whiteColor;
            }
            else
            {
                _diffiColor[i].SetActive(false);
                _diffiText[i].color = _grayColor;
            }
        }

        MusicManager.instance.SetChangeDifficulty();
    }
}
