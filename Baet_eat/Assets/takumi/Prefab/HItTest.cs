using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HItTest : MonoBehaviour
{

    private void Awake()
    {
        transform.localScale = new Vector3(0.5f, 1, 2f* LineUtility.HitTestRate);
    }
}
