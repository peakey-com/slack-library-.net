using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slack
{


    public class GroupLeftEventArgs
    {


        //https://api.slack.com/events/group_left


        private String _channel;


        public GroupLeftEventArgs(dynamic Data)
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
