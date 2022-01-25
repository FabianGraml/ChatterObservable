using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ChatterObservable;

public class MessageModel
{
    public DateTime Time { get; set; } = DateTime.Now;
    public string? Message { get; set; }
    public string? Username { get; set; }
    public override string ToString()
    {
        return $"[{Time}] {Username}: {Message}";
    }
}

