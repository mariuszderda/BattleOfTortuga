using System.Collections.Specialized;

namespace GameServer;

public class PlayersManagerInstance : IPlayerManager
{
    private static readonly Lock Lock = new();
    private static PlayersManagerInstance? _instance = null;
    private readonly List<GameManager> _players = [];

    private PlayersManagerInstance()
    {
    }

    public static PlayersManagerInstance GetInstance()
    {
        lock (Lock) {
            _instance ??= new PlayersManagerInstance();
        }

        return _instance;
    }

    public void AddPlayer(GameManager manager)
    {
        _players.Add(manager);
    }

    public void RemovePlayer(string playerId)
    {
        _players.RemoveAll(x => x.ID == playerId);
    }

    public ListDictionary GetPlayersList()
    {
        var list = new ListDictionary();

        foreach (var player in _players) {
            var pl = player.Context.QueryString.Get(0);
            list.Add(player.ID, pl);
        }

        return list;
    }
}