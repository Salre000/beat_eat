using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SelectSceneFontSet : MonoBehaviour
{
    // SelectSceneのフォントマテリアルをセットする
    [SerializeField,Header("アウトライン用マテリアル")] private Material rankAndScoreFontMaterial;
    [SerializeField, Header("共通マテリアル")] private Material _nomalFontMaterial;
    [SerializeField, Header("難易度アウトラインマテリアル")] private Material _difficultyFontMaterial;
    [SerializeField] private List<GameObject> _difficultys=new(5);
    private TextMeshProUGUI _text=new();
    private List<GameObject> gameObjects = new(MusicManager.CAPACITY);

    [SerializeField] private TMP_FontAsset _fontAsset;
    [SerializeField] private Texture2D _fontTexture;

    [SerializeField] private TMP_FontAsset _CheckfontAsset;

    private void Awake()
    {
        _fontAsset.atlas=_fontTexture;
        Debug.Log(_CheckfontAsset);

        gameObjects = MusicManager.instance.GetMusicCards();
        for (int i = 0; i < MusicManager.CAPACITY; i++)
        {
            // スコアテキストのマテリアル変更
            GameObject gameObject = gameObjects[i].transform.GetChild(1).gameObject;
            // スコアの数値
            _text = gameObject.GetComponent<TextMeshProUGUI>();
            _text.fontMaterial = rankAndScoreFontMaterial;
            // 「Score:」
            _text = gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            _text.fontMaterial = rankAndScoreFontMaterial;

            // クリアランクのマテリアル変更
            gameObject = gameObjects[i].transform.GetChild(6).gameObject;
            // プラスマークのアウトライン
            _text = gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            _text.fontMaterial = rankAndScoreFontMaterial;
            // プラスマーク
            _text = gameObject.transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            _text.fontMaterial = _nomalFontMaterial;
            // スコアランクのアウトライン
            _text = gameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
            _text.fontMaterial = rankAndScoreFontMaterial;
            // マスクがついてるほうのスコアランク
            _text = gameObject.transform.GetChild(1).transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            _text.fontMaterial = _nomalFontMaterial;
            // 「Rank」
            _text = gameObject.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
            _text.fontMaterial = rankAndScoreFontMaterial;

            // 難易度マテリアル設定
            if (i > _difficultys.Count - 1) return;
            _text = _difficultys[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>();
            _text.fontMaterial = _difficultyFontMaterial;
        }
    }
}
