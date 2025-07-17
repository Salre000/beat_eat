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
        Start,
        Notes,//���̔���̕���������
        NotesDemo,//�����Ɏg�����m�[�c���I�[�g�Ŏ擾
        ScoreGage,//�X�R�A�̃Q�[�W�̐����A�X�R�A���オ��̂�������
        Playnormal,//�ʏ�m�[�c�̃^�b�`����点��
        LongDemo,//�����O�m�[�c���I�[�g�Ŏ�点��
        PlayLong,//�����O�m�[�c�̃^�b�`����点��
        FlickDemo,//�t���b�N�m�[�c���I�[�g�Ŏ�点��
        PlayFlick,//�t���b�N�m�[�c�^�b�`�Ŏ�点��
        SkillDemo,//�X�L���m�[�c���I�[�g�Ŏ�点��
        SkillFlick,//�X�L���m�[�c�^�b�`�Ŏ�点��
        End
    }

    // Start is called before the first frame update
    void Start()
    {
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

        ShowTestCheck();



    }

    private float tutorialPhase = 0;

    private void ShowTestCheck() 
    {
        if (TextShow.showFlag) return;
        TextShow.showText = "���ꂩ��`���[�g���A�����J�n���܂��B��_��"+ tutorialPhase.ToString();
        SceneManager.LoadScene("TextScene", LoadSceneMode.Additive);
        TextShow.AddEndAction(() => tutorialPhase++);
        CreateStart();
    }
    private bool one = false;   
    private void CreateStart() 
    {
        if (one) return;
        one = true;

        GameObject canvas = GameObject.Find("PoseCanvas");

        GameObject tutorialCanvas=new GameObject("tutorialcanvas");

        tutorialCanvas.transform.parent = canvas.transform;

        GameObject Mask = new GameObject("tutorialMask");
        Mask.transform.parent = tutorialCanvas.transform;



        GameObject back = new GameObject("Back");

        back.AddComponent<Image>();

        back.transform.parent = tutorialCanvas.transform;

        back.transform.localPosition = Vector3.zero;

        back.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, Screen.height);
        back.GetComponent<Image>().color = new Color(80, 80, 80, 120);


    }

}
