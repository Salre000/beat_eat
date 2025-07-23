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
        //���߂ăQ�[�����N������
        AchievementStatus.Achievement(AchievementTypeEnum.AchievementType._StartGame);

    }

    //���j���[�̏󋵂𔽓]
    public void ChengeMenu() 
    {
        _canvas.gameObject.SetActive(!_canvas.gameObject.activeSelf);
    }

    public void ChengeTitel() 
    {

        TransitionEffect.nextSceneNameSystem = GameSceneManager.titleScene;

        GameSceneManager.LoadScene(GameSceneManager.changeScene, LoadSceneMode.Additive);

    }
    //�ꎞ�I�Ƀ^�C�g���ֈړ��ɂȂ��Ă���
    public void ChengeCredit()
    {

        TransitionEffect.nextSceneNameSystem = GameSceneManager.titleScene;

        GameSceneManager.LoadScene(GameSceneManager.changeScene, LoadSceneMode.Additive);

    }

    public void TutorialStart() 
    {

        ScoreStatus.nowDifficulty = publicEnum.Difficulty.dessert;
        ScoreStatus.nowMusic = 0;

        AchievementStatus.Achievement(AchievementTypeEnum.AchievementType._Tutorial);
        OptionStatus.SetNotesSpeed(1);

        new GameObject("TutorialObject").AddComponent<TutorialManager>();

        TransitionEffect.nextSceneNameSystem = GameSceneManager.mainScene;

        GameSceneManager.LoadScene(GameSceneManager.changeScene, LoadSceneMode.Additive);


    }
}
