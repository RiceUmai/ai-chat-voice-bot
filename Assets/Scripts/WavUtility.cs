using System;
using UnityEngine;

public class WavUtility
{
    public static AudioClip ToAudioClip(byte[] data)
    {
        int channels = BitConverter.ToInt16(data, 22);
        int sampleRate = BitConverter.ToInt32(data, 24);
        int pos = 44;
        int samples = (data.Length - pos) / 2;

        float[] audioData = new float[samples];
        int sampleIndex = 0;

        while (pos < data.Length)
        {
            short sample = (short)((data[pos + 1] << 8) | data[pos]);
            audioData[sampleIndex] = sample / 32768f;
            pos += 2;
            sampleIndex++;
        }

        AudioClip audioClip = AudioClip.Create("SynthesizedVoice", samples, channels, sampleRate, false);
        audioClip.SetData(audioData, 0);
        return audioClip;
    }
}
