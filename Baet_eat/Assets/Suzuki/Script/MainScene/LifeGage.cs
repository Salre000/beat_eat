using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class LifeGage : MonoBehaviour
{
    // hpゲージ、それに伴うテキストの変動

    private float _nowHp = 0f;
    private readonly float _STARTHP=InGameStatus.GetMAXHP();
    [SerializeField] TextMeshProUGUI _nowHpText = new TextMeshProUGUI();
    private StringBuilder _stringBuiluder = new StringBuilder();

    // Start is called before the first frame update
    void Start()
    {
        _nowHp=_STARTHP;
        BuildingString(_nowHp);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void BuildingString(float value)
    {
        _stringBuiluder.Clear();
        _stringBuiluder.Append(value);
        _nowHpText.text = _stringBuiluder.ToString();
    }
}
