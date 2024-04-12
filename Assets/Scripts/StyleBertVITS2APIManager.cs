using System;
using System.Collections.Generic;
using System.Text;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class StyleBertVITS2APIManager : MonoBehaviour
{
    [SerializeField]
    private string baseUrl = "http://127.0.0.1:5000/";

    public async UniTask<List<AudioClip>> SendVoiceRequest(List<string> textList)
    {
        List<AudioClip> audioClipList = new List<AudioClip>();

        foreach (var text in textList)
        {
            audioClipList.Add(await SendVoiceRequest(text));
        }

        return audioClipList;
    }

    public async UniTask<AudioClip> SendVoiceRequest(string text)
    {
        TTSParameters parameters = new TTSParameters(text);

        var url = $"{baseUrl}voice?{ToQueryString(parameters)}";
        using var request = UnityWebRequest.Get(url);
        request.SetRequestHeader("Accept", "audio/wav");
        await request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError($"Error: {request.error}");
            return null;
        }
        else
        {
            Debug.Log($"Successed");

            byte[] audioData = request.downloadHandler.data;
            return WavUtility.ToAudioClip(audioData);
        }
    }


    private string ToQueryString(TTSParameters parameters)
    {
        StringBuilder query = new StringBuilder();
        query.Append($"text={UnityWebRequest.EscapeURL(parameters.Text)}")
             .Append($"&encoding={UnityWebRequest.EscapeURL(parameters.Encoding)}")
             .Append($"&model_id={parameters.ModelId}")
             .Append($"&speaker_id={parameters.SpeakerId}");

        if (!string.IsNullOrEmpty(parameters.SpeakerName))
        {
            query.Append($"&speaker_name={UnityWebRequest.EscapeURL(parameters.SpeakerName)}");
        }

        query.Append($"&sdp_ratio={parameters.SdpRatio}")
             .Append($"&noise={parameters.Noise}")
             .Append($"&noisew={parameters.Noisew}")
             .Append($"&length={parameters.Length}")
             .Append($"&language={UnityWebRequest.EscapeURL(parameters.Language)}")
             .Append($"&auto_split={parameters.AutoSplit.ToString().ToLower()}")
             .Append($"&split_interval={parameters.SplitInterval}");

        if (!string.IsNullOrEmpty(parameters.AssistText))
        {
            query.Append($"&assist_text={UnityWebRequest.EscapeURL(parameters.AssistText)}");
        }

        query.Append($"&assist_text_weight={parameters.AssistTextWeight}")
             .Append($"&style={UnityWebRequest.EscapeURL(parameters.Style)}")
             .Append($"&style_weight={parameters.StyleWeight}");

        if (!string.IsNullOrEmpty(parameters.ReferenceAudioPath))
        {
            query.Append($"&reference_audio_path={UnityWebRequest.EscapeURL(parameters.ReferenceAudioPath)}");
        }

        return query.ToString();
    }
}

[Serializable]
public class TTSParameters
{
    public string Text;
    public string Encoding;
    public int ModelId;
    public string SpeakerName;
    public int SpeakerId;
    public float SdpRatio;
    public float Noise;
    public float Noisew;
    public float Length;
    public string Language;
    public bool AutoSplit;
    public float SplitInterval;
    public string AssistText;
    public float AssistTextWeight;
    public string Style;
    public float StyleWeight;
    public string ReferenceAudioPath;

    public TTSParameters(string text)
    {
        Text = text;
        Encoding = "utf-8";
        ModelId = 0;
        SpeakerName = string.Empty;
        SpeakerId = 0;
        SdpRatio = 0.2f;
        Noise = 0.6f;
        Noisew = 0.8f;
        Length = 1f;
        Language = "JP";
        AutoSplit = true;
        SplitInterval = 0.5f;
        AssistText = string.Empty;
        AssistTextWeight = 1f;
        Style = "Neutral";
        StyleWeight = 5;
        ReferenceAudioPath = string.Empty;
    }
}