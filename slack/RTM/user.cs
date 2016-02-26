using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slack
{
    public partial class RTM
    {


        public class user
        {
            public String color;
            public Boolean deleted;
            public String id;
            public Boolean is_admin;
            public Boolean is_bot;
            public Boolean is_owner;
            public Boolean is_primary_owner;
            public Boolean is_restrictred;
            public Boolean is_ultra_restricted;
            public String name;
            public String presence;
            public Dictionary<String, String> profile;
            public String real_name;
            public String team_id;
            public String tz;
            public String tz_label;
            public Int32 tz_offset;

        }


    }
}