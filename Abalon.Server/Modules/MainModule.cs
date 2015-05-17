using Abalon.Server.Services;
using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Web;
using Abalon.Server.Services.Impl;

namespace Abalon.Server
{
	internal enum Players
	{
		Black,
		White
	}

	public class MainClass : NancyModule
	{
		public MainClass(SiteController siteController)
		{
			Get["/"] = p => Response.AsFile("Content/index.html", "text/html");

			Get["/list"] = p =>
			{
				return siteController.ConnectedPlayers
					.Select(pl => new { name = pl.Name }).ToArray();
			};
		}
	}
}