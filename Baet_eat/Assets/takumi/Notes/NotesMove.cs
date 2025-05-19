using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotesMove : MonoBehaviour
{
    protected const float BaseSpeed = 20;//20
    protected Vector3 Vec = new Vector3(0, 0, (BaseSpeed * InGameStatus.GetSpeed()) / 50.0f);

    private void FixedUpdate()
    {
        this.transform.position -= Vec;

    }
}
