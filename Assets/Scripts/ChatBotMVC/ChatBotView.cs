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
    private TMP_Text responseMessageText;
    public TMP_Text ResponseMessageText => responseMessageText;

    [SerializeField]
    private TMP_InputField messageInputField;
    public TMP_InputField MessageInputField => messageInputField;

    [SerializeField]
    private Button sendMessageBtn;
    public Button SendMessageBtn => sendMessageBtn;
}
