using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Slack
{
	public partial class Exceptions
	{


		public class ServiceDisconnectedException : Exception
		{


			public ServiceDisconnectedException() : base("Slack service has been disconnected")
			{
			}


			public ServiceDisconnectedException(Exception exInner) : base("Slack service has been disconnected", exInner)
			{
			}


		}


	}
}