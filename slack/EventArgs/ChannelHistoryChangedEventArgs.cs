using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slack
{


    public class ChannelHistoryChangedEventArgs
    {


        //https://api.slack.com/events/channel_history_changed


        private String _type;
        private String _latest;
        private String _ts;
        private String _event_ts;


        public ChannelHistoryChangedEventArgs(dynamic Data)
        {
            _event_ts = Data.event_ts;
            _latest = Data.latest;
            _ts = Data.ts;
            _type = Data.type;
        }


        public String type
        {
            get
            {
                return _type;
            }
        }


        public String latest
        {
            get
            {
                return _latest;
            }
        }


        public String ts
        {
            get
            {
                return _ts;
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
