using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DessertUtility 
{

    public static CreateDessertGame dessertGame;

    public static void CheckSideTap(Vector2 vector2,int id) 
    {
        if (dessertGame == null) return;

        dessertGame.GetClickPoint(vector2, id);
    }

    public static void Click(int index, int id)
    {
            HandUtility.AddEndAction(() => { }, id);

        //ラインを光らせる


        //サイドに属しているノーツを取得
        for (int i = 0; i <0; i++)
        {

            return;


        }

    }



}