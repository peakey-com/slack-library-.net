using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Slack.Channels
{


    //https://api.slack.com/methods/channels.join


    public class JoinResponse
    {


        private RTM.channel _channel;


        public JoinResponse(dynamic Response)
        {
            _channel = new RTM.channel();
            _channel.id = Response.channel.id;
            _channel.name = Response.channel.name;
            _channel.created = Response.channel.created;
            _channel.creator = Response.channel.creator;
            _channel.is_archived = Response.channel.is_archived;
            _channel.is_member = Response.channel.is_member;
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
