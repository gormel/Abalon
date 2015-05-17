using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Abalon.Server
{
	public class RoomModule : NancyModule
	{
		public RoomModule()
		{
			Get["/room/{uid}"] = p =>
			{

				return "";
			};
		}
	}
}