using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Slack.Channels
{


    //https://api.slack.com/methods/channels.rename


    public class RenameResponse
    {


        private RTM.channel _channel;


        public RenameResponse(dynamic Response)
        {
            if (Utility.HasProperty(Response, "channel"))
            {
                _channel = new RTM.channel(Response.channel);
            }
            else
            {
                _channel = new RTM.channel();
            }
        }


        public RTM.channel channel
        {
            get
            {
                return _channel;
            }
        }


    }


}
