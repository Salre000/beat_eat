using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Judgment : MonoBehaviour
{
    float time = 0;
    private const float MAXTIME = 0.5f;

    public void OnEnable()
    {
        //•ÛŒ¯
        time = 0;

    }
    public void OnDisable()
    {
        //•ÛŒ¯
        time = 0;

    }
    public void FixedUpdate()
    {
        time += Time.deltaTime;

        if (time < MAXTIME) return;

        gameObject.SetActive(false);
        time = 0;

    }
}
