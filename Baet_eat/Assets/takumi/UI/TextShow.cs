using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TextShow : MonoBehaviour
{


    public static bool showFlag = false;
    public static string showText = "TEST";

    float t = 0;

    [SerializeField] RectTransform endPos;
    [SerializeField] RectTransform startPos;

    private static List<System.Action> EndAction=new List<System.Action>();
    public static void AddEndAction(System.Action action) {EndAction.Add(action);}
    private void Awake()
    {
        transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = showText;
        showFlag = true;
    }
    private float speedRate = 1;
    private void FixedUpdate()
    {
        if (t > 0.45f && t < 0.55) speedRate = 6;
        else speedRate = 1;

        t += Time.deltaTime / (5.0f * speedRate);


        transform.position = Vector3.Lerp(startPos.position, endPos.position, t);
        transform.GetChild(0).GetChild(0).transform.position = new Vector2(Screen.width / 2, Screen.height / 2);
        if (t < 1) return;
        for (int i = 0; i < EndAction.Count; i++) EndAction[i]();
        EndAction.Clear();

        SceneManager.UnloadSceneAsync("TextScene");


    }
    private void OnDisable()
    {
        showFlag = false;
    }


}
