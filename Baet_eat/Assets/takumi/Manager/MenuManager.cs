using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField]Canvas _canvas;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    //ƒƒjƒ…[‚Ìó‹µ‚ğ”½“]
    public void ChengeMenu() 
    {
        _canvas.gameObject.SetActive(!_canvas.gameObject.activeSelf);
    }
}
