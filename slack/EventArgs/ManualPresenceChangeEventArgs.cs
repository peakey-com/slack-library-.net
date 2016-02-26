using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slack
{


    //https://api.slack.com/events/manual_presence_change


    public class ManualPresenceChangeEventArgs
    {


        private String _presence;


        public ManualPresenceChangeEventArgs(dynamic Data)
        {
            _presence = Data.presence;
        }


        public String presence
        {
            get
            {
                return _presence;
            }
        }


    }


}
