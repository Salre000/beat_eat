using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotesBase : MonoBehaviour
{
    //����^�C�v
    protected enum JudgmentType
    {
        DC,
        Delicious,
        Yammy,
        Good,
        Miss

    }

    public void Hit() 
    {
        //�����������Ȃ�����
        this.gameObject.SetActive(false);






    }

    public void FixedUpdate()
    {
        
    }


}
