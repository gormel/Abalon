using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Abalon.Server.Core.Info;
using Abalon.Server.Services.Impl;
using Nancy.ModelBinding;

namespace Abalon.Server
{
	public class RoomModule : NancyModule
	{
		private SiteController siteController;
		public RoomModule(SiteController controller)
		{
			siteController = controller;
			Post["/room/create"] = par =>
			{
				Room created = siteController.CreateRoom((string) Request.Session["Key"]);
				if (created == null)
					return HttpStatusCode.BadRequest;
				return created.RoomID;
			};

			Post["/room/destroy"] = par =>
			{
				bool result = siteController.DestroyRoom((string) Request.Session["Key"]);
				return result ? HttpStatusCode.OK : HttpStatusCode.BadRequest;
			};
		}
	}
}