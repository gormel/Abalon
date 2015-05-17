using Abalon.Server.Services;
using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Abalon.Server
{
	internal enum Players
	{
		Black,
		White
	}

	public class MainClass : NancyModule
	{
		public MainClass(ISiteController siteController)
		{
			Get["/"] = p => "hello world";

			Get["/list"] = p =>
			{
                return string.Join(Environment.NewLine,
                    siteController.ConnectedPlayers.Select(pl => pl.Name));
			};
		}
	}
}