using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slack.Channels
{


    //https://api.slack.com/methods/channels.create


    public class CreateResponse
    {


        private RTM.channel _channel;


        public CreateResponse(dynamic Response)
        {
            _channel = new RTM.channel();
            if (Utility.HasProperty(Response, "channel"))
            {
                _channel.id = Utility.TryGetProperty(Response.channel, "id");
                _channel.name = Utility.TryGetProperty(Response.channel, "name");
                _channel.created = Utility.TryGetProperty(Response.channel, "created", 0);
                _channel.creator = Utility.TryGetProperty(Response.channel, "creator");
                _channel.is_archived = Utility.TryGetProperty(Response.channel, "is_archived", false);
                _channel.is_member = Utility.TryGetProperty(Response.channel, "is_member", false);
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
