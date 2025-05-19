using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ScoreUtility 
{
    public static ScoreManager scoreManager {  set; private get; }

    public static void ChengeDifficulty() { scoreManager.ChengeDifficulty(); }

}