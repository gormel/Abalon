using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Abalon.Server
{
	public class SiteController
	{
		public static readonly Lazy<SiteController> Instance = new Lazy<SiteController>(() => new SiteController());

		public List<Player> ConnectedPlayers { get; set; }
		public List<Room> Rooms { get; private set; } 

		private SiteController()
		{
			ConnectedPlayers = new List<Player>();
			Rooms = new List<Room>();
		}
	}
}