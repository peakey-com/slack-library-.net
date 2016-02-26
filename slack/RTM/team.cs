using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slack
{
    public partial class RTM
    {


        public struct team
        {
            public String domain;
            public String email_domain;
            public icon icon;
            public String id;
            public Int32 msg_edit_window_mins;
            public String name;
            public Boolean over_integrations_limit;
            public Boolean over_storage_limit;
            public String plan;
            public Dictionary<String, String> prefs;
        }


    }
}