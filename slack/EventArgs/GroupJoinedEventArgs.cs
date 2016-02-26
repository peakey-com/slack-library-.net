using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slack
{


    public class GroupJoinedEventArgs
    {


        //https://api.slack.com/events/group_joined


        private String _channel;


        public GroupJoinedEventArgs(dynamic Data)
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
