using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CherrySharp.Interfaces
{
	public interface ISessionConfiguration
	{
		TimeSpan SessionLifespan { get; }
	}
}
