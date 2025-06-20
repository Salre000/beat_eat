using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EX 
{

    public static string GetName(this publicEnum.Difficulty difficulty) 
    {
        switch (difficulty)
        {
            case publicEnum.Difficulty.Drink:
                return "Drink";
            case publicEnum.Difficulty.Hors_d_oeuvre:
                return "HorsDoeuvre";
            case publicEnum.Difficulty.Soup:
                return "Soup";
            case publicEnum.Difficulty.MainDish:
                return "MainDish";
            case publicEnum.Difficulty.dessert:
                return "Dessert";
        }

        return "";
    }


}