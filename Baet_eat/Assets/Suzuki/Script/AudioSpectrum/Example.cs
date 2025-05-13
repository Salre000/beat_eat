using UnityEngine;

public class Example : MonoBehaviour
{
    public AudioSpectrum spectrum;
    public Transform[] objects;
    public float scale;

    private void Update()
    {
        for (int i = 0; i < objects.Length; i++)
        {
            var cube = objects[i];
            var localScale = cube.localScale;
            localScale.y = spectrum.Levels[i] * scale;
            if(localScale.y <= 0.1f) localScale.y = 0.1f;
            cube.localScale = localScale;
        }
    }
}