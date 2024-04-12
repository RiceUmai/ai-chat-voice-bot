using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[Serializable]
public class ChatBotView
{
    [SerializeField]
    private string chatGPTAPIKey;
    public string ChatGPTAPIKey => chatGPTAPIKey;

    [SerializeField]
    private StyleBertVITS2APIManager styleBertVITS2APIManager;
    public StyleBertVITS2APIManager StyleBertVITS2APIManager => styleBertVITS2APIManager;

    [SerializeField]
    private AudioManager audioManager;
    public AudioManager AudioManager => audioManager;

    [SerializeField]
    private TMP_Text responseMessageText;
    public TMP_Text ResponseMessageText => responseMessageText;

    [SerializeField]
    private TMP_InputField messageInputField;
    public TMP_InputField MessageInputField => messageInputField;

    [SerializeField]
    private Button sendMessageBtn;
    public Button SendMessageBtn => sendMessageBtn;
}
