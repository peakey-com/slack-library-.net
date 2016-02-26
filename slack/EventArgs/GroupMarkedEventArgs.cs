using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slack
{


    public class GroupMarkedEventArgs
    {


        //https://api.slack.com/events/group_marked


        private String _channel;
        private String _ts;


        public GroupMarkedEventArgs(dynamic Data)
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
