using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitelManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI pushTo;
    // Start is called before the first frame update
    public static bool oneFlag = true;
    public static bool startChangeFlag = false;
    private void Awake()
    {
        GameSceneManager.isTargetTitle = false;
        oneFlag=true;
        startChangeFlag = false;

        if (!System.IO.File.Exists(Application.persistentDataPath + "/" + SaveData.FoundationFileName + SaveData.FILR_EXTENSION))
        {
            SaveData.SaveFoundation(1);
        }

        if (!System.IO.File.Exists(Application.persistentDataPath + "/" + SaveData.OpstionFileName + SaveData.FILR_EXTENSION))
        {
            SaveData.SaveOption(1);
        }
        OptionStatus.Initialize();


    }



    public void FixedUpdate()
    {
        if ((Input.GetMouseButton(0) || Input.touchCount > 0) && !oneFlag)
        {
            startChangeFlag=true;
            oneFlag = true;
            ChengeSelect();

        }
        AlphaChenge();
    }

    float alpha = 0.01f;
    private void AlphaChenge()
    {
        Color color = pushTo.color;
        color.a += alpha;
        if (color.a >= 1 || color.a <= 0) alpha *= -1;
        pushTo.color = color;

    }
    public void ChengeSelect()
    {
        TransitionEffect.nextSceneNameSystem = GameSceneManager.selectScene;

        GameSceneManager.LoadScene(GameSceneManager.changeScene, LoadSceneMode.Additive);


    }

}
