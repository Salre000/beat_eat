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
        Notes,//下の判定の部分も説明
        NotesDemo,//説明に使ったノーツをオートで取得
        ScoreGage,//スコアのゲージの説明、スコアが上がるのも見せる
        Playnormal,//通常ノーツのタッチをやらせる
        LongDemo,//ロングノーツをオートで取らせる
        PlayLong,//ロングノーツのタッチをやらせる
        FlickDemo,//フリックノーツをオートで取らせる
        PlayFlick,//フリックノーツタッチで取らせる
        SkillDemo,//スキルノーツをオートで取らせる
        SkillFlick,//スキルノーツタッチで取らせる
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

        //チュートリアルの開始
        if (!startFlag) return;

        ShowTestCheck();



    }

    private float tutorialPhase = 0;

    private void ShowTestCheck() 
    {
        if (TextShow.showFlag) return;
        TextShow.showText = "これからチュートリアルを開始します。＞_＜"+ tutorialPhase.ToString();
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
