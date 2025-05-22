using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LifeGage : MonoBehaviour
{
    // hpゲージ、それに伴うテキストの変動

    private float _nowHp = 0f;
    private readonly float _STARTHP=InGameStatus.GetMAXHP();
    [SerializeField] TextMeshProUGUI _nowHpText = new TextMeshProUGUI();
    private StringBuilder _stringBuiluder = new StringBuilder();
    [SerializeField] private Image _lifeGage;
    private bool _isOverHeal = false;


    // Start is called before the first frame update
    void Start()
    {
        _nowHp=_STARTHP;
        BuildingString(_nowHp);
        InGameStatus.SetChengeHPUIAction(ChangedHp);
    }

    // hpが変動した時に呼ぶ
    private void ChangedHp()
    {
        if (_nowHp <= 0){ _nowHp = 0; return; }
        if (!_isOverHeal)
            HpPercent();
    }

    private void HpPercent()
    {
        _nowHp = InGameStatus.GetHP();
        float value = _nowHp / _STARTHP;
        _lifeGage.fillAmount = value;
        BuildingString(_nowHp);
    }

    private void BuildingString(float value)
    {
        _stringBuiluder.Clear();
        _stringBuiluder.Append(value);
        _nowHpText.text = _stringBuiluder.ToString();
    }
}
