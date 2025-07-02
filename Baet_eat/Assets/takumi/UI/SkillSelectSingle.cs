using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSelectSingle : MonoBehaviour
{
    [SerializeField] GameObject SkillJammer;

    public void OpenJammer() { SkillJammer.SetActive(true); }
    public void CloseJammer() { SkillJammer.SetActive(false); }

}
