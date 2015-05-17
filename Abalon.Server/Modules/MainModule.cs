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
		public MainClass()
		{
			Get["/"] = p => "hello world";

			Get["/list"] = p =>
				{
					if (SiteController.Instance.Value.ConnectedPlayers.Count < 1)
						return "";
					return SiteController.Instance.Value.ConnectedPlayers.Select(pl => pl.Name).Aggregate((a, b) => a + Environment.NewLine + b);
				};
		}
	}
}