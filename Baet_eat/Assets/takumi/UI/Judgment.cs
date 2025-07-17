using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Judgment : MonoBehaviour
{
    float time = 0;
    private const float MAXTIME = 0.3f;

    public static GameObject parent;

    private List<System.Action> endAction = new List<System.Action>();
    public void SetEndAction(System.Action action) { endAction.Add(action); }

    public void OnEnable()
    {
        //•ÛŒ¯
        time = 0;


    }
    public void OnDisable()
    {
        for (int i = 0; i < endAction.Count; i++) endAction[i]();
        //•ÛŒ¯
        time = 0;
        endAction.Clear();

    }
    public void FixedUpdate()
    {
        time += Time.deltaTime;

        if (time < MAXTIME) return;

        for (int i = 0; i < endAction.Count; i++) endAction[i]();
        endAction.Clear();


        gameObject.SetActive(false);
        time = 0;

    }
}
