// Audio spectrum component
// By Keijiro Takahashi, 2013
// https://github.com/keijiro/unity-audio-spectrum
using UnityEngine;
using System.Collections;

public class AudioSpectrum : MonoBehaviour
{
    #region Band type definition
    public enum SmplesNumber
    {
        Smples1024=1024,
        Smples2048=2048,
        Smples4096=4096,
        Smples8192=8192,
    }

    public enum BandType {
        FourBand,
        FourBandVisual,
        EightBand,
        TenBand,
        TwentySixBand,
        ThirtyOneBand,
        SixtyFourBandSix,     // 1/6オクターブの64バンドを追加
        SixtyFourBandthiree   // 1/3オクターブの64バンドを追加
    };

    static float[][] middleFrequenciesForBands = {
        new float[]{ 125.0f, 500, 1000, 2000 },
        new float[]{ 250.0f, 400, 600, 800 },
        new float[]{ 63.0f, 125, 500, 1000, 2000, 4000, 6000, 8000 },
        new float[]{ 31.5f, 63, 125, 250, 500, 1000, 2000, 4000, 8000, 16000 },
        new float[]{ 25.0f, 31.5f, 40, 50, 63, 80, 100, 125, 160, 200, 250, 315, 400, 500, 630, 800, 1000, 1250, 1600, 2000, 2500, 3150, 4000, 5000, 6300, 8000 },
        new float[]{ 20.0f, 25, 31.5f, 40, 50, 63, 80, 100, 125, 160, 200, 250, 315, 400, 500, 630, 800, 1000, 1250, 1600, 2000, 2500, 3150, 4000, 5000, 6300, 8000, 10000, 12500, 16000, 20000 },
        // 1/6オクターブで計算
        new float[]{ 20.0f,22.4f,25.1f,28.1f,31.5f,35.3f,39.6f,44.4f,49.9f,55.9f,62.8f,70.5f,79.3f,89.1f,100.1f,112.4f,126.1f,141.3f,158.2f,176.9f,197.7f,220.8f,246.5f,274.9f,306.4f,341.2f,379.8f,422.6f,470.1f,522.7f,580.9f,645.2f,716.1f,794.0f,879.8f,974.2f,1078.0f,1192.2f,1317.7f,1455.9f,1608.0f,1775.7f,1960.7f,2164.7f,2389.5f,2637.1f,2909.9f,3210.2f,3540.7f,3904.4f,4294.9f,4715.4f,5169.7f,5661.0f,6193.0f,6769.1f,7393.0f,8069.0f,8801.4f,9595.1f,10455.2f,11387.1f,12396.6f,13490.0f},
        // 等比数列で1/3に61分割
        new float[]{ 20f, 22.44f, 25.18f, 28.25f, 31.7f, 35.57f, 39.91f, 44.77f, 50.24f, 56.37f, 63.25f, 70.96f, 79.62f, 89.34f, 100.24f, 112.47f, 126.19f, 141.59f, 158.87f, 178.25f, 200f, 224.4f, 251.79f, 282.51f, 316.98f, 355.66f, 399.05f, 447.74f, 502.38f, 563.68f, 632.46f, 709.63f, 796.21f, 893.37f, 1002.37f, 1124.68f, 1261.91f, 1415.89f, 1588.66f, 1782.5f, 2000f, 2244.04f, 2517.85f, 2825.08f, 3169.79f, 3556.56f, 3990.52f, 4477.44f, 5023.77f, 5636.77f, 6324.56f, 7096.27f, 7962.14f, 8933.67f, 10023.74f, 11246.83f, 12619.15f, 14158.92f, 15886.56f, 17825.02f, 20000f }
    };
    static float[] bandwidthForBands = {
        1.414f, // 2^(1/2)
        1.260f, // 2^(1/3)
        1.414f, // 2^(1/2)
        1.414f, // 2^(1/2)
        1.122f, // 2^(1/6)
        1.122f, // 2^(1/6)
        1.122f, // 2^(1/6)
        1.260f // 2^(1/3)
    };
    #endregion

    #region Public variables
    // FFTサイズを変更しやすいようにenumに変更
    public SmplesNumber smplesNumber = SmplesNumber.Smples1024;
    public BandType bandType = BandType.TenBand;
    public float fallSpeed = 0.08f;
    public float sensibility = 8.0f;
    #endregion

    #region Private variables
    float[] rawSpectrum;
    float[] levels;
    float[] peakLevels;
    float[] meanLevels;
    #endregion

    #region Public property
    public float[] Levels {
        get { return levels; }
    }

    public float[] PeakLevels {
        get { return peakLevels; }
    }
    
    public float[] MeanLevels {
        get { return meanLevels; }
    }
    #endregion

    #region Private functions
    void CheckBuffers ()
    {
        int numberOfSamples = (int)smplesNumber;
        if (rawSpectrum == null || rawSpectrum.Length != numberOfSamples) {
            rawSpectrum = new float[numberOfSamples];
        }
        var bandCount = middleFrequenciesForBands [(int)bandType].Length;
        if (levels == null || levels.Length != bandCount) {
            levels = new float[bandCount];
            peakLevels = new float[bandCount];
            meanLevels = new float[bandCount];
        }
    }

    int FrequencyToSpectrumIndex (float f)
    {
        var i = Mathf.FloorToInt (f / AudioSettings.outputSampleRate * 2.0f * rawSpectrum.Length);
        return Mathf.Clamp (i, 0, rawSpectrum.Length - 1);
    }
    #endregion

    #region Monobehaviour functions
    void Awake ()
    {
        CheckBuffers ();
    }

    void Update ()
    {
        CheckBuffers ();

        AudioListener.GetSpectrumData (rawSpectrum, 0, FFTWindow.BlackmanHarris);

        float[] middlefrequencies = middleFrequenciesForBands [(int)bandType];
        var bandwidth = bandwidthForBands [(int)bandType];

        var falldown = fallSpeed * Time.deltaTime;
        var filter = Mathf.Exp (-sensibility * Time.deltaTime);

        for (var bi = 0; bi < levels.Length; bi++) {
            int imin = FrequencyToSpectrumIndex (middlefrequencies [bi] / bandwidth);
            int imax = FrequencyToSpectrumIndex (middlefrequencies [bi] * bandwidth);

            var bandMax = 0.0f;
            for (var fi = imin; fi <= imax; fi++) {
                bandMax = Mathf.Max (bandMax, rawSpectrum [fi]);
            }

            levels [bi] = bandMax;
            peakLevels [bi] = Mathf.Max (peakLevels [bi] - falldown, bandMax);
            meanLevels [bi] = bandMax - (bandMax - meanLevels [bi]) * filter;
        }
    }
    #endregion
}