using System.Collections.Specialized;

namespace GameServer;

public interface IPlayerManager
{
    void AddPlayer(GameManager manager);

    void RemovePlayer(string playerId);

    ListDictionary GetPlayersList();
}