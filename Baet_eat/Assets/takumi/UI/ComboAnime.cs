using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboAnime : MonoBehaviour
{
    readonly private Vector3 _startPos=new Vector3(0,1,-3.8f);

    private Vector3 _endPos;

    private float t=1;

    private float moveSpeedRate = 5;
    private void Awake()
    {
        _endPos = transform.position;
        Debug.Log(_endPos + "‰Šú’l");
    }

    private void FixedUpdate()
    {
        if (t > 1) return;

        t += Time.deltaTime* moveSpeedRate;

        transform.position=Vector3.Lerp(_startPos,_endPos, t);

    }

    public void StartAnime() 
    {
        t = 0;

    }


}
