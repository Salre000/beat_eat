using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NotesMove : MonoBehaviour
{
    protected const float BaseSpeed = 20;//20
    protected Vector3 Vec = new Vector3(0, 0, (BaseSpeed * OptionStatus.GetNotesSpeed()) / 50.0f);
    public static NotesMove Instance;
    public bool stopFlag = false;
    public void Awake()
    {
        Instance = this;
    }
    private void FixedUpdate()
    {
        if (stopFlag) return;

        this.transform.position -= Vec;
    }
}
