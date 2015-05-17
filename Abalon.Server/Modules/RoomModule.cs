using System.Linq;
using Abalon.Server.Services.Impl;
using Nancy;
using Nancy.Session;

namespace Abalon.Server.Modules
{
	public class RoomModule : NancyModule
	{
		private readonly RoomController roomController;
		private readonly PlayerController playerController;

		private string SessionID
		{
			get { return (string)Request.Session["SessionUID"]; }
		}

		public RoomModule(RoomController controller, PlayerController playerController)
		{
			roomController = controller;
			this.playerController = playerController;
			Post["/room/create"] = par =>
			{
				var player = playerController[SessionID];
				if (player == null)
					return HttpStatusCode.Unauthorized;
				Room created = roomController.CreateRoom(player);
				if (created == null)
					return HttpStatusCode.BadRequest;
				return created.RoomID;
			};

			Post["/room/destroy"] = par =>
			{
				var player = playerController[SessionID];
				if (player == null)
					return HttpStatusCode.Unauthorized;
				bool result = roomController.DestroyRoom(player);
				return result ? HttpStatusCode.OK : HttpStatusCode.BadRequest;
			};

			Get["/room/list"] = par =>
			{
				return roomController.Rooms.Select(r => r.RoomID).Aggregate((a, b) => a + ", " + b);
			};

			Get["/room/info/{uid}"] = par =>
			{
				Room room = roomController[par.uid];
				return string.Format("{0}, {1}, {2}", room.RoomID, room.Creator.Name, (room.Guest == null ? "null" : room.Guest.Name));
			};

			Post["/room/join/{rid}"] = par =>
			{
				var player = playerController[SessionID];
				if (player == null)
					return HttpStatusCode.Unauthorized;
				Room room = roomController[par.rid];
				if (room.Guest != null)
					return HttpStatusCode.Conflict;
				room.Guest = player;
				return HttpStatusCode.OK;
			};
		}
	}
}