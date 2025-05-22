using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SetFontMaterial : MonoBehaviour
{
    // ���݂̃X�R�A
    [SerializeField] TextMeshProUGUI _scoreText;
    // �ǂ�Ŏ��̔@���uLife�v
    [SerializeField] List<TextMeshProUGUI> _lifeText;
    // ���݂̃X�R�A�����N
    [SerializeField] List<TextMeshProUGUI> _rankText;
    // �Q�[�W���C���̃����N
    [SerializeField] List<TextMeshProUGUI> _lineRank;
    // ���A�E�g���C��
    [SerializeField]Material _scoreMaterial;
    // �D�A�E�g���C��
    [SerializeField]Material _lifeMaterial;
    // ���A�E�g���C��
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
