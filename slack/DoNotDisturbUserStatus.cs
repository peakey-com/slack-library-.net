using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slack
{


    public class DoNotDisturbUserStatus
    {


        private Boolean _dnd_enabled;
        private Slack.TimeStamp _next_dnd_start_ts;
        private Slack.TimeStamp _next_dnd_end_ts;


        public DoNotDisturbUserStatus(dynamic Data)
        {
            _dnd_enabled = Data.dnd_enabled;
            _next_dnd_end_ts = new Slack.TimeStamp( (String)Data.next_dnd_end_ts);
            _next_dnd_start_ts = new Slack.TimeStamp( (String)Data.next_dnd_start_ts);
        }


        public Boolean dnd_enabled
        {
            get
            {
                return _dnd_enabled;
            }
        }


        public Slack.TimeStamp next_dnd_start_ts
        {
            get
            {
                return _next_dnd_start_ts;
            }
        }


        public Slack.TimeStamp next_dnd_end_ts
        {
            get
            {
                return _next_dnd_end_ts;
            }
        }

    
    }


}
