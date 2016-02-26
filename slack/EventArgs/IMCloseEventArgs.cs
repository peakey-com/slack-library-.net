using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slack
{


    public class IMCloseEventArgs
    {


        //https://api.slack.com/events/im_close


        private String _user;
        private String _channel;


        public IMCloseEventArgs(dynamic Data)
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
