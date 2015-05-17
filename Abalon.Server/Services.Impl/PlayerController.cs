using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using Abalon.Server.Core;
using Abalon.Server.Core.Info;
using Nancy.Session;

namespace Abalon.Server.Services.Impl
{
	public class PlayerController
	{
		readonly ConcurrentDictionary<string, Player> players = new ConcurrentDictionary<string, Player>();

		public IEnumerable<Player> ConnectedPlayers { get { return players.Values; } }

		public Player AddConnectedPlayer(AuthInfo info)
		{
			//TODO: password check
			if (ConnectedPlayers.Any(p => p.Name == info.Name))
				return null;
			Player result = new Player() {Name = info.Name, UID = GenerateLogID()};
			bool res = players.TryAdd(result.UID, result);
			return res ? result : null;
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

		public bool LoggedIn(string uid)
		{
			return players.ContainsKey(uid);
		}

		public Player this[string uid]
		{
			get { return players[uid]; }
		}

		public Player this[ISession session]
		{
			get { return this[(string)session["SessionUID"]]; }
		}
	}
}