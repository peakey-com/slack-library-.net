using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slack
{


    //https://api.slack.com/events/pin_removed


    public class PinRemovedEventArgs
    {


        private String _user;
        private String _channel_id;
        private dynamic _item;
        private Boolean _has_pins;
        private String _event_ts;


        public PinRemovedEventArgs(dynamic Data)
        {
            _user = Data.user;
            _channel_id = Data.channel_id;
            _item = Data.item;
            _has_pins = Data.has_pins;
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


        public Boolean has_pins
        {
            get
            {
                return _has_pins;
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
