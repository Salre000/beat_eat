using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class TransitionEffect : MonoBehaviour
{
    [SerializeField] private GameObject tile;
    private List<GameObject> objects = new List<GameObject>();
    private List<RectTransform> rectTransforms = new List<RectTransform>();
    private List<Image> images = new List<Image>();
    [SerializeField] private RectTransform canvas;

    [SerializeField] private int tileSize = 80;
    [SerializeField] private float delay = 0.01f;

    private int tilesX;
    private int tilesY;

    private AsyncOperation async;
    private AsyncOperation unLoadAsync;

    void Start()
    {

        tilesX = Mathf.CeilToInt(/*1600f*/canvas.rect.width / tileSize);
        tilesY = Mathf.CeilToInt(/*720f*/canvas.rect.height / tileSize);

        Initialize();
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(GameSceneManager.changeScene));

        StartCoroutine(AnimationBottomLeftToUpperRight());
    }

    void Initialize()
    {
        int index = 0;
        objects.Add(tile);
        rectTransforms.Add(tile.GetComponent<RectTransform>());
        // タイルの生成と非アクティブ化させる
        for (int sum = 0; sum <= tilesX + tilesY - 2; sum++)
        {
            for (int x = 0; x <= sum; x++)
            {
                int y = sum - x;
                if (x < tilesX && y < tilesY)
                {
                    objects.Add(Instantiate(tile, canvas));
                    rectTransforms.Add(objects[index].GetComponent<RectTransform>());
                    rectTransforms[index+1].anchoredPosition = new Vector2(x * tileSize, y * tileSize);
                    images.Add(objects[index].GetComponent<Image>());
                    objects[index].SetActive(false);
                    index++;
                }
            }
        }
        ChangeColor();
    }

    //  左下から対角線上に画面を閉じていく
    IEnumerator AnimationBottomLeftToUpperRight()
    {
        int index = 0;
        async = SceneManager.LoadSceneAsync(GameSceneManager.loadScene, LoadSceneMode.Additive);
        async.allowSceneActivation = false;
        for (int sum = 0; sum <= tilesX + tilesY - 2; sum++)
        {
            for (int x = 0; x <= sum; x++)
            {
                int y = sum - x;
                if (x < tilesX && y < tilesY)
                {
                    objects[index].SetActive(true);
                    index++;
                }
            }
            yield return new WaitForSeconds(delay);
        }
        StopCoroutine(AnimationBottomLeftToUpperRight());
        StartCoroutine(AnimationUpperRightToBottomLeft());
    }

    // 右上から画面を開いていく
    IEnumerator AnimationUpperRightToBottomLeft()
    {
        unLoadAsync = SceneManager.UnloadSceneAsync(GameSceneManager.selectScene);
        Scene activeScene=SceneManager.GetActiveScene();
        while (!unLoadAsync.isDone)
        {
            yield return null;
        }
        async.allowSceneActivation = true;
        int index = objects.Count-1;
        for (int sum = tilesX + tilesY - 2; sum >= 0; sum--)
        {
            for (int x = 0; x <= sum; x++)
            {
                int y = sum - x;
                if (x < tilesX && y < tilesY)
                {
                    objects[index].SetActive(false);
                    index--;
                }
            }
            yield return new WaitForSeconds(delay);
        }
        objects[0].SetActive(false);
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(GameSceneManager.loadScene));
        SceneManager.UnloadSceneAsync(GameSceneManager.changeScene);
    }

    // 難易度で色分け
    private void ChangeColor()
    {
        for (int i = 0; i < images.Count; i++)
        {
            switch (MusicManager.instance.GetDifficultyNumber())
            {
                case 0:
                    images[i].color = ColorManager.DRINK_COLOR;
                    break;
                case 1:
                    images[i].color = ColorManager.HORSDOEUVRE_COLOR;
                    break;
                case 2:
                    images[i].color = ColorManager.SOUP_COLOR;
                    break;
                case 3:
                    images[i].color = ColorManager.MAINDISH_COLOR;
                    break;
                case 4:
                    images[i].color = ColorManager.DESSERT_COLOR;
                    break;
            }
        }
    }
}