using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slack
{


    public class ChannelDeletedEventArgs
    {


        //https://api.slack.com/events/channel_deleted


        private String _channel;


        public ChannelDeletedEventArgs(dynamic Data)
        {
            _channel = Data.channel;
        }


        public String channel
        {
            get
            {
                return _channel;
            }
        }


    }


}
