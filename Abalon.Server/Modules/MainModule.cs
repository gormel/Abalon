using System;
using System.Linq;
using Abalon.Server.Services.Impl;
using Nancy;

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
		}
	}
}