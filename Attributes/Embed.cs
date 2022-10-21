using Newtonsoft.Json;

namespace DiscordWebhookManager.Attributes;

public class Embed {

    private Message _message;
    private int _color;
    private Author? _author;
    private string? _title;
    private string? _url;
    private string? _description;
    private readonly List<Field> _fields = new();
    private Image? _image;
    private Image? _thumbnail;
    private Footer? _footer;
    private string? _timestamp;

    internal Embed(Message message) {
        _message = message;
    }

    public Message Finalize() {
        return _message ?? (_message = new Message { Embeds = new List<Embed> { this } });
    }

    public Embed WithTitle(string? value) {
        _title = value;
        return this;
    }

    public Embed WithUrl(string? value) {
        _url = value;
        return this;
    }

    public Embed WithDescription(string? value) {
        _description = value;
        return this;
    }

    public Embed WithTimestamp(DateTime value) {
        _timestamp = Utils.Helper.DateTimeToIso(value.ToLocalTime());
        return this;
    }

    public Embed WithField(string name, string value, bool inline = true) {
        _fields.Add(new Field { Name = name, Value = value, Inline = inline });
        return this;
    }

    public Embed WithImage(string value) {
        _image = new Image { Url = value };
        return this;
    }

    public Embed WithThumbnail(string value) {
        _thumbnail = new Image { Url = value };
        return this;
    }

    public Embed WithFooter(string text, string iconUrl) {
        _footer = new Footer { Text = text, IconUrl = iconUrl };
        return this;
    }

    public Embed WithAuthor(string name, string? url = null, string? icon = null) {
        _author = new Author { Name = name, Url = url, IconUrl = icon };
        return this;
    }

    public Embed WithColor(Color color) {
        _color = BitConverter.ToInt32(new byte[4] { color.B, color.G, color.R, 0 }, 0);
        return this;
    }

    private static byte Clamp(float value) {
        return (byte)(Math.Round(value * 255, 0));
    }

}