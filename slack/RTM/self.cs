using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slack
{
    public partial class RTM
    {


        public struct self
        {
            public Slack.TimeStamp created;
            public String id;
            public String manual_presence;
            public String name;
            public dynamic prefs;
        }


    }
}