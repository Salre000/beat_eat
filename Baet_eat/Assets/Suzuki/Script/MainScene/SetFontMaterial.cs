using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SetFontMaterial : MonoBehaviour
{
    // 現在のスコア
    [SerializeField] TextMeshProUGUI _scoreText;
    // 読んで字の如く「Life」
    [SerializeField] List<TextMeshProUGUI> _lifeText;
    // 現在のスコアランク
    [SerializeField] List<TextMeshProUGUI> _rankText;
    // ゲージラインのランク
    [SerializeField] List<TextMeshProUGUI> _lineRank;
    // 白アウトライン
    [SerializeField]Material _scoreMaterial;
    // 灰アウトライン
    [SerializeField]Material _lifeMaterial;
    // 黒アウトライン
    [SerializeField]Material _rankMaterial;

    // Start is called before the first frame update
    void Awake()
    {
        for(int i=0;i< _lifeText.Count;i++)
        {
            _lifeText[i].fontMaterial = _lifeMaterial;
        }
        for(int i=0;i< _rankText.Count;i++)
        {
            _rankText[i].fontMaterial = _rankMaterial;
        }
        for(int i=0;i< _lineRank.Count;i++)
        {
            _lineRank[i].fontMaterial = _lifeMaterial;
        }
            _scoreText.fontMaterial = _scoreMaterial;
    }
}
