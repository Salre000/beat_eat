using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NotesSize : MonoBehaviour
{
    public static int m_Size = 1;
    public TextMeshProUGUI text;
    // Start is called before the first frame update
    public void Awake()
    {
        
    }
    public void FixedUpdate()
    {

        if (Input.GetKey(KeyCode.Alpha0)) m_Size = 0;
        if (Input.GetKey(KeyCode.Alpha1)) m_Size = 1;
        if (Input.GetKey(KeyCode.Alpha2)) m_Size = 2;
        if (Input.GetKey(KeyCode.Alpha3)) m_Size = 3;
        if (Input.GetKey(KeyCode.Alpha4)) m_Size = 4;
        if (Input.GetKey(KeyCode.Alpha5)) m_Size = 5;
        if (Input.GetKey(KeyCode.Alpha6)) m_Size = 6;
        if (Input.GetKey(KeyCode.Alpha7)) m_Size = 7;
        if (Input.GetKey(KeyCode.Alpha8)) m_Size = 8;
        if (Input.GetKey(KeyCode.Alpha9)) m_Size = 9;

        text.text = "Add Note Size" + m_Size;

    }

}
