using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SetFontMaterial : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _scoreText;
    [SerializeField] List<TextMeshProUGUI> _lifeText;
    [SerializeField] List<TextMeshProUGUI> _rankText;
    [SerializeField]Material _scoreMaterial;
    [SerializeField]Material _lifeMaterial;
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
            _scoreText.fontMaterial = _scoreMaterial;
    }
}
