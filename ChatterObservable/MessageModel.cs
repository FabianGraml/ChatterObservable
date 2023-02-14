using System;
namespace ChatterObservable;
public class MessageModel
{
    public string Time { get; set; } = DateTime.Now.ToString("HH:mm:ss");
    public string? Message { get; set; }
    public string? Username { get; set; }
    public override string ToString()
    {
        return $"[{Time}] {Username}: {Message}";
    }
}