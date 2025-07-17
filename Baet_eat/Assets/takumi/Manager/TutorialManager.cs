using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
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

    private void ShowTestCheck() 
    {
        if (TextShow.showFlag) return;
        TextShow.showText = "これからチュートリアルを開始します。＞_＜";
        SceneManager.LoadScene("TextScene", LoadSceneMode.Additive);

    }

}
