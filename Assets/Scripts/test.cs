using AAA.OpenAI;
using UnityEngine;

public class test : MonoBehaviour
{
    async void Start()
    {
        var chatGPTConnection = new ChatGPTConnection("");
        var test = await chatGPTConnection.RequestAsync("ゲームを作る方法を教えて");

        //await chatGPTConnection.RequestAsync("あなたは何が一番好き？");

    }
}
