using Nancy;

namespace Abalon.Server.Modules
{
	internal enum Players
	{
		Black,
		White
	}

	public class MainModule : NancyModule
	{
		public MainModule()
		{
			Get["/"] = p => Response.AsFile("Content/index.html", "text/html");
		}
	}
}