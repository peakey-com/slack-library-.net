using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slack
{


    public class GroupCloseEventArgs
    {


        //https://api.slack.com/events/group_close


        private String _user;
        private String _channel;


        public GroupCloseEventArgs(dynamic Data)
        {
            _channel = Data.channel;
            _user = Data.user;
        }


        public String user
        {
            get
            {
                return _user;
            }
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
