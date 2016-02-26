using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slack
{


    //https://api.slack.com/events/pin_added


    public class PinAddedEventArgs
    {


        private String _user;
        private String _channel_id;
        private dynamic _item;
        private String _event_ts;


        public PinAddedEventArgs(dynamic Data)
        {
            _user = Data.user;
            _channel_id = Data.channel_id;
            _item = Data.item;
            _event_ts = Data.event_ts;
        }


        public String user
        {
            get
            {
                return _user;
            }
        }


        public String channel_id
        {
            get
            {
                return _channel_id;
            }
        }


        public dynamic item
        {
            get
            {
                return _item;
            }
        }


        public String event_ts
        {
            get
            {
                return _event_ts;
            }
        }


    }


}
