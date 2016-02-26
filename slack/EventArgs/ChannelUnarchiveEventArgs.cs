using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slack
{


    public class ChannelUnarchiveEventArgs
    {


        //https://api.slack.com/events/channel_unarchive


        private String _channel;
        private String _user;


        public ChannelUnarchiveEventArgs(dynamic Data)
        {
            _channel = Data.channel;
            _user = Data.user;
        }


        public String channel
        {
            get
            {
                return _channel;
            }
        }


        public String user
        {
            get
            {
                return _user;
            }
        }


    }


}
