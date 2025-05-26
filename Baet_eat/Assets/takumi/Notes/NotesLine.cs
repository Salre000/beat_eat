using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotesLine : MonoBehaviour
{


    private void FixedUpdate()
    {
        if (transform.position.z > -20) return;

        this.gameObject.SetActive(false);
        
    }
}
