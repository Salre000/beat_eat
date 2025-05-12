using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotesManager : MonoBehaviour
{






    public void Awake()
    {
        
    }

    public void FixedUpdate()
    {
        List<HandManager.Hands> hands = HandUtility.GetHands();

        for(int i = 0; i < hands.Count; i++) 
        {
            if (!hands[i].flag) continue;

            LineUtility.GetTapArea().GetClickPoint(hands[i].HandPosition, Click,i);


        }

        ////デバッグ用

        //if(Input.GetMouseButton(0)) LineUtility.GetTapArea().GetClickPoint(Input.mousePosition, Click,0);



    }

    public void Click(int index, int id) 
    {
        LineUtility.Click(index, id);
    }


}
