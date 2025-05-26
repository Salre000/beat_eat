using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonOnScene : MonoBehaviour
{
    [SerializeField, Header("Start�{�^��")] private Button _startButton;
    [SerializeField, Header("�ݒ�{�^��")] private Button _settingButton;
    [SerializeField, Header("�m�[�c�ݒ�{�^��")] private Button _notesButton;
    [SerializeField, Header("�e�X�g�p")] private Button _test;

    private void Awake()
    {
        _startButton.onClick.AddListener(OnStart);
        _test.onClick.AddListener(OnTest);
    }

    private void OnStart()
    {
        GameSceneManager.LoadScene(GameSceneManager.loadScene);

        ScoreStatus.nowMusic=MusicManager.instance.GetSelectMusicNumber();

        ScoreStatus.nowDifficulty=(publicEnum.Difficulty)MusicManager.instance.GetDifficultyNumber();


    }

    private void OnTest()
    {
        GameSceneManager.LoadScene(GameSceneManager.changeScene,LoadSceneMode.Additive);
    }
}
