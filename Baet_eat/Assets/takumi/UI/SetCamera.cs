using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCamera : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       DessertManager.mainCamera = this.gameObject;
        
    }

    private void FixedUpdate()
    {
        if (DessertManager.mainCamera != null) return;

        DessertManager.mainCamera = gameObject;
        
    }

}
