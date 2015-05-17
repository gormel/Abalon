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
		readonly ConcurrentDictionary<string, Room> rooms = new ConcurrentDictionary<string, Room>();
		public IEnumerable<Room> Rooms { get { return rooms.Values; } }

		private readonly PlayerController playerController;
		public RoomController(PlayerController controller)
		{
			playerController = controller;
		}

		public Room CreateRoom(string uid)
		{
			if (!playerController.LoggedIn(uid))
				return null;
			string roomID;
			do
			{
				roomID = playerController.GenerateLogID();
			} while (rooms.ContainsKey(roomID));
			Room room = new Room()
			{
				Creator = playerController[uid],
				RoomID = roomID
			};
			rooms.TryAdd(uid, room);
			return room;
		}

		public bool DestroyRoom(string uid)
		{
			if (!playerController.LoggedIn(uid))
				return false;
			Room room = Rooms.First(r => r.Creator.UID == uid);
			Room val;
			return rooms.TryRemove(room.RoomID, out val);
		}

		public Room this[string uid]
		{
			get { return rooms[uid]; }
		}
	}
}
