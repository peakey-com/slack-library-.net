using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slack
{
    public partial class RTM
    {


        public struct dnd
        {
            public Boolean dnd_enabled;
            public Slack.TimeStamp next_dnd_end_ts;
            public Slack.TimeStamp next_dnd_start_ts;
            public Boolean snooze_enabled;
        }


    }
}