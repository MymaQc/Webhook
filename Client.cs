using System.Net;
using System.Text;
using DiscordWebhookManager.Attributes;
using Newtonsoft.Json;

namespace DiscordWebhookManager;

public class Client {
    
    private readonly Uri _webhookUrl;

    public Client(string url) {
        _webhookUrl = new Uri(url);
    }

    public void PostMessage(Message message) {
        HttpWebRequest webRequest = WebRequest.CreateHttp(_webhookUrl);
        webRequest.Method = "POST";
        webRequest.ContentType = "application/json";

        string payload = JsonConvert.SerializeObject(message) ?? throw new ArgumentNullException(nameof(message));
        byte[] buffer = Encoding.UTF8.GetBytes(payload) ?? throw new ArgumentNullException(nameof(message));

        webRequest.ContentLength = buffer.Length;
        using (Stream stream = webRequest.GetRequestStream()) {
            stream.Write(buffer, 0, buffer.Length);
            stream.Flush();
        }

        HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse();
    }

    public async Task PostMessageAsync(Message message) {
        HttpWebRequest request = WebRequest.CreateHttp(_webhookUrl);
        request.Method = "POST";
        request.ContentType = "application/json";

        string payload = JsonConvert.SerializeObject(message) ?? throw new ArgumentNullException(nameof(message));
        byte[] buffer = Encoding.UTF8.GetBytes(payload) ?? throw new ArgumentNullException(nameof(message));

        request.ContentLength = buffer.Length;
        await using (Stream stream = await request.GetRequestStreamAsync()) {
            await stream.WriteAsync(buffer);
            await stream.FlushAsync();
        }

        _ = (HttpWebResponse)await request.GetResponseAsync();
    }

}