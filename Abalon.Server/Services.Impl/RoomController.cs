using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abalon.Server.Services.Impl
{
	public class RoomController
	{
		readonly PlayerController playerController;

		readonly ConcurrentDictionary<string, Room> rooms = new ConcurrentDictionary<string, Room>();
		public IEnumerable<Room> Rooms { get { return rooms.Values; } }

		public RoomController(PlayerController playerController)
		{
			this.playerController = playerController;
		}

		public Room CreateRoom(Player player)
		{
			if (player == null)
				throw new ArgumentNullException("player");
			string roomID;
			do
			{
				roomID = playerController.GenerateLogID();
			} while (rooms.ContainsKey(roomID));
			Room room = new Room()
			{
				Creator = player,
				RoomID = roomID
			};
			rooms.TryAdd(roomID, room);
			return room;
		}

		public bool DestroyRoom(Player player)
		{
			if (player == null)
				throw new ArgumentNullException("player");
			Room room = Rooms.First(r => r.Creator == player);
			Room val;
			return rooms.TryRemove(room.RoomID, out val);
		}

		public Room this[string uid]
		{
			get { return rooms[uid]; }
		}
	}
}
