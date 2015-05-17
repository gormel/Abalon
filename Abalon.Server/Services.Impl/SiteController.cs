using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using Abalon.Server.Core;

namespace Abalon.Server.Services.Impl
{
	public class SiteController
	{
		readonly ConcurrentDictionary<string, Player> players = new ConcurrentDictionary<string, Player>();
		readonly List<Room> rooms = new List<Room>();

		public IEnumerable<Player> ConnectedPlayers { get { return players.Values; } }
		public IReadOnlyList<Room> Rooms { get { return rooms; } }

		public Player AddConnectedPlayer(AuthInfo info, string uid)
		{
			//TODO: password check
			Player result = new Player() {Name = info.Name, UID = uid};
			players.TryAdd(uid, result);
			return result;
		}

		public bool RemoveDisconnectedPlayer(string uid)
		{
			Player v;
			return players.TryRemove(uid, out v);
		}

		private readonly RandomNumberGenerator rng = new RNGCryptoServiceProvider();
		public string GenerateLogID()
		{
			byte[] tokenData = new byte[32];
			rng.GetBytes(tokenData);
			return string.Concat(tokenData.Select(b => b.ToString("X2")));
		}
	}
}