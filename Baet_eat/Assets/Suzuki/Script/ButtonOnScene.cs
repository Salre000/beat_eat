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

    private void Awake()
    {
        _startButton.onClick.AddListener(OnStart);
    }

    private void OnStart()
    {
        ScoreStatus.nowMusic=MusicManager.instance.GetSelectMusicNumber();

        ScoreStatus.nowDifficulty=(publicEnum.Difficulty)MusicManager.instance.GetDifficultyNumber();

        OnTest();
    }

    private void OnTest()
    {
        GameSceneManager.LoadScene(GameSceneManager.changeScene,LoadSceneMode.Additive);
    }
}
