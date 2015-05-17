using Abalon.Server.Services;
using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Abalon.Server.Core;
using Abalon.Server.Services.Impl;
using Nancy.ModelBinding;

namespace Abalon.Server
{
	public class LoginModule : NancyModule
	{
		readonly SiteController siteController;

		public LoginModule(SiteController siteController)
		{
			this.siteController = siteController;
			Post["/login"] = par =>
			{
				var info = this.Bind<AuthInfo>();
				var pl = siteController.AddConnectedPlayer(info);
				if (pl == null) { return HttpStatusCode.Unauthorized; }
				Request.Session["SessionUID"] = pl.UID;
				return HttpStatusCode.OK;
			};

			Post["/logout"] = par =>
			{
				bool logoutResult = siteController.RemoveDisconnectedPlayer((string)Request.Session["SessionUID"]);
				return logoutResult ? HttpStatusCode.OK : HttpStatusCode.BadRequest;
			};
		}
	}
}