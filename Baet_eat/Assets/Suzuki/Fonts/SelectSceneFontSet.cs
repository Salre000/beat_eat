using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SelectSceneFontSet : MonoBehaviour
{
    // SelectSceneのフォントマテリアルをセットする
    [SerializeField,Header("アウトライン用マテリアル")] private Material rankAndScoreFontMaterial;
    [SerializeField, Header("共通マテリアル")] private Material _nomalFontMaterial;
    private TextMeshProUGUI _text=new();
    private List<GameObject> gameObjects = new(MusicManager.CAPACITY);

    private void Awake()
    {
        for(int i = 0; i < MusicManager.CAPACITY; i++)
        {
            // スコアテキストのマテリアル変更
            GameObject gameObject = gameObjects[i].transform.GetChild(1).gameObject;
            // スコアの数値
            _text=gameObject.GetComponent<TextMeshProUGUI>();
            _text.fontMaterial = rankAndScoreFontMaterial;
            // 「Score:」
            _text=gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            _text.fontMaterial = rankAndScoreFontMaterial;

            // クリアランクのマテリアル変更
            gameObject = gameObjects[i].transform.GetChild(6).gameObject;
            // プラスマークのアウトライン
            _text = gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            _text.fontMaterial= rankAndScoreFontMaterial;
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
        }
    }
}
