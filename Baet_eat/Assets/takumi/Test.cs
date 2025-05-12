using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textMeshProUGUI1;

    void Start()
    {

    }

    void Update()
    {
        if (Input.touchCount == 0) return;

        textMeshProUGUI1.text = Input.GetTouch(0).position.ToString();

        int count=Input.touchCount;

    }
}
