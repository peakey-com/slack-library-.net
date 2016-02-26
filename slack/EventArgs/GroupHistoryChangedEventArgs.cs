using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slack
{


    public class GroupHistoryChangedEventArgs
    {


        //https://api.slack.com/events/group_history_changed


        private String _latest;
        private String _ts;
        private String _event_ts;


        public GroupHistoryChangedEventArgs(dynamic Data)
        {
            _event_ts = Data.event_ts;
            _latest = Data.latest;
            _ts = Data.ts;
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
