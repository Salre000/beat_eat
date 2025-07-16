using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCamera : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (DessertUtility.dessertGame != null) DessertManager.mainCamera = this.gameObject;
        
    }

}
