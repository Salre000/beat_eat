using Coffee.UIExtensions;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    private enum TutorialPhase
    {
        None,
        Start,
        Notes,//��{�̃m�[�c�����
        NotesLine,//���̔���̕���������
        NotesDemo,//�����Ɏg�����m�[�c���I�[�g�Ŏ擾
        ScoreGage,//�X�R�A�̃Q�[�W�̐����A�X�R�A���オ��̂�������
        Playnormal,//�ʏ�m�[�c�̃^�b�`����点��
        LongDemo,//�����O�m�[�c���I�[�g�Ŏ�点��
        PlayLong,//�����O�m�[�c�̃^�b�`����点��
        FlickDemo,//�t���b�N�m�[�c���I�[�g�Ŏ�点��
        PlayFlick,//�t���b�N�m�[�c�^�b�`�Ŏ�点��
        SkillDemo,//�X�L���m�[�c���I�[�g�Ŏ�点��
        PlaySkill,//�X�L���m�[�c�^�b�`�Ŏ�点��
        End
    }
    private TutorialPhase phase;
    private TutorialPhase NextPhase;

    private GameObject TutorialBack;
    private RectTransform TutorialUnMask;

    // Start is called before the first frame update
    void Start()
    {
        phase = TutorialPhase.None;
        NextPhase = TutorialPhase.None;
        DontDestroyOnLoad(gameObject);
        InGameStatus.AutoMode(true);
    }

    private bool startFlag = false;
    private void FixedUpdate()
    {
        if (SceneManager.GetActiveScene().name == GameSceneManager.resultScene) Destroy(gameObject);
        if (SceneManager.GetActiveScene().name == GameSceneManager.mainScene) startFlag = true;

        //�`���[�g���A���̊J�n
        if (!startFlag) return;
        CheckPhase();
        SwitchPhase();


    }
    private void SwitchPhase()
    {
        switch (phase)
        {
            case TutorialPhase.None:
                TutorialPhaseStart();
                break;
            case TutorialPhase.Start:
                TutorialPhaseNotes();
                break;
            case TutorialPhase.Notes:
                TutorialPhaseNotesLine();
                break;
            case TutorialPhase.NotesLine:
                TutorialPhaseNotesDemo();
                break;
            case TutorialPhase.NotesDemo:
                TutorialPhaseScoreGage();
                break;
            case TutorialPhase.ScoreGage:
                TutorialPhaseNotesPlay();
                break;
            case TutorialPhase.Playnormal:
                TutorialPhaseLongDemo();
                break;
            case TutorialPhase.LongDemo:
                TutorialPhaseLongPlay();
                break;
            case TutorialPhase.PlayLong:
                TutorialPhaseFlickDemo();
                break;
            case TutorialPhase.FlickDemo:
                TutorialPhaseFlickPlay();
                break;
            case TutorialPhase.PlayFlick:
                TutorialPhaseSkillDemo();
                break;
            case TutorialPhase.SkillDemo:
                TutorialPhaseSkillPlay();
                break;
            case TutorialPhase.PlaySkill:
                TutorialPhaseEnd();
                break;
            case TutorialPhase.End:
                break;
        }
    }

    private void CheckPhase()
    {
        if (NextPhase == phase) return;
        phase = NextPhase;
    }
    private bool one = false;
    private void CreateStart()
    {
        if (one) return;
        one = true;

        GameObject canvas = GameObject.Find("PoseCanvas");

        GameObject tutorialCanvas = new GameObject("tutorialcanvas");

        tutorialCanvas.transform.parent = canvas.transform;

        tutorialCanvas.AddComponent<RectTransform>().localPosition = Vector3.zero; ;
        tutorialCanvas.AddComponent<Mask>().showMaskGraphic = false; ;
        tutorialCanvas.AddComponent<Image>();
        tutorialCanvas.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, Screen.height);

        GameObject Mask = new GameObject("tutorialMask");
        Mask.transform.parent = tutorialCanvas.transform;
        Mask.AddComponent<Image>();
        Mask.AddComponent<Unmask>();
        Mask.AddComponent<RectTransform>();
        TutorialUnMask = Mask.GetComponent<RectTransform>();
        TutorialUnMask.sizeDelta = new Vector2(Screen.width / 2, Screen.height / 2);


        GameObject back = new GameObject("Back");

        back.AddComponent<Image>();

        back.transform.parent = tutorialCanvas.transform;

        back.transform.localPosition = Vector3.zero;

        back.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, Screen.height);

        Color color = new Color(0.1f, 0.1f, 0.1f);
        color.a = 0.7f;
        back.GetComponent<Image>().color = color;

        TutorialBack = back;

        TutorialBack.SetActive(false);

    }

    private bool oneFlag = true;
    private void TutorialPhaseStart()
    {
        if (!oneFlag) return;
        oneFlag = false;
        TextShow.showText = "���ꂩ��`���[�g���A�����J�n���܂��B��_��";
        SceneManager.LoadScene("TextScene", LoadSceneMode.Additive);
        TextShow.AddEndAction(() => { NextPhase = TutorialPhase.Start; oneFlag = true; });
        //�`���[�g���A���Ɏg���I�u�W�F�N�g�Ȃǂ𐶐�
        CreateStart();
    }

    private void StartMask(Vector2 pos, Vector2 size)
    {
        TutorialUnMask.sizeDelta = size;
        TutorialUnMask.localPosition = pos;
        TutorialBack?.SetActive(true);


    }
    private void EndMask()
    {

        TutorialBack?.SetActive(false);
    }
    private void TutorialPhaseNotes()
    {
        if (!oneFlag) return;
        oneFlag = false;

        //�m�[�c�Ɖ��y���~�߂�
        SoundUtility.MainBGMStop();
        NotesMove.Instance.stopFlag = true;
        StartMask(new Vector2(50, 130), new Vector2(110, 30));

        TextShow.showText = "�m�[�c�̐����B��_��";
        SceneManager.LoadScene("TextScene", LoadSceneMode.Additive);
        TextShow.AddEndAction(() =>
        {
            TextShow.showText = "��{�̃^�b�v�m�[�c�B��_��";
            SceneManager.LoadScene("TextScene", LoadSceneMode.Additive);




            TextShow.AddEndAction(() =>
            {

                NextPhase = TutorialPhase.Notes;
                oneFlag = true;

            });

        });
    }
    private void TutorialPhaseNotesLine()
    {
        if (!oneFlag) return;
        oneFlag = false;
        TextShow.showText = "����̃��C���̐����B��_��";
        SceneManager.LoadScene("TextScene", LoadSceneMode.Additive);
        StartMask(new Vector2(0, -180), new Vector2(1300, 80));
        TextShow.AddEndAction(() =>
        {

            TextShow.showText = "�m�[�c�����胉�C���ɏd�Ȃ�������";
            SceneManager.LoadScene("TextScene", LoadSceneMode.Additive);
            TextShow.AddEndAction(() =>
            {
                TextShow.showText = "�m�[�c�̈ʒu�̔��胉�C�����^�b�v";
                SceneManager.LoadScene("TextScene", LoadSceneMode.Additive);
                TextShow.AddEndAction(() =>
                {

                    //�m�[�c�Ɖ��y�𓮂��n�߂�
                    SoundUtility.MainBGMStart();
                    NotesMove.Instance.stopFlag = false;
                    NextPhase = TutorialPhase.NotesLine; oneFlag = true;
                    EndMask();
                });
            });
        });
    }
    private void TutorialPhaseNotesDemo()
    {
        if (!oneFlag) return;
        oneFlag = false;
        //�m�[�c�Ɖ��y�𓮂��n�߂�
        SoundUtility.MainBGMStart();
        NotesMove.Instance.stopFlag = false;

        TextShow.OFFSet = 200;
        TextShow.Speed = 3;
        TextShow.showText = "�f���v���C�B��_��";
        SceneManager.LoadScene("TextScene", LoadSceneMode.Additive);
        TextShow.AddEndAction(() =>
        {

            NextPhase = TutorialPhase.NotesDemo; 
            oneFlag = true;
            //�m�[�c�Ɖ��y���~�߂�
            SoundUtility.MainBGMStop();
            NotesMove.Instance.stopFlag = true;


        });
    }
    private void TutorialPhaseScoreGage()
    {
        if (!oneFlag) return;

        //�m�[�c�Ɖ��y���~�߂�
        SoundUtility.MainBGMStop();
        NotesMove.Instance.stopFlag = true;
        StartMask(new Vector2(-525, 310), new Vector2(500, 110));

        oneFlag = false;
        TextShow.showText = "�X�R�A�Q�[�W�̐����B��_��";
        SceneManager.LoadScene("TextScene", LoadSceneMode.Additive);
        TextShow.AddEndAction(() => 
        {
            TextShow.showText = "�m�[�c�����ƃX�R�A�ƃQ�[�W���オ���čs���B��_��";
            SceneManager.LoadScene("TextScene", LoadSceneMode.Additive);

            TextShow.AddEndAction(() =>
            {
                TextShow.showText = "�オ�����X�R�A�ɉ����ă����N�����܂�B��_��";
                SceneManager.LoadScene("TextScene", LoadSceneMode.Additive);

                TextShow.AddEndAction(() =>
                {
                    NextPhase = TutorialPhase.ScoreGage;
                    oneFlag = true;
                });
            });
        });
    }
    private void TutorialPhaseNotesPlay()
    {
        if (!oneFlag) return;
        oneFlag = false;
        TextShow.showText = "���ۂɃm�[�c���v���C�B��_��";
        SceneManager.LoadScene("TextScene", LoadSceneMode.Additive);
        TextShow.AddEndAction(() => { NextPhase = TutorialPhase.Playnormal; oneFlag = true; });
    }
    private void TutorialPhaseLongDemo()
    {
        if (!oneFlag) return;
        oneFlag = false;
        TextShow.showText = "Long�m�[�c�̃f���v���C�B��_��";
        SceneManager.LoadScene("TextScene", LoadSceneMode.Additive);
        TextShow.AddEndAction(() => { NextPhase = TutorialPhase.LongDemo; oneFlag = true; });
    }
    private void TutorialPhaseLongPlay()
    {
        if (!oneFlag) return;
        oneFlag = false;
        TextShow.showText = "Long�m�[�c�̃v���C�B��_��";
        SceneManager.LoadScene("TextScene", LoadSceneMode.Additive);
        TextShow.AddEndAction(() => { NextPhase = TutorialPhase.PlayLong; oneFlag = true; });
    }
    private void TutorialPhaseFlickDemo()
    {
        if (!oneFlag) return;
        oneFlag = false;
        TextShow.showText = "Flick�m�[�c�̃f���v���C�B��_��";
        SceneManager.LoadScene("TextScene", LoadSceneMode.Additive);
        TextShow.AddEndAction(() => { NextPhase = TutorialPhase.FlickDemo; oneFlag = true; });
    }
    private void TutorialPhaseFlickPlay()
    {
        if (!oneFlag) return;
        oneFlag = false;
        TextShow.showText = "Flick�m�[�c�̃v���C�B��_��";
        SceneManager.LoadScene("TextScene", LoadSceneMode.Additive);
        TextShow.AddEndAction(() => { NextPhase = TutorialPhase.PlayFlick; oneFlag = true; });
    }
    private void TutorialPhaseSkillDemo()
    {
        if (!oneFlag) return;
        oneFlag = false;
        TextShow.showText = "Skill�m�[�c�̃f���v���C�B��_��";
        SceneManager.LoadScene("TextScene", LoadSceneMode.Additive);
        TextShow.AddEndAction(() => { NextPhase = TutorialPhase.SkillDemo; oneFlag = true; });
    }
    private void TutorialPhaseSkillPlay()
    {
        if (!oneFlag) return;
        oneFlag = false;
        TextShow.showText = "Skill�m�[�c�̃v���C�B��_��";
        SceneManager.LoadScene("TextScene", LoadSceneMode.Additive);
        TextShow.AddEndAction(() => { NextPhase = TutorialPhase.PlaySkill; oneFlag = true; });
    }
    private void TutorialPhaseEnd()
    {
        if (!oneFlag) return;
        oneFlag = false;
        TextShow.showText = "����Ń`���[�g���A�����I�������B��_��";
        SceneManager.LoadScene("TextScene", LoadSceneMode.Additive);
        TextShow.AddEndAction(() =>
        {
            TransitionEffect.nextSceneNameSystem = GameSceneManager.selectScene;

            GameSceneManager.LoadScene(GameSceneManager.changeScene, LoadSceneMode.Additive);


        });
    }

}
