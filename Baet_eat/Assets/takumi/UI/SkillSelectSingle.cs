using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SkillSelectSingle : MonoBehaviour
{
    [SerializeField] GameObject SkillJammer;
    [SerializeField] TextMeshProUGUI description;
    public void OpenJammer() { SkillJammer.SetActive(true); description.text = SkillManager.instance.GetDescription(); }
    public void CloseJammer() { SkillJammer.SetActive(false); }

}
