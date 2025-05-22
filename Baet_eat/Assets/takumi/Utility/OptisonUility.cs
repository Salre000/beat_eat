using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class OptisonUility
{
    public static OptionManager optionManager { set; private get; }

    public static void DCStart() {  optionManager.DCStart(); }
    public static void SetHitPos(Vector2 pos) { optionManager.SetHitPos(pos); }

}
