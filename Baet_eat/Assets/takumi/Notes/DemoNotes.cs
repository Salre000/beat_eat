using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoNotes : MonoBehaviour
{
    private float BaseSpeed = 20;

    private bool ActionFlag = false;
    private void FixedUpdate()
    {
        transform.position += new Vector3(0,0, BaseSpeed*OptioStatus.GetNotesSpeed()/50);

        if (transform.position.z < -11 && !ActionFlag) 
        {
            //���茋�ʂ��o��
            OptisonUility.DCStart();

            ActionFlag = true;
        }


        if (transform.position.z < -20) { transform.position += new Vector3(0, 0, 100); ActionFlag = false; }
    }
}
