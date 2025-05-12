using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HandUtility
{

    public static HandManager handManager { private get; set; }


    public static Vector2 handPosition(int id) { return handManager.GetPosition(id); }

    public static void AddEndAction(System.Action action, int ID)
    {
        handManager.AddEndAction(action, ID);

    }

    public static List<HandManager.Hands> GetHands() {return  handManager.GetHand(); }
}