using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class TextShow : MonoBehaviour
{


    public static bool showFlag = false;
    public static string showText = "TEST";
    public static float OFFSet = 0;
    public static float Speed = 1;

    float t = 0;

    [SerializeField] RectTransform endPos;
    [SerializeField] RectTransform startPos;

    private static List<System.Action> EndAction = new List<System.Action>();
    public static void AddEndAction(System.Action action) { EndAction.Add(action); }
    public static System.Action textShow;
    private void Awake()
    {
        textShow = EndAction[0];


        transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = showText;
        showFlag = true;
    }
    private float speedRate = 1;
    private void Update()
    {
        Skip();
        if (t > 0.45f && t < 0.55) speedRate = 6;
        else speedRate = 1;

        t += (Time.deltaTime / (5.0f * speedRate)) * Speed;


        transform.position = Vector3.Lerp(startPos.position + new Vector3(0, OFFSet), endPos.position + new Vector3(0, OFFSet), t);
        transform.GetChild(0).GetChild(0).transform.position = new Vector2(Screen.width / 2, Screen.height / 2);
        if (t < 1) return;

        OFFSet = 0;
        Speed = 1;


        textShow();
        EndAction.RemoveAt(0);

        SceneManager.UnloadSceneAsync("TextScene");
    }
    private void Skip()
    {
        if (NotesMove.Instance == null) return;
        if (Input.touchCount == 0) return;
        if (!NotesMove.Instance.stopFlag) return;
        Speed = 8;

    }
    private void OnDisable()
    {
        showFlag = false;
    }


}
