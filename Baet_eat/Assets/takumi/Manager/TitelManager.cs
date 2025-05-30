using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitelManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI pushTo;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private bool oneFlag = false;
    public void FixedUpdate()
    {
        if (Input.GetMouseButton(0)&&!oneFlag) 
        {
            oneFlag = true;
           ChengeSelect();

        }
        AlphaChenge();
    }

    float alpha =0.01f;
   private void AlphaChenge() 
    {
        Color color=pushTo.color;
        color.a += alpha;
        if (color.a >= 1 || color.a <= 0) alpha *= -1;
        pushTo.color = color;

    }
    public void ChengeSelect() 
    {
        TransitionEffect.nextSceneNameSystem = GameSceneManager.selectScene;

        GameSceneManager.LoadScene(GameSceneManager.changeScene, LoadSceneMode.Additive);


    }

}
