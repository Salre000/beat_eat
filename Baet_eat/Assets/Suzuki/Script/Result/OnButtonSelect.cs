using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnButtonSelect : MonoBehaviour
{
    private Button mButton;

    private void Awake()
    {
        mButton = GetComponent<Button>();
        mButton.onClick.AddListener(OnButton);
    }

    private void OnButton()
    {
        GameSceneManager.LoadScene(GameSceneManager.selectScene);
    }
}
