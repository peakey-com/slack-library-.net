using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slack
{


    //https://api.slack.com/events/star_added


    public class StarAddedEventArgs
    {


        private String _user;
        private dynamic _item;
        private String _event_ts;


        public StarAddedEventArgs(dynamic Data)
        {
            _user = Data.user;
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
