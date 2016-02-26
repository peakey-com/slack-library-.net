using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slack
{


    //https://api.slack.com/events/commands_changed


    public class CommandsChangedEventArgs
    {


        private String _event_ts;


        public CommandsChangedEventArgs(dynamic Data)
        {
            _event_ts = Data.event_ts;
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
