using AAA.OpenAI;
using Cysharp.Threading.Tasks;
using System;
using UniRx;
using UnityEngine;

public class ChatBotController : MonoBehaviour
{
    [SerializeField]
    private ChatBotView view;
    private ChatBotModel model = new ChatBotModel();

    private void Awake()
    {
        model.ChatGPTConnection = new ChatGPTConnection(view.ChatGPTAPIKey);
        BindView();
    }

    private void BindView()
    {
        view.SendMessageBtn
            .OnClickAsObservable()
            .AsObservable()
            .ThrottleFirst(TimeSpan.FromMilliseconds(2000))
            .Subscribe(_ =>
            {
                OnSendMessageToChatGPTEvent().Forget();
            }).AddTo(this);
    }

    private async UniTask OnSendMessageToChatGPTEvent()
    {
        try
        {
            view.SendMessageBtn.interactable = false;

            ChatGPTResponseModel response = await model.ChatGPTConnection.RequestAsync(view.MessageInputField.text);
            string responseMessage = response.choices[0].message.content;
            view.ResponseMessageText.text = responseMessage;

            TTSParameters ttsParameters = new TTSParameters(responseMessage);
            AudioClip AudioClip = await view.StyleBertVITS2APIManager.SendVoiceRequest(ttsParameters);
            view.AudioManager.PlayAudioClip(AudioClip);
        }
        catch (Exception e) 
        {
            Debug.Log(e.Message);
        }
        finally
        {
            view.SendMessageBtn.interactable = true;
        }
    }
}
