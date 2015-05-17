using System.Linq;
using Abalon.Server.Services.Impl;
using Nancy;

namespace Abalon.Server.Modules
{
	public class RoomModule : NancyModule
	{
		private readonly RoomController roomController;
		public RoomModule(RoomController controller)
		{
			roomController = controller;
			Post["/room/create"] = par =>
			{
				Room created = roomController.CreateRoom((string) Request.Session["Key"]);
				if (created == null)
					return HttpStatusCode.BadRequest;
				return created.RoomID;
			};

			Post["/room/destroy"] = par =>
			{
				bool result = roomController.DestroyRoom((string) Request.Session["Key"]);
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
		}
	}
}