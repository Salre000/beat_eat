using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class publicEnum
{ 

    //�Ȃ̓�Փx�̗񋓑�
    public enum Difficulty 
    {
        None=-1,
        Drink,
        Hors_d_oeuvre,
        Soup,
        MainDish,
        dessert,
        MAX
    }

    //�N���A�����N�̗񋓑�
    public enum ClearRank
    {
        None=-1,
        SPlus,
        S,
        A,
        B,
        C,
        D,
        MAX

    }

    public enum ClearStates 
    {
        None=-1,
        Unplayed,
        Clear,
        FullCombo,
        ALLDC,
        MAX

    }

    public static int ChengeInt(this ClearRank rank) 
    {
        return (int)rank;
    }
    public static int ChengeInt(this Difficulty rank) 
    {
        return (int)rank;
    }







}
