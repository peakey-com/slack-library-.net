using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slack
{


    public class ChannelJoinedEventArgs
    {


        //https://api.slack.com/events/channel_joined


        private String _id;


        public ChannelJoinedEventArgs(dynamic Data)
        {
            _id = Data.id;
        }


        public String id
        {
            get
            {
                return _id;
            }
        }


    }


}
