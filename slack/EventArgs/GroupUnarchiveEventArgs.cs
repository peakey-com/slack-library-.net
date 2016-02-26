using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slack
{


    public class GroupUnarchiveEventArgs
    {


        //https://api.slack.com/events/group_unarchive


        private String _channel;


        public GroupUnarchiveEventArgs(dynamic Data)
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
