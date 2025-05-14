using System.Collections.Generic;
using UnityEngine;

public class LevelMeter : MonoBehaviour
{
    [SerializeField] private AudioSpectrum spectrum;
    [SerializeField] private List<Transform> objects;
    [SerializeField] private float scale;

    // �T���v�����O�œ���Hz�Ŕz��ɓ����Ă���I�u�W�F�N�g�𓮂���

    private void Update()
    {
        for (int i = 0; i < objects.Count; i++)
        {
            var cube = objects[i];
            var localScale = cube.localScale;
            localScale.y = spectrum.Levels[i] * scale;
            if(localScale.y <= 0.1f) localScale.y = 0.1f;
            cube.localScale = localScale;
        }
    }
}