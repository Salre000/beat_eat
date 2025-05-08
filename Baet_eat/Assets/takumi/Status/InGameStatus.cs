using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InGameStatus
{
    private static int score = 0;

    private static int HP = 1000;

    private static int damege = 50;

    public static int GetScore(){ return score; }
    public static int GetHP(){ return HP; }

    public static void AddScore(int addCount) {  score += addCount; }

    public static void HPDamege() { HP-=damege; }


}