using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slack
{


    public class DoNotDisturbStatus
    {


        private Boolean _dnd_enabled;
        private Slack.TimeStamp _next_dnd_start_ts;
        private Slack.TimeStamp _next_dnd_end_ts;
        private Boolean _snooze_enabled;
        private Slack.TimeStamp _snooze_endtime;
        private Int32 _snooze_remaining;


        public DoNotDisturbStatus(dynamic Data)
        {
            if (!Utility.HasProperty(Data, "dnd_status"))
            {
                return;
            }
            _dnd_enabled = Utility.TryGetProperty(Data.dnd_status, "dnd_enabled");
            _next_dnd_end_ts = new Slack.TimeStamp(Utility.TryGetProperty(Data.dnd_status, "next_dnd_end_ts"));
            _next_dnd_start_ts = new Slack.TimeStamp(Utility.TryGetProperty(Data.dnd_status, "dnd_start_ts"));
            _snooze_enabled = Utility.TryGetProperty(Data.dnd_status, "snooze_enabled", false);
            _snooze_endtime = new Slack.TimeStamp(Utility.TryGetProperty(Data.dnd_status, "snooze_endtime"));
            _snooze_remaining = Utility.TryGetProperty(Data.dnd_status, "snooze_remaining", 0);
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


        public Boolean snooze_enabled
        {
            get
            {
                return _snooze_enabled;
            }
        }


        public Slack.TimeStamp snooze_endtime
        {
            get
            {
                return _snooze_endtime;
            }
        }


        public Int32 snooze_remaining
        {
            get
            {
                return _snooze_remaining;
            }
        }

    
    }


}
