using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Abalon.Server.Services.Impl
{
	public class SiteController : ISiteController
	{
        readonly List<Player> players = new List<Player>();
        readonly List<Room> rooms = new List<Room>();

        public IReadOnlyList<Player> ConnectedPlayers { get { return players; } }
        public IReadOnlyList<Room> Rooms { get { return rooms; } }

        public void AddConnectedPlayer(Player player)
        {
            lock (players)
            {
                players.Add(player);
            }
        }

        public void RemoveDisconnectedPlayer(Player player)
        {
            lock (players)
            {
                players.Remove(player);
            }
        }
	}
}