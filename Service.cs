using System.Net;
using System.Text;
using DiscordWebhookManager.Attributes;
using Newtonsoft.Json;

namespace DiscordWebhookManager;

public class Service {

    public static void PostMessage(string webhookUrl, Message message) {
        HttpWebRequest webRequest = WebRequest.CreateHttp(webhookUrl);
        webRequest.Method = "POST";
        webRequest.ContentType = "application/json";

        string payload = JsonConvert.SerializeObject(message);
        byte[] buffer = Encoding.UTF8.GetBytes(payload);

        webRequest.ContentLength = buffer.Length;

        using (Stream stream = webRequest.GetRequestStream()) {
            stream.Write(buffer, 0, buffer.Length);
            stream.Flush();
        }

        HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse();
    }

    public static async Task PostMessageAsync(string webhookUrl, Message message) {
        HttpWebRequest webRequest = WebRequest.CreateHttp(webhookUrl);
        webRequest.Method = "POST";
        webRequest.ContentType = "application/json";

        string payload = JsonConvert.SerializeObject(message) ?? throw new ArgumentNullException(nameof(webhookUrl));
        byte[] buffer = Encoding.UTF8.GetBytes(payload) ?? throw new ArgumentNullException(nameof(webhookUrl));

        webRequest.ContentLength = buffer.Length;

        await using (Stream stream = await webRequest.GetRequestStreamAsync()) {
            await stream.WriteAsync(buffer, 0, buffer.Length);
            await stream.FlushAsync();
        }

        _ = (HttpWebResponse)await webRequest.GetResponseAsync();
    }
    
}