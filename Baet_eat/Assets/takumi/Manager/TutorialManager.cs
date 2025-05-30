using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{

    StringObject tutorialText;

    GameObject tutorialCanvas;

    TextMeshProUGUI DemoPlay;
    TextMeshProUGUI ExplanationName;
    TextMeshProUGUI Explanation;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        InGameStatus.AutoMode(true);
    }

    private bool oneFlag = false;
    private void FixedUpdate()
    {
        if(SceneManager.GetActiveScene().name == GameSceneManager.resultScene)Destroy(gameObject);


        if (SoundUtility.soundManager != null ? false : true) return;

        CheckCount();
        StartTime();
        StartTimeCount();

        if (oneFlag) return;
        Debug.Log("tutorialスタート");
        List<NotesBase> notes = NotesUtility.GetNotes();

        maxCount = notes.Count;
        for (int i = 0; i < maxCount; i++)
        {
            notes[i].gameObject.AddComponent<TutorialNotes>();

        }
        tutorialText = Resources.Load<StringObject>("Tutorial/TutorialText");
        tutorialCanvas = GameObject.Instantiate(Resources.Load<GameObject>("Tutorial/TutorialCanvas"), notes[0].transform.parent.transform.parent);
        DemoPlay = tutorialCanvas.transform.GetChild(0).GetComponent<TextMeshProUGUI>();

        ExplanationName = tutorialCanvas.transform.GetChild(1).transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        Explanation = tutorialCanvas.transform.GetChild(1).transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        DemoPlay.gameObject.SetActive(true);
        oneFlag = true;

        ExplanationName.text = tutorialText.strings[index] != null ? tutorialText.strings[index] : "";
        Explanation.text = tutorialText.strings[index] != null ? tutorialText.Explanation[index] : "";

        StopTime();
    }

    private int lostCount = 0;
    private int maxCount = 0;
    private int[] ChengeCount = { 0, 5, 10, 15, 20, 23, 26, 27, 28, 30, 32, 34 };

    private void CheckCount()
    {
        int count = maxCount - LineUtility.GetNotesBases().Count;

        if (count == lostCount) return;

        lostCount = count;

        for (int i = 0; i < ChengeCount.Length; i++)
        {
            if (ChengeCount[i] == count)
            {
                ChengeDemo();
                return;
            }
        }

    }

    [SerializeField] int index = 0;
    //デモモードを切り替える
    void ChengeDemo()
    {
        if (!oneFlga) return;
        index++;
        InGameStatus.AutoMode(lostCount<27?!InGameStatus.GetAuto():true);
        DemoPlay.gameObject.SetActive(lostCount < 27 ? !DemoPlay.gameObject.activeSelf:true);
        if (lostCount==34) 
        {
            TransitionEffect.nextSceneNameSystem = GameSceneManager.resultScene;

            GameSceneManager.LoadScene(GameSceneManager.changeScene, LoadSceneMode.Additive);


        }
        ExplanationName.text = tutorialText.strings[index] != null ? tutorialText.strings[index] : "";
        Explanation.text = tutorialText.strings[index] != null ? tutorialText.Explanation[index] : "";

        StopTime();


    }

    private bool oneFlga = false;
    private void StopTime()
    {
        NotesMove.Instance.stopFlag = true;

        SoundUtility.MainBGMStop();
        time = 0;
    }
    float time = 0;
    private void StartTimeCount()
    {
        if (!oneFlga) return;

        if (!NotesMove.Instance.stopFlag) return;

        time = Time.deltaTime;
        if (Input.GetMouseButton(0))
        {

            NotesMove.Instance.stopFlag = false;

            SoundUtility.MainBGMStart();


        }

        if (time < 3) return;

        NotesMove.Instance.stopFlag = false;

        SoundUtility.MainBGMStart();

    }

    private void StartTime()
    {
        if (!NotesMove.Instance.stopFlag) return;

        if (!Input.GetMouseButton(0)) return;

        if (oneFlga) return;
        oneFlga = true;

        NotesMove.Instance.stopFlag = false;

        SoundUtility.MainBGMStart();

        index++;
        ExplanationName.text = tutorialText.strings[index] != null ? tutorialText.strings[index] : "";
        Explanation.text = tutorialText.strings[index] != null ? tutorialText.Explanation[index] : "";



    }


}
