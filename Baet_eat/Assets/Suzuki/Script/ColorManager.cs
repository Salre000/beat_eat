using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ColorManager
{
    private const byte _ALFA_COLOR = 150;
    // 難易度別カラー translucent
    public static readonly Color32 DRINK_COLOR = new Color32(42, 144, 255, 255);
    public static readonly Color32 HORSDOEUVRE_COLOR = new Color32(47, 255, 32, 255);
    public static readonly Color32 SOUP_COLOR = new Color32(255, 180, 25, 255);
    public static readonly Color32 MAINDISH_COLOR = new Color32(255, 40, 40, 255);
    public static readonly Color32 DESSERT_COLOR = new Color32(200, 0, 255, 255);

    // スぺクラム用半透明
    public static readonly Color32 DRINK_COLOR_TRANSLUCENT = new Color32(105, 177, 255, _ALFA_COLOR);
    public static readonly Color32 HORSDOEUVRE_COLOR_TRANSLUCENT = new Color32(111, 255, 100, _ALFA_COLOR);
    public static readonly Color32 SOUP_COLOR_TRANSLUCENT = new Color32(255, 200, 83, _ALFA_COLOR);
    public static readonly Color32 MAINDISH_COLOR_TRANSLUCENT = new Color32(255, 110, 110, _ALFA_COLOR);
    public static readonly Color32 DESSERT_COLOR_TRANSLUCENT = new Color32(220, 100, 255, _ALFA_COLOR);
}
