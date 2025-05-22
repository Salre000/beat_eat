using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoNotes : MonoBehaviour
{
    private float BaseSpeed = 20;
    private void FixedUpdate()
    {
        transform.position += new Vector3(0,0, BaseSpeed*OptioStatus.GetNotesSpeed()/50);
        
        if(transform.position.z<-20)transform.position+= new Vector3(0, 0,50);
    }
}
