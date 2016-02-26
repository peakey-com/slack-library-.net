using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slack
{


    public class GroupArchiveEventArgs
    {


        //https://api.slack.com/events/group_archive


        private String _channel;


        public GroupArchiveEventArgs(dynamic Data)
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
