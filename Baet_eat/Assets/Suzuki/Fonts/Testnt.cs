using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Testnt : MonoBehaviour
{
    TextMeshProUGUI m_TextMeshProUGUI;
    [SerializeField]Material m_Material;

    // Start is called before the first frame update
    void Start()
    {
        m_TextMeshProUGUI = GetComponent<TextMeshProUGUI>();
        m_TextMeshProUGUI.fontMaterial = m_Material;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
