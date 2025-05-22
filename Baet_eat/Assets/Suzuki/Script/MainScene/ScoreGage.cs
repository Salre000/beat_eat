using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreGage : MonoBehaviour
{
    private float _nowScore = 0f;
    private readonly float _MAX_SCORE = InGameStatus.GetMAXScore();
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private Image _scoreGage;
    [SerializeField] private List<GameObject> _ranks = new(6);
    private StringBuilder _stringBuiluder = new StringBuilder();

    // Start is called before the first frame update
    void Start()
    {
        BuildingString(_nowScore);
        for(int i= _ranks.Count-2; i> 0; i--)
        {
            _ranks[i].SetActive(false);
        }
    }

    // スコア変動時
    private void ChangedScore()
    {
        ScorePercent();
    }

    private void ScorePercent()
    {
        _nowScore = InGameStatus.GetScore();
        float value = _nowScore / _MAX_SCORE;
        _scoreGage.fillAmount = value;
        BuildingString(_nowScore);
    }

    private void BuildingString(float value)
    {
        _stringBuiluder.Clear();
        // スコアに小数点は表示させない
        _stringBuiluder.Append((int)value);
        _scoreText.text = _stringBuiluder.ToString();
    }
}
