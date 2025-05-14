using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelMeter : MonoBehaviour
{
    [SerializeField] private AudioSpectrum spectrum;
    [SerializeField] private List<Transform> objects;
    [SerializeField] private float scale;
    private List<Image> images;

    // サンプリングで得たHzで配列に入っているオブジェクトを動かす

    private void Awake()
    {
        images = new(objects.Count);
        for(int i = 0; i < objects.Count; i++)
        {
            images.Add(objects[i].GetComponent<Image>());
        }
    }

    private void Update()
    {
        ChangeColor();
        for (int i = 0; i < objects.Count; i++)
        {
            var cube = objects[i];
            var localScale = cube.localScale;
            localScale.y = spectrum.Levels[i] * scale;
            if(localScale.y <= 0.1f) localScale.y = 0.1f;
            cube.localScale = localScale;
        }
    }

    // ついでにカラーを難易度に寄せてみる
    private void ChangeColor()
    {
        if(MusicManager.instance.IsChangeDifficulty()) return;
        for (int i = 0;i < objects.Count;i++)
        {
            switch (MusicManager.instance.GetDifficultyNumber())
            {
                case 0:
                    images[i].color = ColorManager.DRINK_COLOR_TRANSLUCENT;
                    break;
                case 1:
                    images[i].color = ColorManager.HORSDOEUVRE_COLOR_TRANSLUCENT;
                    break;
                case 2:
                    images[i].color = ColorManager.SOUP_COLOR_TRANSLUCENT;
                    break;
                case 3:
                    images[i].color = ColorManager.MAINDISH_COLOR_TRANSLUCENT;
                    break;
                case 4:
                    images[i].color = ColorManager.DESSERT_COLOR_TRANSLUCENT;
                    break;
            }
        }
    }
}