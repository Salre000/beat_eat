using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoNotes : MonoBehaviour
{
    private float BaseSpeed = 20;

    private bool ActionFlag = false;

    [SerializeField]Camera _camera;
    private void FixedUpdate()
    {
        transform.position -= new Vector3(0,0, BaseSpeed*OptionStatus.GetNotesSpeed()/50);


        if (transform.position.z < -11+(OptionStatus.GetNotesHitLinePos()*0.1f) && !ActionFlag) 
        {
            //”»’èŒ‹‰Ê‚ðo‚·
            OptisonUility.DCStart();
            OptisonUility.SetHitPos(transform.localPosition);

            ActionFlag = true;
        }


        if (transform.position.z < -20) 
        { transform.position += new Vector3(0, 0, 100); ActionFlag = false; }
    }
}
