using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadSceneManager : MonoBehaviour
{

    [SerializeField] Canvas jacketCanvas;


    // Start is called before the first frame update
    private void Awake()
    {
        MusicDataBase musicData = Resources.Load<MusicDataBase>(SaveData.MusicDataName);

        GameObject jacket = jacketCanvas.gameObject.transform.GetChild(2).gameObject;

        jacket.GetComponent<Image>().sprite =
           musicData.musicData[ScoreStatus.nowMusic].jacket;

        //�Ȗ�
        jacket.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text =
            musicData.musicData[ScoreStatus.nowMusic].name;


        //�����
        jacket.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text =
            musicData.musicData[ScoreStatus.nowMusic].musicAuthorName;

        //��Ȏ�
        jacket.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text +=
            musicData.musicData[ScoreStatus.nowMusic].musicComposerName;

        //�ҋȎ�
        jacket.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text +=
            musicData.musicData[ScoreStatus.nowMusic].musicArrangerName;

    }    // Update is called once per frame

    float time =0;
    private void FixedUpdate()
    {
        time += Time.deltaTime;

        if (time < 2.5f || CheckLoad()) return;

        GameSceneManager.LoadScene(GameSceneManager.mainScene);



    }

    private bool CheckLoad() 
    {
        //���[�h�֌W�����Ȃ炱����
        return false;
        if (InGameStatus.GetMusicData() == null) return true;
        if (InGameStatus.GetNowMusic() == null) return true;
    }
}
