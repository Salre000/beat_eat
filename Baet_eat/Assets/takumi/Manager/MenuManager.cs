using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField]Canvas _canvas;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    //ƒƒjƒ…[‚Ìó‹µ‚ğ”½“]
    public void ChengeMenu() 
    {
        _canvas.gameObject.SetActive(!_canvas.gameObject.activeSelf);
    }

    public void TutorialStart() 
    {

        ScoreStatus.nowDifficulty = 0;
        ScoreStatus.nowMusic = 0;

        AchievementStatus.Achievement(AchievementTypeEnum.AchievementType._Tutorial);

        new GameObject().AddComponent<TutorialManager>();

        TransitionEffect.nextSceneNameSystem = GameSceneManager.mainScene;

        GameSceneManager.LoadScene(GameSceneManager.changeScene, LoadSceneMode.Additive);


    }
}
