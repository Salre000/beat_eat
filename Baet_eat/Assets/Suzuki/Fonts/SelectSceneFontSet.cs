using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SelectSceneFontSet : MonoBehaviour
{
    // SelectScene�̃t�H���g�}�e���A�����Z�b�g����
    [SerializeField,Header("�A�E�g���C���p�}�e���A��")] private Material rankAndScoreFontMaterial;
    [SerializeField, Header("���ʃ}�e���A��")] private Material _nomalFontMaterial;
    private TextMeshProUGUI _text=new();
    private List<GameObject> gameObjects = new(MusicManager.CAPACITY);

    private void Awake()
    {
        for(int i = 0; i < MusicManager.CAPACITY; i++)
        {
            // �X�R�A�e�L�X�g�̃}�e���A���ύX
            GameObject gameObject = gameObjects[i].transform.GetChild(1).gameObject;
            // �X�R�A�̐��l
            _text=gameObject.GetComponent<TextMeshProUGUI>();
            _text.fontMaterial = rankAndScoreFontMaterial;
            // �uScore:�v
            _text=gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            _text.fontMaterial = rankAndScoreFontMaterial;

            // �N���A�����N�̃}�e���A���ύX
            gameObject = gameObjects[i].transform.GetChild(6).gameObject;
            // �v���X�}�[�N�̃A�E�g���C��
            _text = gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            _text.fontMaterial= rankAndScoreFontMaterial;
            // �v���X�}�[�N
            _text = gameObject.transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            _text.fontMaterial = _nomalFontMaterial;
            // �X�R�A�����N�̃A�E�g���C��
            _text = gameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
            _text.fontMaterial = rankAndScoreFontMaterial;
            // �}�X�N�����Ă�ق��̃X�R�A�����N
            _text = gameObject.transform.GetChild(1).transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            _text.fontMaterial = _nomalFontMaterial;
            // �uRank�v
            _text = gameObject.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
            _text.fontMaterial = rankAndScoreFontMaterial;
        }
    }
}
