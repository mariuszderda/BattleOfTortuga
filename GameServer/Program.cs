using WebSocketSharp.Server;

namespace GameServer;

public static class Program
{
    private const string Url = "ws://127.0.0.1";
    private const string? Port = null;

    public static void Main(string[] args)
    {
        var port = Port == null ? "" : $":{Port}";

        var wssv = new WebSocketServer($"{Url}{port}");
        wssv.AddWebSocketService<GameManager>("/player");
        wssv.Start();
        Console.WriteLine($"Listen address: {wssv.Address}:{wssv.Port}");
        Console.ReadKey(true);
        wssv.Stop();
    }
}