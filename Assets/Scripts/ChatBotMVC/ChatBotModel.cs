using AAA.OpenAI;
using System.Threading;

public class ChatBotModel
{
    public ChatGPTConnection ChatGPTConnection;
    public CancellationTokenSource Cts = new CancellationTokenSource();
}
