using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotesLinePool : MonoBehaviour
{

    private List<GameObject> linePool=new List<GameObject>();
    [SerializeField] GameObject origin;
    [SerializeField] int lineCount = 30;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < lineCount; i++) 
        {
            linePool.Add(GameObject.Instantiate(origin));

            linePool[i].SetActive(false);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
