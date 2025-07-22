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
        Notes,//基本のノーツを説明
        NotesLine,//下の判定の部分も説明
        NotesDemo,//説明に使ったノーツをオートで取得
        ScoreGage,//スコアのゲージの説明、スコアが上がるのも見せる
        Playnormal,//通常ノーツのタッチをやらせる
        LongDemo,//ロングノーツをオートで取らせる
        PlayLong,//ロングノーツのタッチをやらせる
        FlickDemo,//フリックノーツをオートで取らせる
        PlayFlick,//フリックノーツタッチで取らせる
        SkillDemo,//スキルノーツをオートで取らせる
        PlaySkill,//スキルノーツタッチで取らせる
        SideNotes,//サイドレーンの説明もこみ
        SideNotesFlick,//フリックすると
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

        //チュートリアルの開始
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
                TutorialPhaseSideNotes();
                break;
            case TutorialPhase.SideNotes:
                TutorialPhaseSideFlick();
                break;
            case TutorialPhase.SideNotesFlick:
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
        TextShow.showText = "これからチュートリアルを開始します。＞_＜";
        SceneManager.LoadScene("TextScene", LoadSceneMode.Additive);
        TextShow.AddEndAction(() => { NextPhase = TutorialPhase.Start; oneFlag = true; });
        //チュートリアルに使うオブジェクトなどを生成
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

        //ノーツと音楽を止める
        SoundUtility.MainBGMStop();
        NotesMove.Instance.stopFlag = true;
        StartMask(new Vector2(50, 130), new Vector2(110, 30));

        TextShow.showText = "ノーツの説明。＞_＜";
        SceneManager.LoadScene("TextScene", LoadSceneMode.Additive);
        TextShow.AddEndAction(() =>
        {
            TextShow.showText = "基本のタップノーツ。＞_＜";
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
        TextShow.showText = "判定のラインの説明。＞_＜";
        SceneManager.LoadScene("TextScene", LoadSceneMode.Additive);
        StartMask(new Vector2(0, -180), new Vector2(1300, 80));
        TextShow.AddEndAction(() =>
        {

            TextShow.showText = "ノーツが判定ラインに重なった時に";
            SceneManager.LoadScene("TextScene", LoadSceneMode.Additive);
            TextShow.AddEndAction(() =>
            {
                TextShow.showText = "ノーツの位置の判定ラインをタップ";
                SceneManager.LoadScene("TextScene", LoadSceneMode.Additive);
                TextShow.AddEndAction(() =>
                {

                    //ノーツと音楽を動き始める
                    NextPhase = TutorialPhase.NotesLine; oneFlag = true;
                });
            });
        });
    }
    private void TutorialPhaseNotesDemo()
    {
        if (!oneFlag) return;
        oneFlag = false;

        TextShow.OFFSet = 200;
        TextShow.Speed = 3;
        TextShow.showText = "デモプレイに切り替え。＞_＜";
        SceneManager.LoadScene("TextScene", LoadSceneMode.Additive);
        TextShow.AddEndAction(() =>
        {

            TextShow.OFFSet = 200;
            TextShow.Speed = 3;
            TextShow.showText = "デモプレイ中。＞_＜";
            SceneManager.LoadScene("TextScene", LoadSceneMode.Additive);
            //ノーツと音楽を動き始める
            SoundUtility.MainBGMStart();
            NotesMove.Instance.stopFlag = false;
            EndMask();


            TextShow.AddEndAction(() =>
            {

                NextPhase = TutorialPhase.NotesDemo;
                oneFlag = true;
                //ノーツと音楽を止める
                SoundUtility.MainBGMStop();
                NotesMove.Instance.stopFlag = true;
            });


        });
    }
    private void TutorialPhaseScoreGage()
    {
        if (!oneFlag) return;

        //ノーツと音楽を止める
        SoundUtility.MainBGMStop();
        NotesMove.Instance.stopFlag = true;
        StartMask(new Vector2(-525, 310), new Vector2(500, 110));

        oneFlag = false;
        TextShow.showText = "スコアゲージの説明。＞_＜";
        SceneManager.LoadScene("TextScene", LoadSceneMode.Additive);
        TextShow.AddEndAction(() =>
        {
            TextShow.showText = "ノーツを取るとスコアとゲージが上がって行く。＞_＜";
            SceneManager.LoadScene("TextScene", LoadSceneMode.Additive);

            TextShow.AddEndAction(() =>
            {
                TextShow.showText = "上がったスコアに応じてランクが決まる。＞_＜";
                SceneManager.LoadScene("TextScene", LoadSceneMode.Additive);

                StartMask(new Vector2(-725, 290), new Vector2(100, 100));

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
        TextShow.showText = "実際にノーツをプレイ。＞_＜";
        SceneManager.LoadScene("TextScene", LoadSceneMode.Additive);
        TextShow.AddEndAction(() =>
        {
            EndMask();
            //ノーツと音楽を動き始める
            SoundUtility.MainBGMStart();
            NotesMove.Instance.stopFlag = false;
            TextShow.OFFSet = 200;
            TextShow.Speed = 3;


            TextShow.showText = "プレイ中。＞_＜";
            SceneManager.LoadScene("TextScene", LoadSceneMode.Additive);

            TextShow.AddEndAction(() =>
            {
                NotesMove.Instance.stopFlag = false;
                TextShow.OFFSet = 200;
                TextShow.Speed = 2;

                TextShow.showText = "プレイ中。＞_＜";
                SceneManager.LoadScene("TextScene", LoadSceneMode.Additive);

                TextShow.AddEndAction(() =>
                {
                    TextShow.OFFSet = 200;
                    TextShow.Speed = 3;

                    TextShow.showText = "簡単でしょ。＞_＜";
                    SceneManager.LoadScene("TextScene", LoadSceneMode.Additive);

                    TextShow.AddEndAction(() =>
                    {


                        NextPhase = TutorialPhase.Playnormal;
                        oneFlag = true;
                    });
                });
            });
        });
    }
    private void TutorialPhaseLongDemo()
    {
        if (!oneFlag) return;
        //ノーツと音楽を止める
        SoundUtility.MainBGMStop();
        NotesMove.Instance.stopFlag = true;
        StartMask(new Vector2(25, 240), new Vector2(100, 120));

        oneFlag = false;
        TextShow.showText = "このノーツはロングノーツ。＞_＜";
        SceneManager.LoadScene("TextScene", LoadSceneMode.Additive);
        TextShow.AddEndAction(() =>
        {
            TextShow.showText = "ラインに沿って終点までタッチ。＞_＜";
            SceneManager.LoadScene("TextScene", LoadSceneMode.Additive);

            TextShow.AddEndAction(() =>
            {
                TextShow.OFFSet = 200;
                TextShow.Speed = 2;

                TextShow.showText = "デモプレイに切り替え。＞_＜";
                SceneManager.LoadScene("TextScene", LoadSceneMode.Additive);

                TextShow.AddEndAction(() =>
                {
                    SoundUtility.MainBGMStart();
                    NotesMove.Instance.stopFlag = false;
                    EndMask();
                    TextShow.OFFSet = 200;
                    TextShow.Speed = 2;

                    TextShow.showText = "デモプレイ中。＞_＜";
                    SceneManager.LoadScene("TextScene", LoadSceneMode.Additive);

                    TextShow.AddEndAction(() =>
                    {
                        NextPhase = TutorialPhase.LongDemo; oneFlag = true;

                    });

                });

            });

        });
    }
    private void TutorialPhaseLongPlay()
    {
        if (!oneFlag) return;
        SoundUtility.MainBGMStop();
        NotesMove.Instance.stopFlag = true;

        oneFlag = false;
        TextShow.showText = "実際にノーツをプレイ。＞_＜";
        SceneManager.LoadScene("TextScene", LoadSceneMode.Additive);
        TextShow.AddEndAction(() =>
        {

            SoundUtility.MainBGMStart();
            NotesMove.Instance.stopFlag = false;
            TextShow.OFFSet = 200;
            TextShow.Speed = 2;
            TextShow.showText = "プレイ中。＞_＜";
            SceneManager.LoadScene("TextScene", LoadSceneMode.Additive);
            TextShow.AddEndAction(() =>
            {
                TextShow.OFFSet = 200;
                TextShow.Speed = 2;

                TextShow.showText = "プレイ中。＞_＜";
                SceneManager.LoadScene("TextScene", LoadSceneMode.Additive);
                TextShow.AddEndAction(() =>
                {
                    NextPhase = TutorialPhase.PlayLong; oneFlag = true;


                });


            });
        });
    }
    private void TutorialPhaseFlickDemo()
    {
        if (!oneFlag) return;
        oneFlag = false;

        SoundUtility.MainBGMStop();
        NotesMove.Instance.stopFlag = true;

        StartMask(new Vector2(-10, 230), new Vector2(90, 60));

        TextShow.showText = "このノーツはフリックノーツ。＞_＜";
        SceneManager.LoadScene("TextScene", LoadSceneMode.Additive);
        TextShow.AddEndAction(() =>
        {
            TextShow.showText = "タイミングよくフリック。＞_＜";
            SceneManager.LoadScene("TextScene", LoadSceneMode.Additive);

            TextShow.AddEndAction(() =>
            {
                TextShow.OFFSet = 200;
                TextShow.Speed = 2;

                TextShow.showText = "デモプレイに切り替え。＞_＜";
                SceneManager.LoadScene("TextScene", LoadSceneMode.Additive);

                TextShow.AddEndAction(() =>
                {
                    SoundUtility.MainBGMStart();
                    NotesMove.Instance.stopFlag = false;
                    EndMask();
                    TextShow.OFFSet = 200;
                    TextShow.Speed = 2;

                    TextShow.showText = "デモプレイ中。＞_＜";
                    SceneManager.LoadScene("TextScene", LoadSceneMode.Additive);

                    TextShow.AddEndAction(() =>
                    {
                        SoundUtility.MainBGMStop();
                        NotesMove.Instance.stopFlag = true;

                        NextPhase = TutorialPhase.FlickDemo; oneFlag = true;

                    });

                });

            });

        });
    }
    private void TutorialPhaseFlickPlay()
    {
        if (!oneFlag) return;
        oneFlag = false;
        TextShow.showText = "実際にノーツをプレイ。＞_＜";
        SceneManager.LoadScene("TextScene", LoadSceneMode.Additive);
        TextShow.AddEndAction(() =>
        {

            SoundUtility.MainBGMStart();
            NotesMove.Instance.stopFlag = false;
            TextShow.OFFSet = 200;
            TextShow.Speed = 2;
            TextShow.showText = "プレイ中。＞_＜";
            SceneManager.LoadScene("TextScene", LoadSceneMode.Additive);
            TextShow.AddEndAction(() =>
            {
                NextPhase = TutorialPhase.PlayFlick; oneFlag = true;
            });
        });
    }
    private void TutorialPhaseSkillDemo()
    {
        if (!oneFlag) return;
        oneFlag = false;
        SoundUtility.MainBGMStop();
        NotesMove.Instance.stopFlag = true;
        StartMask(new Vector2(-5, 155), new Vector2(120, 30));


        TextShow.showText = "このノーツはスキルノーツ。＞_＜";
        SceneManager.LoadScene("TextScene", LoadSceneMode.Additive);
        TextShow.AddEndAction(() =>
        {
            TextShow.showText = "いつでもデリシャスクリティカル。＞_＜";
            SceneManager.LoadScene("TextScene", LoadSceneMode.Additive);

            TextShow.AddEndAction(() =>
            {
                TextShow.OFFSet = 200;
                TextShow.Speed = 2;

                TextShow.showText = "デモプレイに切り替え。＞_＜";
                SceneManager.LoadScene("TextScene", LoadSceneMode.Additive);

                TextShow.AddEndAction(() =>
                {
                    SoundUtility.MainBGMStart();
                    NotesMove.Instance.stopFlag = false;
                    EndMask();
                    TextShow.OFFSet = 200;
                    TextShow.Speed = 2;

                    TextShow.showText = "デモプレイ中。＞_＜";
                    SceneManager.LoadScene("TextScene", LoadSceneMode.Additive);

                    TextShow.AddEndAction(() =>
                    {
                        SoundUtility.MainBGMStop();
                        NotesMove.Instance.stopFlag = true;

                        NextPhase = TutorialPhase.SkillDemo; oneFlag = true;

                    });

                });

            });

        });
    }
    private void TutorialPhaseSkillPlay()
    {
        if (!oneFlag) return;
        oneFlag = false;
        TextShow.showText = "実際にノーツをプレイ。＞_＜";
        SceneManager.LoadScene("TextScene", LoadSceneMode.Additive);
        TextShow.AddEndAction(() =>
        {

            SoundUtility.MainBGMStart();
            NotesMove.Instance.stopFlag = false;
            TextShow.OFFSet = 200;
            TextShow.Speed = 2;
            TextShow.showText = "プレイ中。＞_＜";
            SceneManager.LoadScene("TextScene", LoadSceneMode.Additive);
            TextShow.AddEndAction(() =>
            {
                NextPhase = TutorialPhase.PlaySkill; oneFlag = true;
                SoundUtility.MainBGMStop();
                NotesMove.Instance.stopFlag = true;



            });
        });
    }
    private void TutorialPhaseSideNotes()
    {
        if (!oneFlag) return;
        oneFlag = false;
        TextShow.OFFSet = 200;
        TextShow.Speed = 2;
        TextShow.showText = "ここからの説明は一部の難易度しかでない！！。";
        SceneManager.LoadScene("TextScene", LoadSceneMode.Additive);
        TextShow.AddEndAction(() =>
        {

            TextShow.OFFSet = 200;
            TextShow.Speed = 2;
            TextShow.showText = "サイドレーンの説明";
            SceneManager.LoadScene("TextScene", LoadSceneMode.Additive);
            TextShow.AddEndAction(() =>
            {
                TextShow.OFFSet = 200;
                TextShow.Speed = 2;
                TextShow.showText = "タップする場所が違うだけ";
                SceneManager.LoadScene("TextScene", LoadSceneMode.Additive);
                TextShow.AddEndAction(() =>
                {
                    StartMask(new Vector2(620, 150), new Vector2(200, 250));
                    TextShow.OFFSet = -200;
                    TextShow.Speed = 2;
                    TextShow.showText = "タップする場所はここと";
                    SceneManager.LoadScene("TextScene", LoadSceneMode.Additive);
                    TextShow.AddEndAction(() =>
                    {
                        StartMask(new Vector2(-620, 150), new Vector2(200, 250));
                        TextShow.OFFSet = -200;
                        TextShow.Speed = 2;
                        TextShow.showText = "ここの二箇所";
                        SceneManager.LoadScene("TextScene", LoadSceneMode.Additive);
                        TextShow.AddEndAction(() =>
                        {

                            TextShow.Speed = 3;
                            SoundUtility.MainBGMStart();
                            NotesMove.Instance.stopFlag = false;



                            TextShow.showText = "次はサイドレーンにしかないノーツ";
                            SceneManager.LoadScene("TextScene", LoadSceneMode.Additive);
                            TextShow.AddEndAction(() =>
                            {
                                StartMask(new Vector2(-250, 250), new Vector2(110, 150));
                                SoundUtility.MainBGMStop();
                                NotesMove.Instance.stopFlag = true;

                                NextPhase = TutorialPhase.SideNotes; oneFlag = true;



                            });

                        });

                    });
                });
            });
        });
    }
    private void TutorialPhaseSideFlick()
    {
        if (!oneFlag) return;
        oneFlag = false;
        TextShow.showText = "このノーツはサイドフリックノーツ";
        SceneManager.LoadScene("TextScene", LoadSceneMode.Additive);
        TextShow.AddEndAction(() =>
        {
            TextShow.showText = "矢印の方向にフリックが必要";
            SceneManager.LoadScene("TextScene", LoadSceneMode.Additive);
            TextShow.AddEndAction(() =>
            {

                EndMask();
                TextShow.OFFSet = 200;
                TextShow.Speed = 2;
                TextShow.showText = "デモプレイを開始";
                SceneManager.LoadScene("TextScene", LoadSceneMode.Additive);
                TextShow.AddEndAction(() =>
                {
                    SoundUtility.MainBGMStart();
                    NotesMove.Instance.stopFlag = false;

                    TextShow.OFFSet = 200;
                    TextShow.Speed = 2;
                    TextShow.showText = "デモプレイ中";
                    SceneManager.LoadScene("TextScene", LoadSceneMode.Additive);
                    TextShow.AddEndAction(() =>
                    {
                        SoundUtility.MainBGMStop();
                        NotesMove.Instance.stopFlag = true;
                        TextShow.showText = "ビックリした？";
                        SceneManager.LoadScene("TextScene", LoadSceneMode.Additive);
                        TextShow.AddEndAction(() =>
                        {
                            SoundUtility.MainBGMStop();
                            NotesMove.Instance.stopFlag = true;

                            TextShow.showText = "このノーツはサイドレーンをひっくり返すんだ";
                            SceneManager.LoadScene("TextScene", LoadSceneMode.Additive);
                            TextShow.AddEndAction(() =>
                            {
                                TextShow.showText = "それじゃあプレイしてみよう";
                                SceneManager.LoadScene("TextScene", LoadSceneMode.Additive);
                                TextShow.AddEndAction(() =>
                                {
                                    SoundUtility.MainBGMStart();
                                    NotesMove.Instance.stopFlag = false;

                                    TextShow.showText = "プレイ中";
                                    SceneManager.LoadScene("TextScene", LoadSceneMode.Additive);
                                    TextShow.AddEndAction(() =>
                                    {
                                        NextPhase = TutorialPhase.SideNotesFlick; oneFlag = true;
                                    });
                                });

                            });

                        });

                    });


                });


            });
        });
    }
    private void TutorialPhaseEnd()
    {
        if (!oneFlag) return;
        oneFlag = false;
        TextShow.showText = "これでチュートリアルを終了するよ。＞_＜";
        SceneManager.LoadScene("TextScene", LoadSceneMode.Additive);
        TextShow.AddEndAction(() =>
        {
            TransitionEffect.nextSceneNameSystem = GameSceneManager.selectScene;

            GameSceneManager.LoadScene(GameSceneManager.changeScene, LoadSceneMode.Additive);


        });
    }

}
