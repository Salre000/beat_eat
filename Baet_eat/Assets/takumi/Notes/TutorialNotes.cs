using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static NotesBase;

public class TutorialNotes : NotesBase
{
    public void Start()
    {
        if (this.gameObject.transform.GetChild(0) == null) return;
        if (this.transform.GetChild(0).transform.childCount == 0) return;
        this.transform.GetChild(0).AddComponent<TutorialNotes>();
        this.transform.GetChild(1).AddComponent<TutorialNotes>();

        Destroy(this);



    }
    public void OnDisable()
    {
        SoundUtility.MainBGMStart();
        NotesMove.Instance.stopFlag = false;

    }

    private bool oneFlag = false;
    public void FixedUpdate()
    {
        if (this.transform.position.z <=-6.25f&&!oneFlag)
        {
            oneFlag = true;
            SoundUtility.MainBGMStop();
            NotesMove.Instance.stopFlag = true;

            if (InGameStatus.GetAuto()) 
            {
                NotesMove.Instance.stopFlag = false;
                SoundUtility.MainBGMStart();


            }

        }

    }

}
