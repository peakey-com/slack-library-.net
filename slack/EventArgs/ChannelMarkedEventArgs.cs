using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slack
{


    public class ChannelMarkedEventArgs
    {


        //https://api.slack.com/events/channel_marked


        private String _channel;
        private String _ts;


        public ChannelMarkedEventArgs(dynamic Data)
        {
            _channel = Data.channel;
            _ts = Data.ts;
        }


        public String channel
        {
            get
            {
                return _channel;
            }
        }


        public String ts
        {
            get
            {
                return _ts;
            }
        }


    }


}
