using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonOnScene : MonoBehaviour
{
    [SerializeField, Header("Startボタン")] private Button _startButton;
    [SerializeField, Header("設定ボタン")] private Button _settingButton;
    [SerializeField, Header("ノーツ設定ボタン")] private Button _notesButton;

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
