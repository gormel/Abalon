using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abalon.Server.Services
{
    public interface ISiteController
    {
        IReadOnlyList<Player> ConnectedPlayers { get; }
        IReadOnlyList<Room> Rooms { get; }

        void AddConnectedPlayer(Player player);
        void RemoveDisconnectedPlayer(Player player);
    }
}
