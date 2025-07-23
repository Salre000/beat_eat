using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

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

    public static string nextSceneNameSystem = "None";
    private string nextSceneName = "";
    private string nowSceneName = "";

    void Start()
    {

        tilesX = Mathf.CeilToInt(canvas.rect.width / tileSize);
        tilesY = Mathf.CeilToInt(canvas.rect.height / tileSize);

        Initialize();
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(GameSceneManager.changeScene));

        StartCoroutine(AnimationBottomLeftToUpperRight());
    }

    void Initialize()
    {
        int index = 0;
        objects.Add(tile);
        rectTransforms.Add(tile.GetComponent<RectTransform>());

        SetNextScene();

        // タイルの生成と非アクティブ化させる
        for (int i = 0; i <= tilesX + tilesY - 2; i++)
        {
            for (int x = 0; x <= i; x++)
            {
                int y = i - x;
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
        //第一引数　が行き先
        async = SceneManager.LoadSceneAsync(nextSceneName, LoadSceneMode.Additive);
        async.allowSceneActivation = false;
        for (int i = 0; i <= tilesX + tilesY - 2; i++)
        {
            for (int x = 0; x <= i; x++)
            {
                int y = i - x;
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
        //第一引数が現在のシーン
        unLoadAsync = SceneManager.UnloadSceneAsync(nowSceneName);
        Scene activeScene = SceneManager.GetActiveScene();
        while (unLoadAsync.isDone)
        {
            yield return null;
        }
        while (async.progress<0.9f)
        {
            yield return null;
        }
        async.allowSceneActivation = true;
        int index = objects.Count-1;
        for (int i = tilesX + tilesY - 2; i >= 0; i--)
        {
            for (int x = 0; x <= i; x++)
            {
                int y = i - x;
                if (x < tilesX && y < tilesY)
                {
                    objects[index].SetActive(false);
                    index--;
                }
            }
            yield return new WaitForSeconds(delay);
        }
        objects[0].SetActive(false);
        //第一引数が移動先
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(nextSceneName));
        SceneManager.UnloadSceneAsync(GameSceneManager.changeScene);
    }

    // 難易度で色分け
    private void ChangeColor()
    {
        for (int i = 0; i < images.Count; i++)
        {
            switch ((int)ScoreStatus.nowDifficulty)
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

    private void SetNextScene() 
    {
        nowSceneName = SceneManager.GetActiveScene().name;

        switch (nowSceneName) 
        {
            case GameSceneManager.selectScene:nextSceneName = GameSceneManager.isTargetTitle? GameSceneManager.titleScene: GameSceneManager.loadScene/*GameSceneManager.loadScene*/; break;
            case GameSceneManager.resultScene:nextSceneName = GameSceneManager.selectScene; break;
            case GameSceneManager.mainScene:nextSceneName = GameSceneManager.resultScene; break;
            case GameSceneManager.loadScene:nextSceneName = GameSceneManager.mainScene; break;

        }

        if (nextSceneNameSystem == "None") return;
        nextSceneName = nextSceneNameSystem;
        nextSceneNameSystem = "None";

    }
}