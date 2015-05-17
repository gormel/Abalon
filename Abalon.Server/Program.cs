using Abalon.Server.Services;
using Abalon.Server.Services.Impl;
using Nancy;
using Nancy.Hosting.Self;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy.Bootstrapper;
using Nancy.Session;
using Nancy.TinyIoc;

namespace Abalon.Server
{
	static class Program
	{
		static void Main(string[] args)
		{
			var uri = new Uri("http://localhost:8080");
			var bootstrapper = new Bootstrapper();
			var hostConfig = new HostConfiguration()
			{
				UrlReservations = new UrlReservations() { CreateAutomatically = true }
			};

			using (var host = new NancyHost(uri, bootstrapper, hostConfig))
			{
				host.Start();
				Console.WriteLine("Server started at " + uri);
				Console.ReadLine();
			}
		}
	}

	public class Bootstrapper : DefaultNancyBootstrapper
	{
		protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
		{
			CookieBasedSessions.Enable(pipelines);
		}
	}
}