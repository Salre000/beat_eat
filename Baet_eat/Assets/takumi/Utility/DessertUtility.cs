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

        //���C�������点��


        //�T�C�h�ɑ����Ă���m�[�c���擾
        for (int i = 0; i <0; i++)
        {

            return;


        }

    }



}