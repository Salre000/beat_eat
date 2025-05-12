using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotesManager : MonoBehaviour
{

    private List<Hands> hand=new List<Hands>(12);

    private struct Hands 
    {
        Vector2 HandPosition;

        bool TapFlag;

        int TapID;
    
    }





    public void Awake()
    {
        
    }

    public void FixedUpdate()
    {




        
    }


}
