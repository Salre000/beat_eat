using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SetFontMaterial : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _scoreText;
    [SerializeField]Material _outLineMaterial;

    // Start is called before the first frame update
    void Awake()
    {
        _scoreText.fontMaterial = _outLineMaterial;
    }
}
