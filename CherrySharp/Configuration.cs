using System;
using CherrySharp.Interfaces;

namespace CherrySharp{
	public class Configuration : IConfiguration {
		public Configuration(){
			Protocol = "http";
			Port = 8080;

			SessionLifespan = TimeSpan.FromHours(1);
		}

		public string Protocol { get; set; }

		public int Port { get; set; }

		public TimeSpan SessionLifespan { get; set; }
	}
}