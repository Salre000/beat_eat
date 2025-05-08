using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotesBase : MonoBehaviour
{
    //”»’èƒ^ƒCƒv
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
        //Ž©•ª‚ðŒ©‚¦‚È‚­‚·‚é
        this.gameObject.SetActive(false);






    }

    public void FixedUpdate()
    {
        
    }


}
