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
				var pl = siteController.AddConnectedPlayer(info, (string)Request.Session["Key"]);
				if (pl == null)
					return HttpStatusCode.Unauthorized;
				return HttpStatusCode.OK;
			};

			Post["/logout"] = par =>
			{
				bool logoutResult = siteController.RemoveDisconnectedPlayer((string)Request.Session["Key"]);
				return logoutResult ? HttpStatusCode.OK : HttpStatusCode.BadRequest;
			};
		}
	}
}