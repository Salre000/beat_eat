using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    [SerializeField] GameObject Effect;

    public static EffectManager instance;
    private void Awake()
    {
        instance = this;
    }
    public void StartEffect(Vector3 Pos)
    {
        GameObject effect = GameObject.Instantiate(Effect);
        effect.transform.position = Pos;

    }


}
