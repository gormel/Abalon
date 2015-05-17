using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using Abalon.Server.Core;
using Abalon.Server.Core.Info;

namespace Abalon.Server.Services.Impl
{
	public class SiteController
	{
		readonly ConcurrentDictionary<string, Player> players = new ConcurrentDictionary<string, Player>();
		readonly ConcurrentDictionary<string, Room> rooms = new ConcurrentDictionary<string, Room>();

		public IEnumerable<Player> ConnectedPlayers { get { return players.Values; } }
		public IEnumerable<Room> Rooms { get { return rooms.Values; } }

		public Player AddConnectedPlayer(AuthInfo info, string uid)
		{
			//TODO: password check
			if (ConnectedPlayers.Any(p => p.Name == info.Name))
				return null;
			Player result = new Player() {Name = info.Name, UID = uid};
			bool res = players.TryAdd(uid, result);
			return res ? result : null;
		}

		public bool RemoveDisconnectedPlayer(string uid)
		{
			Player v;
			return players.TryRemove(uid, out v);
		}

		public Room CreateRoom(string uid)
		{
			if (!LoggedIn(uid))
				return null;
			string roomID;
			do
			{
				roomID = GenerateLogID();
			} while (rooms.ContainsKey(roomID));
			Room room = new Room()
			{
				Creator = players[uid],
				RoomID = roomID
			};
			rooms.TryAdd(uid, room);
			return room;
		}

		public bool DestroyRoom(string uid)
		{
			if (!LoggedIn(uid))
				return false;
			Room room = Rooms.First(r => r.Creator.UID == uid);
			Room val;
			return rooms.TryRemove(room.RoomID, out val);
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
	}
}