namespace DiscordWebhookManager.Attributes;

public class Message {
    
    private string? _username;
    private string? _avatarUrl;
    private string? _content;
    public List<Embed> Embeds = new();
    private bool Tts { get; set; }
    
    public Message WithEmbed(Embed embed) {
        Embeds.Add(embed);
        return this;
    }

    public Embed PassEmbed() {
        Embed embed = new Embed(this);
        Embeds.Add(embed);
        return embed;
    }

    public Message WithUsername(string? value) {
        _username = value;
        return this;
    }

    public Message WithAvatar(string? value) {
        _avatarUrl = value;
        return this;
    }

    public Message WithContent(string? value) {
        _content = value;
        return this;
    }

    public Message WithTts() {
        Tts = true;
        return this;
    }

}