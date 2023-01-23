

using WebSocketSharp.Net.WebSockets;

public enum EventType
{
    Open,
    Message,
    Error,
    Close
}
public class WSEvent 
{
    public string Name;
    public string Data;
    public EventType Type;
}
