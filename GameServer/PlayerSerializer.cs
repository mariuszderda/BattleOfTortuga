using Newtonsoft.Json;
using System.Collections.Specialized;

namespace GameServer;

public static class PlayerSerializer
{
    public static string SerializePlayers(ListDictionary players)
    {
        return JsonConvert.SerializeObject(players);
    }
}