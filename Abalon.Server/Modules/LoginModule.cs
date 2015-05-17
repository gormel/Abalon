using System;
using System.Linq;
using Abalon.Server.Core.Info;
using Abalon.Server.Services.Impl;
using Nancy;
using Nancy.ModelBinding;

namespace Abalon.Server.Modules
{
	public class LoginModule : NancyModule
	{
		readonly PlayerController playerController;

		public LoginModule(PlayerController playerController)
		{
			this.playerController = playerController;
			Post["/login"] = par =>
			{
				var info = this.Bind<AuthInfo>();
				var pl = playerController.AddConnectedPlayer(info, (string)Request.Session["Key"]);
				if (pl == null)
					return HttpStatusCode.Unauthorized;
				return HttpStatusCode.OK;
			};

			Post["/logout"] = par =>
			{
				bool logoutResult = playerController.RemoveDisconnectedPlayer((string)Request.Session["Key"]);
				return logoutResult ? HttpStatusCode.OK : HttpStatusCode.BadRequest;
			};

			Get["/list"] = p =>
			{
				return string.Join(Environment.NewLine,
					playerController.ConnectedPlayers.Select(pl => pl.Name));
			};

			Get["/user/info/{name}"] = par =>
			{
				Player player = playerController.ConnectedPlayers.FirstOrDefault(p => p.Name == par.name);
				if (player == null)
					return HttpStatusCode.Unauthorized;
				return string.Format("{0}", player.Name);
			};
		}
	}
}